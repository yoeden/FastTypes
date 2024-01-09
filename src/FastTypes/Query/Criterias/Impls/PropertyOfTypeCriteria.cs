using System;

namespace FastTypes.Query
{
    /// <summary>
    /// Criteria to check if the type contains a property that returns a given type.
    /// </summary>
    public sealed class PropertyOfTypeCriteria : ITypeQueryCriteria
    {
        private readonly Type _returnType;

        internal PropertyOfTypeCriteria(Type returnType)
        {
            _returnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
        }

        /// <inheritdoc cref="IsMatching"/>
        public bool IsMatching(Type t)
        {
            foreach (var property in t.GetProperties())
            {
                if (property.PropertyType == _returnType) return true;
            }

            return false;
        }

        /// <inheritdoc cref="Priority"/>
        public int Priority => QueryCriteriaPriority.Low;
    }
}