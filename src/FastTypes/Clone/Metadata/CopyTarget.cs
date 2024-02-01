using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

            //
            var ctors = new List<CopyTargetCtor>(typeCtors.Length);
            foreach (var ctor in typeCtors)
            {
                ctors.Add(new CopyTargetCtor(ctor));
            }

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

            return new CopyTarget(t, ctors, fields, properties);
        }

        public CopyTarget(Type type, IReadOnlyList<CopyTargetCtor> constructors, IReadOnlyList<CopyTargetField> fields, IReadOnlyList<CopyTargetProperty> properties)
        {
            Type = type;
            Constructors = constructors;
            Fields = fields;
            Properties = properties;
        }

        public Type Type { get; }
        public IReadOnlyList<CopyTargetCtor> Constructors { get; }
        public IReadOnlyList<CopyTargetField> Fields { get; }
        public IReadOnlyList<CopyTargetProperty> Properties { get; }

        public ConstructorInfo GetDefaultCtor() => Constructors.FirstOrDefault(c => c.IsDefault)?.Constructor;

        public override string ToString()
        {
            return $"{Type.Name} ({Constructors.Count} Ctors, {Fields.Count} Fields, {Properties.Count} Properties)";
        }
    }
}