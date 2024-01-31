using System;
using System.Reflection.Emit;
using FastTypes.Clone.Units;
using FastTypes.Compiler;

namespace FastTypes.Clone.AnotherTake
{
    internal sealed class CopyObjectByFields : DeepCopyAction
    {
        public CopyObjectByFields(IDeepCopyEvaluator evaluator) : base(evaluator)
        {

        }

        public override bool Copy(FluentIL il, Type type)
        {
            var target = CopyTarget.FromType(type);

            //
            var src = il.DeclareLocal(type);
            var dst = il.DeclareLocal(type);
            il.StoreLocal(src);

            // Create new Property Object and put on top of stack
            Init(target, dst, il);

            //
            CopyFields(il, target, src, dst, ignoreBackingFields: false);

            il.LoadLocal(dst);
            return true;
        }

        private void CopyFields(FluentIL il, CopyTarget target, LocalBuilder src, LocalBuilder dst, bool ignoreBackingFields = false)
        {
            foreach (var field in target.Fields)
            {
                //
                if (ignoreBackingFields && field.IsBackingField) continue;

                // Should remain on top of stack
                if (target.Type.IsValueType) il.LoadLocalAddress(dst);
                else il.LoadLocal(dst);

                //
                if (target.Type.IsValueType) il.LoadLocalAddress(src);
                else il.LoadLocal(src);

                //
                il.Nop($"{field.Field.DeclaringType.Name}.{field.Field.Name} = ");
                il.LoadField(field.Field);

                //
                Evaluate(field.Type);

                //
                il.StoreField(field.Field);
            }
        }

        private void CopyProperties(FluentIL il, CopyTarget target, LocalBuilder src, LocalBuilder dst, bool ignoreBackingFields = false)
        {
            foreach (var property in target.Properties)
            {
                if (!property.CanWrite) continue;
                if (!property.CanRead) continue;

                // Should remain on top of stack
                if (target.Type.IsValueType) il.LoadLocalAddress(dst);
                else il.LoadLocal(dst);

                //
                if (target.Type.IsValueType) il.LoadLocalAddress(src);
                else il.LoadLocal(src);

                //
                il.Call(property.GetMethod);

                //
                Evaluate(property.Type);

                //
                il.Call(property.SetMethod);
            }
        }

        private static void Init(CopyTarget target, LocalBuilder dst, FluentIL il)
        {
            // Init
            if (target.Type.IsValueType)
            {
                // Value type
                il.Nop($"{dst.LocalIndex} = new {target.Type.Name}()");
                il.LoadLocalAddress(dst);
                il.InitObject(dst.LocalType);
            }
            else if (target.Type.IsArray)
            {

            }
            else
            {
                if (target.GetDefaultCtor() == null) throw new NotImplementedException("No default constructor found, unable to create object");

                // Object
                il.Nop($"{dst.LocalIndex} = new {target.Type.Name}()");
                il.New(target.GetDefaultCtor());
                il.StoreLocal(dst);
            }
        }
    }

}