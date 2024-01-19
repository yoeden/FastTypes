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

        private static bool IsFieldInfoBackingField(FieldInfo info) =>
            info.Name.EndsWith("k__BackingField") &&
            info.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
    }
}