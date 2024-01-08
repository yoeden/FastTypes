using System;
using System.Reflection;

namespace FastTypes.Query
{
    /// <summary>
    /// Criteria to check if the type contains a given attribute.
    /// </summary>
    public sealed class AttributeCriteria : ITypeQueryCriteria
    {
        private readonly Type _attributeType;

        internal AttributeCriteria(Type attributeType)
        {
            if (attributeType == null) throw new ArgumentNullException(nameof(attributeType));
            if (!typeof(Attribute).IsAssignableFrom(attributeType)) throw new ArgumentException("Type isn't attribute.");

            _attributeType = attributeType;
        }

        /// <inheritdoc cref="IsMatching"/>
        public bool IsMatching(Type t)
        {
            return t.GetCustomAttribute(_attributeType) != null;
        }

        /// <inheritdoc cref="Priority"/>
        public int Priority => QueryCriteriaPriority.Low;
    }
}