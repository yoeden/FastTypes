using System;

namespace FastTypes.Features.Query
{
    public sealed class MethodOfTypeCriteria : ITypeQueryCriteria
    {
        private readonly Type _returnType;

        internal MethodOfTypeCriteria(Type returnType)
        {
            _returnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
        }

        public bool IsMatching(Type t)
        {
            foreach (var method in t.GetMethods())
            {
                if (method.DeclaringType == typeof(object) || method.DeclaringType == typeof(ValueType)) continue;

                if (method.ReturnType == _returnType) return true;
            }

            return false;
        }

        public int Priority => QueryCriteriaPriority.Low;
    }
}