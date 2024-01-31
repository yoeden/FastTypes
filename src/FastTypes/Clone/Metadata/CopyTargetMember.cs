using System;
using System.Net;
using System.Reflection;
using System.Text;

namespace FastTypes.Clone.Metadata
{
    internal static class TypeExt
    {
        public static bool IsPureType<T>(this T value)
        {
            //Order by most used to least used
            if (value is null) return false;
            if (value is string) return true;
            if (value is int) return true;
            if (value is long) return true;
            if (value is DateTime) return true;
            if (value is Guid) return true;
            if (value is decimal) return true;
            //TODO: Add more

            return value.GetType().IsPureType();
        }

        public static bool IsPureType(this Type t)
        {
            if (t == typeof(string)) return true;
            if (t.IsPrimitive) return true;
            if (t.IsEnum) return true;
            if (!t.IsValueType) return false;
            if (t.IsArray) return false;

            //TODO: use fasttype evrerywhere in the copy api
            var fields = t.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (var f in fields)
            {
                if (!IsPureType(f.FieldType)) return false;
            }

            return true;
        }
    }


    internal abstract class CopyTargetMember
    {
        protected CopyTargetMember(Type type)
        {
            Type = type;
            IsPureValueType = type.IsPureType();
            IsValueType = type.IsValueType;
        }

        public abstract bool CanRead { get; }
        public abstract bool CanWrite { get; }

        public bool IsPureValueType { get; }
        public bool IsCircularDepdency { get; }
        public bool IsValueType { get; }
        public Type Type { get; }
    }
}
