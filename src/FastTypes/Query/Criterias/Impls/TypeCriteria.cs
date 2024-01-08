using System;

namespace FastTypes.Query
{
    /// <summary>
    /// Criteria to check the type.
    /// </summary>
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

        /// <inheritdoc cref="IsMatching"/>
        public bool IsMatching(Type t)
        {
            if (t.IsEnum && _isEnum) return true;
            if (t.IsClass && _isClass) return true;
            if (t.IsValueType && !t.IsEnum && _isValueType) return true;
            if (t.IsInterface && _isInterface) return true;

            return false;
        }

        /// <inheritdoc cref="Priority"/>
        public int Priority => QueryCriteriaPriority.High;

        /// <summary>
        /// Gets a value indicating whether the object is a class.
        /// </summary>
        public bool IsClass => _isClass;

        /// <summary>
        /// Gets a value indicating whether the object is a value type.
        /// </summary>
        public bool IsValueType => _isValueType;

        /// <summary>
        /// Gets a value indicating whether the object is an interface.
        /// </summary>
        public bool IsInterface => _isInterface;

        /// <summary>
        /// Gets a value indicating whether the object is an enumeration.
        /// </summary>
        public bool IsEnum => _isEnum;
    }
}