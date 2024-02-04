using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace FastTypes.Compiler
{
    internal static class FluentILExt
    {
        public static FluentIL BoxIfNeeded(this FluentIL il, Type t)
        {
            if (t.IsValueType) il.Box(t);

            return il;
        }

        public static FluentIL LoadArguments(this FluentIL il, MethodInfo info)
        {
            var n = info.GetParameters().Length;
            for (int i = 0; i < n; i++)
            {
                il.LoadArgument(i);
            }
            //switch (n)
            //{
            //    case 1:
            //        //Always load instance
            //        il.Emit(OpCodes.Ldarg_0);
            //        break;
            //    case 2:
            //        //Always load instance
            //        il.Emit(OpCodes.Ldarg_0);
            //        il.Emit(OpCodes.Ldarg_1);
            //        break;
            //    case 3:
            //        //Always load instance
            //        il.Emit(OpCodes.Ldarg_0);
            //        il.Emit(OpCodes.Ldarg_1);
            //        il.Emit(OpCodes.Ldarg_2);
            //        break;
            //    case 4:
            //        il.Emit(OpCodes.Ldarg_0);
            //        il.Emit(OpCodes.Ldarg_1);
            //        il.Emit(OpCodes.Ldarg_2);
            //        il.Emit(OpCodes.Ldarg_3);
            //        break;
            //    default:
            //        for (int i = 0; i < args.Count; i++)
            //        {
            //            il.Emit(OpCodes.Ldarg_S, i);
            //        }
            //        break;
            //}
            return il;
        }

        public static FluentIL CheckNull(
            this FluentIL il,
            Type targetType,
            Action<FluentIL> isNullBlock,
            Action<FluentIL> isNotNullBlock,
            bool debugIgnore = false)
        {
            if (!targetType.IsValueType && !debugIgnore)
            {
                //
                var notnullLabel = il.DefineLabel("NullCheck_RefNotNull");
                var endLabel = il.DefineLabel("NullCheck_End");

                //
                il
                    .Duplicate()
                    .Nop("Checking if the value on top of the stack is null")
                    //Since null is 0, then it is also false
                    .BranchIfTrue(notnullLabel)
                    //TODO: Add support for branch validations
                    .Pop()
                    .Delegate(isNullBlock)
                    .Branch(endLabel)
                    .MarkLabel(ref notnullLabel)
                    .Delegate(isNotNullBlock)
                    .MarkLabel(ref endLabel);
            }
            else
            {
                isNotNullBlock(il);
            }

            return il;
        }

        public static FluentIL GetElementAtIndex(this FluentIL il, LocalBuilder array, LocalBuilder index)
        {
            var elementType = array.LocalType.GetElementType();
            il
                .Nop($"{il.ResolveLocalName(array)}[{il.ResolveLocalName(index)}]")
                .LoadLocal(array)
                .LoadLocal(index);

            if (elementType.IsValueType)
            {
                return il.LoadElement(elementType);
            }
            else
            {
                return il.LoadElementReference(elementType);
            }
        }

        public static FluentIL SetElementAtIndex(this FluentIL il, LocalBuilder array, LocalBuilder index)
        {
            var elementType = array.LocalType.GetElementType();
            il
                .Nop($"{il.ResolveLocalName(array)}[{il.ResolveLocalName(index)}]")
                .LoadLocal(array)
                .LoadLocal(index);

            if (elementType.IsValueType)
            {
                return il.LoadElement(elementType);
            }
            else
            {
                return il.LoadElementReference(elementType);
            }
        }

        public static FluentIL ArrayCopy(
            this FluentIL il,
            Type elementType,
            Type arrayType,
            LocalBuilder array)
        {
            var temp = il.DeclareLocal(arrayType, "temp_arr");
            var n = il.DeclareLocal(typeof(int), "n");
            var arrayCopy = typeof(Array).GetMethod(nameof(Array.Copy), new[] { typeof(Array), typeof(Array), typeof(int), });

            il
                // Get the length of the array (n = src.Length);
                .Nop($"n = {array.LocalIndex}.Length")
                .LoadLocal(array)
                .LoadArrayLength()
                .ConvertToInt()
                .StoreLocal(n)
                // Create new destination array
                .Nop($"{temp.LocalIndex} = new {arrayType.Name}")
                .LoadLocal(n)
                .NewArray(elementType)
                .StoreLocal(temp)
                //Load relevant values for copy (src, dst, n)
                .Nop($"Array.Copy({array.LocalIndex},{temp.LocalIndex},{n.LocalIndex})")
                .LoadLocal(array)
                .LoadLocal(temp)
                .LoadLocal(n)
                .Call(arrayCopy)
                //Put the result on top of the stack
                .Nop("Putting Array.Copy result on top of the stack")
                .LoadLocal(temp)
                ;

            return il;
        }

        public static FluentIL For(
            this FluentIL il,
            int startIndex,
            int increments,
            LocalBuilder n,
            Action<FluentIL, LocalBuilder> body)
        {
            //
            var i = il.DeclareLocal(typeof(int), "i");

            //
            Label loopStart = il.DefineLabel("start");
            Label loopEnd = il.DefineLabel("end");

            // i = 0
            il
                .Nop($"i = {startIndex}")
                .LoadInt(startIndex)
                .StoreLocal(i);

            // Start
            il.Branch(loopEnd);
            il.MarkLabel(ref loopStart);

            //
            body(il, i);

            // Increment the index
            il.Nop($"i += {increments}");
            il.LoadLocal(i);
            il.LoadInt(increments);
            il.Add();
            il.StoreLocal(i);

            // Check if the index is less than the count
            il.MarkLabel(ref loopEnd);
            il.LoadLocal(i);
            il.LoadLocal(n);
            il.BranchLowerThan(loopStart);

            return il;
        }

        public static FluentIL ForEach(
            this FluentIL il,
            Type t,
            LocalBuilder src,
            Action<FluentIL, Action> body)
        {
            //
            var enumerableType = t.GetInterfaces().First(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            var getEnumerator = GetMethodFromInterface(t, enumerableType.GetMethod("GetEnumerator"));
            var moveNext = typeof(IEnumerator).GetMethod("MoveNext");
            var enumeratorType = getEnumerator.ReturnType;
            var getCurrent = enumeratorType.GetProperty("Current").GetMethod;
            var dispose = typeof(IDisposable).GetMethod("Dispose");

            //
            var checkLoopLabel = il.DefineLabel("enumerator_movenext");
            var bodyLabel = il.DefineLabel("enumerator_current");
            var enumNull = il.DefineLabel("enumerator_null");

            //
            var e = il.DeclareLocal(enumeratorType, "e");

            //
            return il
                //
                .LoadLocal(src)
                .Call(getEnumerator)
                .StoreLocal(e)
                //
                .Try(il =>
                {
                    il
                        .Branch(checkLoopLabel)
                        .MarkLabel(ref bodyLabel)
                        .Delegate(il => body(il, () => il.LoadLocal(e).Call(getCurrent)))
                        .MarkLabel(ref checkLoopLabel)
                        .LoadLocal(e)
                        .Call(moveNext)
                        .BranchIfTrue(bodyLabel);
                }, @finally: il =>
                {
                    il.LoadLocal(e)
                        .BranchIfFalse(enumNull)
                        .LoadLocal(e)
                        .Call(dispose)
                        .MarkLabel(ref enumNull)
                        .Nop();
                });


            MethodInfo GetMethodFromInterface(Type targetType, MethodInfo interfaceMethod)
            {
                var map = targetType.GetInterfaceMap(interfaceMethod.DeclaringType);
                var index = Array.IndexOf(map.InterfaceMethods, interfaceMethod);

                if (index == -1)
                {
                    //this should literally be impossible
                }

                return map.TargetMethods[index];
            }
        }
    }
}
