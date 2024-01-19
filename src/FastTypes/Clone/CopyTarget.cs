using System;
using System.Collections.Generic;
using System.Reflection;
using FastTypes.Clone.Metadata;

namespace FastTypes.Clone
{
    internal sealed class CopyTarget
    {
        //TODO:
        // Nested targets
        // Property to indicate if can simply copy (pure structs, primitives, strings) or needed complex 
        // This class should be some sort of a tree that maps the given type to its composited types (including circular depdency to self).

        public static CopyTarget FromType(Type t)
        {
            var typeCtors = t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var typeFields = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var typeProps = t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            var ctor = (ConstructorInfo)null;

            //
            for (var i = 0; i < typeCtors.Length; i++)
            {
                var constructorInfo = typeCtors[i];
                if (constructorInfo.GetParameters().Length == 0)
                {
                    ctor = (constructorInfo);
                    break;
                }
            }
            if (ctor == null && t.IsClass) throw new InvalidOperationException("No empty constructor found.");

            //
            var fields = new List<CopyTargetField>(typeFields.Length);
            foreach (var field in typeFields)
            {
                fields.Add(new CopyTargetField(field));
            }

            //
            var properties = new List<CopyTargetProperty>(typeFields.Length);
            foreach (var property in typeProps)
            {
                properties.Add(new CopyTargetProperty(fields, property));
            }

            return new CopyTarget(t, ctor, fields, properties);
        }

        public CopyTarget(
            Type type,
            ConstructorInfo constructor,
            IReadOnlyList<CopyTargetField> fields,
            IReadOnlyList<CopyTargetProperty> properties
        )
        {
            Type = type;
            Constructor = constructor;
            Fields = fields;
            Properties = properties;
        }

        public Type Type { get; }
        public ConstructorInfo Constructor { get; }
        public IReadOnlyList<CopyTargetField> Fields { get; }
        public IReadOnlyList<CopyTargetProperty> Properties { get; }

        public CopyTargetField FindBackingField(PropertyInfo info)
        {
            if (info.DeclaringType != Type) throw new ArgumentException(nameof(info));

            foreach (var field in Fields)
            {
                if (field.Field.IsBackingFieldOf(info)) return field;
            }

            return null;
        }
    }
}