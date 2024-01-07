using System;

namespace FastTypes.Query
{
    public sealed class PropertyOfTypeCriteria : ITypeQueryCriteria
    {
        private readonly Type _returnType;

        internal PropertyOfTypeCriteria(Type returnType)
        {
            _returnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
        }

        public bool IsMatching(Type t)
        {
            foreach (var property in t.GetProperties())
            {
                if (property.PropertyType == _returnType) return true;
            }

            return false;
        }

        public int Priority => QueryCriteriaPriority.Low;
    }
}