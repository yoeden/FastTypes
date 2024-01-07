using System;

namespace FastTypes.Query
{
    public sealed class TypeCriteria : ITypeQueryCriteria
    {
        private readonly bool _isClass;
        private readonly bool _isValueType;
        private readonly bool _isInterface;
        private readonly bool _isEnum;

        internal TypeCriteria(bool isClass, bool isValueType, bool isInterface, bool isEnum)
        {
            if (!isClass && !isValueType && !isInterface && !isEnum) throw new ArgumentException();

            _isClass = isClass;
            _isValueType = isValueType;
            _isInterface = isInterface;
            _isEnum = isEnum;
        }

        public bool IsMatching(Type t)
        {
            if (t.IsEnum && _isEnum) return true;
            if (t.IsClass && _isClass) return true;
            if (t.IsValueType && !t.IsEnum && _isValueType) return true;
            if (t.IsInterface && _isInterface) return true;

            return false;
        }

        public bool IsClass => _isClass;
        public bool IsValueType => _isValueType;
        public bool IsInterface => _isInterface;
        public bool IsEnum => _isEnum;
    }
}