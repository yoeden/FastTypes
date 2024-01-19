using System.Collections.Generic;
using System.Reflection;

namespace FastTypes.Clone.Metadata
{
    internal sealed class CopyTargetProperty : CopyTargetMember
    {
        public CopyTargetProperty(IReadOnlyList<CopyTargetField> fields, PropertyInfo property)
            : base(property.PropertyType)
        {
            Property = property;
            BackingField = FindBackingField(fields, property);

            CanRead = property.GetMethod != null || BackingField != null;
            CanWrite = property.SetMethod != null || BackingField != null;
        }

        public PropertyInfo Property { get; }
        public CopyTargetField BackingField { get; }
        public override bool CanRead { get; }
        public override bool CanWrite { get; }
        public MethodInfo GetMethod => Property.GetMethod;
        public MethodInfo SetMethod => Property.SetMethod;

        private CopyTargetField FindBackingField(IReadOnlyList<CopyTargetField> fields, PropertyInfo info)
        {
            for (var i = 0; i < fields.Count; i++)
            {
                var field = fields[i];
                if (field.Field.IsBackingFieldOf(info)) return field;
            }

            return null;
        }
    }
}