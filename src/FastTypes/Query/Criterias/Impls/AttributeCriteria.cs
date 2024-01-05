using System;
using System.Reflection;

namespace FastTypes.Query
{
    public sealed class AttributeCriteria : ITypeQueryCriteria
    {
        private readonly Type _attributeType;

        internal AttributeCriteria(Type attributeType)
        {
            if (attributeType == null) throw new ArgumentNullException(nameof(attributeType));
            if (!typeof(Attribute).IsAssignableFrom(attributeType)) throw new ArgumentException("Type isn't attribute.");

            _attributeType = attributeType;
        }

        public bool IsMatching(Type t)
        {
            return t.GetCustomAttribute(_attributeType) != null;
        }

        public int Priority => QueryCriteriaPriority.Low;
    }
}