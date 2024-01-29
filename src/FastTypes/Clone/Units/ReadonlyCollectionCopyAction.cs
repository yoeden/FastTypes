using System;
using System.Collections;
using System.Linq;
using System.Text;
using FastTypes.Clone.AnotherTake;
using FastTypes.Clone.Metadata;
using FastTypes.Compiler;

namespace FastTypes.Clone.Units
{
    internal sealed class ReadonlyCollectionCopyAction : DeepCopyAction
    {
        public ReadonlyCollectionCopyAction(IDeepCopyEvaluator evaluator) : base(evaluator)
        {
        }

        public override bool Copy(FluentIL il, Type type)
        {
            if (!CanHandleType(type)) return false;

            var target = CopyTarget.FromType(type);

            //
            var src = il.DeclareLocal(type);
            var dst = il.DeclareLocal(type);

            //
            var count = typeof(ICollection).GetProperty(nameof(ICollection.Count)).GetMethod;
            var capacityCtor = target.Constructors.FirstOrDefault(c => c.IsMatching<int>())?.Constructor;
            var defaultCtor = target.GetDefaultCtor();

            if (capacityCtor == null && defaultCtor == null) throw new NotImplementedException("");

            //
            il.StoreLocal(src);

            if (capacityCtor != null)
            {
                il
                    .LoadLocal(src)
                    .Call(count)
                    .New(capacityCtor)
                    .StoreLocal(dst);
            }
            else
            {
                il
                    .New(capacityCtor)
                    .StoreLocal(dst);
            }

            foreach (var field in target.Fields)
            {
                // Should remain on top of stack
                if (target.Type.IsValueType) il.LoadLocalAddress(dst);
                else il.LoadLocal(dst);

                //
                if (target.Type.IsValueType) il.LoadLocalAddress(src);
                else il.LoadLocal(src);

                //
                il.LoadField(field.Field);

                HandleField(field);

                //
                il.StoreField(field.Field);
            }

            return true;
        }

        private void HandleField(CopyTargetMember field)
        {
            Evaluate(field.Type);
        }

        private static bool CanHandleType(Type type)
        {
            return typeof(ICollection).IsAssignableFrom(type);
        }
    }
}
