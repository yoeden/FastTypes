using System;
using System.Reflection.Emit;
using FastTypes.Clone.Metadata;

namespace FastTypes.Clone.Units
{
    internal static class CopyILHelper
    {
        public static void Init(CopyTarget target, LocalBuilder dst, ILGenerator il)
        {
            // Init
            if (target.Type.IsValueType)
            {
                // Value type
                il.Emit(OpCodes.Ldloca_S, dst);
                il.Emit(OpCodes.Initobj, dst.LocalType);
            }
            else
            {
                // Object
                il.Emit(OpCodes.Newobj, target.Constructor);
                il.Emit(OpCodes.Stloc_S, dst);
            }
        }

        public static void GetValue(CopyTargetMember member, ILGenerator il)
        {
            if (member is CopyTargetField field)
            {
                il.Emit(OpCodes.Ldfld, field.Field);
            }
            else if (member is CopyTargetProperty property)
            {
                //Always prefer calling get
                if (property.GetMethod != null)
                {
                    //TODO: Check perfomance of callvirt (IsVirtual property) and call
                    il.Emit(OpCodes.Callvirt, property.GetMethod);
                }
                else
                {
                    il.Emit(OpCodes.Ldfld, property.BackingField.Field);
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static void SetValue(CopyTargetMember member, ILGenerator il)
        {
            if (member is CopyTargetField field)
            {
                il.Emit(OpCodes.Stfld, field.Field);
            }
            else if (member is CopyTargetProperty property)
            {
                //Always prefer calling get
                if (property.SetMethod != null)
                {
                    il.Emit(OpCodes.Callvirt, property.SetMethod);
                }
                else
                {
                    il.Emit(OpCodes.Stfld, property.BackingField.Field);
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}