using System;
using System.Collections.Generic;
using System.Reflection;

namespace FastTypes.Clone.Metadata
{
    internal sealed class CopyTargetCtor : CopyTargetMember
    {
        private readonly ParameterInfo[] _parameters;

        public CopyTargetCtor(ConstructorInfo constructor) : base(constructor.DeclaringType)
        {
            Constructor = constructor;
            _parameters = constructor.GetParameters();
        }

        public ConstructorInfo Constructor { get; }
        public override bool CanRead => true;
        public override bool CanWrite => true;
        public bool IsDefault => _parameters.Length == 0;

        public bool IsMatching(Type t1)
        {
            if (_parameters.Length != 1) return false;
            if (_parameters[0].ParameterType != t1) return false;

            return true;
        }

        public bool IsMatching<T1>() => IsMatching(typeof(T1));

        public bool IsMatching<T1, T2>()
        {
            if (_parameters.Length != 1) return false;
            if (_parameters[0].ParameterType != typeof(T1)) return false;
            if (_parameters[1].ParameterType != typeof(T2)) return false;

            return true;
        }
    }

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
                if (field.IsBackingFieldOf(info)) return field;
            }

            return null;
        }

        public override string ToString()
        {
            return $"{(CanWrite ? "w" : "")}{(CanRead ? "r" : "")} {Property}";
        }
    }
}