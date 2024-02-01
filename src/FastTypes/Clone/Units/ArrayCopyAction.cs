using System;
using FastTypes.Compiler;

namespace FastTypes.Clone
{
    internal sealed class ArrayCopyAction : DeepCopyAction
    {
        public ArrayCopyAction(IDeepCopyEvaluator evaluator) : base(evaluator)
        {
        }

        public override bool Copy(FluentIL il, Type t)
        {
            if (!t.IsArray) return false;

            //
            var elementType = t.GetElementType();
            var arrayType = t;

            //
            var src = il.DeclareLocal(arrayType, "src");

            // Top of the stack contains the list
            il.Nop($"Array copy for <{elementType.Name}>");
            il.StoreLocal(src);

            if (elementType.IsPureType())
            {
                il.ArrayCopy(elementType, arrayType, src);
            }
            else
            {
                var n = il.DeclareLocal(typeof(int), "n");
                var dst = il.DeclareLocal(arrayType, "dst");

                // Get the length of the array (n = src.Length);
                il.Nop($"n = {src.LocalIndex}.Length")
                    .LoadLocal(src)
                    .LoadArrayLength()
                    .ConvertToInt()
                    .StoreLocal(n);

                // Create new destination array
                il.Nop($"{dst.LocalIndex} = new {arrayType.Name}");
                il.LoadLocal(n);
                il.NewArray(elementType);
                il.StoreLocal(dst);

                il.For(0, 1, n, (_, i) =>
                {
                    // Body
                    il.LoadLocal(dst);
                    il.LoadLocal(i);

                    il.LoadLocal(src);
                    il.LoadLocal(i);

                    if (elementType.IsValueType)
                    {
                        il.LoadElement(elementType);

                        Evaluate(elementType);

                        il.StoreElement(elementType);
                    }
                    else
                    {
                        il.LoadElementReference(elementType);

                        Evaluate(elementType);

                        il.StoreElementReference();
                    }
                });

                //
                il.LoadLocal(dst);
            }

            return true;

        }

    }

}