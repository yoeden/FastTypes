using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using FastTypes.Clone.Metadata;
using FastTypes.Clone.Units;

namespace FastTypes.Clone.AnotherTake
{
    [ExcludeFromCodeCoverage]
    internal sealed class SeenTypes
    {
        private readonly HashSet<Type> _seen = new HashSet<Type>();

        internal void MarkAsSeen(Type type) => _seen.Add(type);

        public bool Seen(Type t) => _seen.Contains(t);
    }

    internal static class CopyILEmitter
    {
        public static void EmitCopy(ILGenerator il, Type t)
        {
            var target = CopyTarget.FromType(t);
            var seenTypes = new SeenTypes();
            //il.Emit(target.Type.IsValueType ? OpCodes.Ldarga : OpCodes.Ldarg_S, 0);
            il.Emit(OpCodes.Ldarg_0);
            CopyComplexObject(il, seenTypes, target, t);
            il.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// This method expects the copyable value to be on top of the evaluation stack, and will set the new value on top instead.
        /// </summary>
        /// <param name="il"></param>
        /// <param name="seenTypes"></param>
        /// <param name="root">The type the initiated this whole copy process</param>
        /// <param name="type"></param>
        private static void Evaluate(
            ILGenerator il,
            SeenTypes seenTypes,
            CopyTarget root,
            Type type)
        {
            if (type.IsPureType())
            {
                //We dont mark pure type as seen

                // Nothing to do since setting the given value will copy it by the runtime
                return;
            }

            HandleNullCheck(il, type, () =>
            {
                if (seenTypes.Seen(type))
                {
                    var m = typeof(FastCopy<>).MakeGenericType(type).GetMethod(nameof(FastCopy<CopyTarget>.DeepCopy));
                    il.Emit(OpCodes.Call, m);
                }
                else if (type.IsArray)
                {
                    CopyArray(il, seenTypes, root, type);
                }
                else if (type.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IList<>)))
                {
                    CopyList(il, seenTypes, root, type);
                }
                //TODO: Add check for : IList, IEnumerable, IDictionary, Array.
                else
                {
                    //To avoid recursion calls and missing out on marking the seen, we mark it before
                    seenTypes.MarkAsSeen(type);
                    CopyComplexObject(il, seenTypes, root, type);
                }
            });
        }

        private static void CopyArray(ILGenerator il, SeenTypes seenTypes, CopyTarget root, Type t)
        {
            //
            var elementType = t.GetElementType();
            var arrayType = t;

            //
            var src = il.DeclareLocal(arrayType);
            var dst = il.DeclareLocal(arrayType);
            var i = il.DeclareLocal(typeof(int));
            var n = il.DeclareLocal(typeof(int));

            //
            Label loopStart = il.DefineLabel();
            Label loopEnd = il.DefineLabel();

            // Top of the stack contains the list
            il.Emit(OpCodes.Stloc_S, src);

            // Get the length of the array (n = src.Length);
            il.Emit(OpCodes.Ldloc_S, src);
            il.Emit(OpCodes.Ldlen);
            il.Emit(OpCodes.Conv_I4);
            il.Emit(OpCodes.Stloc_S, n);

            // Create new destination array
            il.Emit(OpCodes.Ldloc_S, n);
            il.Emit(OpCodes.Newarr, elementType);
            il.Emit(OpCodes.Stloc_S, dst);

            //
            if (elementType.IsPureType())
            {
                // Call array copy
                var arrayCopy = typeof(Array).GetMethod(nameof(Array.Copy), new[] { typeof(Array), typeof(Array), typeof(int), });

                il.Emit(OpCodes.Ldloc_S, src);
                il.Emit(OpCodes.Ldloc_S, dst);
                il.Emit(OpCodes.Ldloc_S, n);
                il.Emit(OpCodes.Call, arrayCopy);
                il.Emit(OpCodes.Ldloc_S, dst);

                return;
            }

            // i = 0
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Stloc_S, i);

            // Start
            il.Emit(OpCodes.Br, loopEnd);
            il.MarkLabel(loopStart);

            // Body
            il.Emit(OpCodes.Ldloc_S, dst);
            il.Emit(OpCodes.Ldloc_S, i);

            il.Emit(OpCodes.Ldloc_S, src);
            il.Emit(OpCodes.Ldloc_S, i);

            if (elementType.IsValueType)
            {
                il.Emit(OpCodes.Ldelem, elementType);

                Evaluate(il, seenTypes, root, elementType);

                il.Emit(OpCodes.Stelem, elementType);
            }
            else
            {
                il.Emit(OpCodes.Ldelem_Ref);

                Evaluate(il, seenTypes, root, elementType);

                il.Emit(OpCodes.Stelem_Ref);
            }

            // Increment the index
            il.Emit(OpCodes.Ldloc_S, i);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc_S, i);

            // Check if the index is less than the count
            il.MarkLabel(loopEnd);
            il.Emit(OpCodes.Ldloc_S, i);
            il.Emit(OpCodes.Ldloc_S, n);
            il.Emit(OpCodes.Blt, loopStart);

            //
            il.Emit(OpCodes.Ldloc_S, dst);
        }

        private static void CopyList(ILGenerator il, SeenTypes seenTypes, CopyTarget root, Type type)
        {
            //Top of the stack contains the list
            //By the end, the stack should have only the result

            var listUnderlyingType = type.GetGenericArguments()[0];

            //
            var src = il.DeclareLocal(type);
            var dst = il.DeclareLocal(type);
            var i = il.DeclareLocal(typeof(int));
            var n = il.DeclareLocal(typeof(int));

            //
            Label loopStart = il.DefineLabel();
            Label loopEnd = il.DefineLabel();

            //Top of the stack contains the list
            il.Emit(OpCodes.Stloc_S, src);

            // Call the Count property to get the number of elements in the list
            il.Emit(OpCodes.Ldloc_S, src);
            il.Emit(OpCodes.Callvirt, type.GetProperty("Count").GetGetMethod());
            il.Emit(OpCodes.Stloc_S, n);

            il.Emit(OpCodes.Ldloc_S, n);
            il.Emit(OpCodes.Newobj, type.GetConstructor(new Type[] { typeof(int) }));
            il.Emit(OpCodes.Stloc_S, dst);

            //
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Stloc_S, i);

            //
            il.Emit(OpCodes.Br, loopEnd);
            il.MarkLabel(loopStart);

            il.Emit(OpCodes.Ldloc_S, dst);
            il.Emit(OpCodes.Ldloc_S, src);
            il.Emit(OpCodes.Ldloc_S, i);
            il.Emit(OpCodes.Callvirt, type.GetMethod("get_Item"));

            //
            Evaluate(il, seenTypes, root, listUnderlyingType);

            il.Emit(OpCodes.Callvirt, type.GetMethod("Add"));

            // Increment the index
            il.Emit(OpCodes.Ldloc_S, i);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc_S, i);

            // Check if the index is less than the count
            il.MarkLabel(loopEnd);
            il.Emit(OpCodes.Ldloc_S, i);
            il.Emit(OpCodes.Ldloc_S, n);
            il.Emit(OpCodes.Blt, loopStart);

            //
            il.Emit(OpCodes.Ldloc_S, dst);
        }

        private static void CopyComplexObject(ILGenerator il, SeenTypes seenTypes, CopyTarget root, Type t)
        {
            var target = CopyTarget.FromType(t);

            //
            var src = il.DeclareLocal(t);
            var dst = il.DeclareLocal(t);
            il.Emit(OpCodes.Stloc_S, src);

            // Create new Property Object and put on top of stack
            CopyILHelper.Init(target, dst, il);

            //
            foreach (var property in target.Properties)
            {
                if (!property.CanWrite) continue;
                if (!property.CanRead) continue;

                // Should remain on top of stack
                il.Emit(target.Type.IsValueType ? OpCodes.Ldloca_S : OpCodes.Ldloc_S, dst);

                //
                il.Emit(target.Type.IsValueType ? OpCodes.Ldloca_S : OpCodes.Ldloc_S, src);
                CopyILHelper.GetValue(property, il);

                //
                Evaluate(il, seenTypes, root, property.Type);

                //
                CopyILHelper.SetValue(property, il);
            }

            //
            foreach (var field in target.Fields)
            {
                //
                if (field.IsBackingField) continue;

                // Should remain on top of stack
                il.Emit(target.Type.IsValueType ? OpCodes.Ldloca_S : OpCodes.Ldloc_S, dst);

                //
                il.Emit(target.Type.IsValueType ? OpCodes.Ldloca_S : OpCodes.Ldloc_S, src);
                CopyILHelper.GetValue(field, il);

                //
                Evaluate(il, seenTypes, root, field.Type);

                //
                CopyILHelper.SetValue(field, il);
            }

            il.Emit(OpCodes.Ldloc, dst);
        }

        private static void HandleNullCheck(ILGenerator il, Type t, Action notNullAction)
        {
            if (!t.IsValueType)
            {
                //
                var notnull = il.DefineLabel();
                var end = il.DefineLabel();

                // Checking if current instance is null
                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Brfalse, notnull);

                // If current instance is null return null
                il.Emit(OpCodes.Pop); // Get rid of the duplicated value
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Br, end);

                // If current instance isn't null
                il.MarkLabel(notnull);

                //HANDLE NOT NULL HERE
                notNullAction();

                // 
                il.MarkLabel(end);
            }
            else
            {
                notNullAction();
            }
        }
    }
}
