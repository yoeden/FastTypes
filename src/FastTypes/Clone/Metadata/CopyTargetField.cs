using System.Reflection;
using System.Runtime.CompilerServices;

namespace FastTypes.Clone.Metadata
{
    internal sealed class CopyTargetField : CopyTargetMember
    {
        public CopyTargetField(FieldInfo field) : base(field.FieldType)
        {
            Field = field;
            IsBackingField = IsFieldInfoBackingField(field);
        }

        public FieldInfo Field { get; }
        public override bool CanRead => true;
        public override bool CanWrite => true;
        public bool IsBackingField { get; }

        public bool IsBackingFieldOf(PropertyInfo prop)
        {
            if (!IsBackingField) return false;

            return $"<{prop.Name}>k__BackingField".Equals(Field.Name);
        }

        public override string ToString()
        {
            return $"{(CanWrite ? "w" : "")}{(CanRead ? "r" : "")} {Field}";
        }

        private static bool IsFieldInfoBackingField(MemberInfo info) =>
            info.Name.EndsWith("k__BackingField") &&
            info.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
    }
}