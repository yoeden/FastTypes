using System;

namespace FastTypes.Query
{
    /// <summary>
    /// Criteria to check if the type contains a method that returns a given type.
    /// </summary>
    public sealed class MethodOfTypeCriteria : ITypeQueryCriteria
    {
        private readonly Type _returnType;

        internal MethodOfTypeCriteria(Type returnType)
        {
            _returnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
        }

        /// <inheritdoc cref="IsMatching"/>
        public bool IsMatching(Type t)
        {
            foreach (var method in t.GetMethods())
            {
                if (method.DeclaringType == typeof(object) || method.DeclaringType == typeof(ValueType)) continue;

                if (method.ReturnType == _returnType) return true;
            }

            return false;
        }

        /// <inheritdoc cref="Priority"/>
        public int Priority => QueryCriteriaPriority.Low;
    }
}