using System;

namespace FastTypes.Query
{
    /// <summary>
    /// Criteria to check if the type is assignable to another given type.
    /// Generics supported.
    /// </summary>
    public sealed class AssignableToCriteria : ITypeQueryCriteria
    {
        private readonly Type _target;

        internal AssignableToCriteria(Type target)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <inheritdoc cref="Priority"/>
        public int Priority => QueryCriteriaPriority.Mid;

        /// <inheritdoc cref="IsMatching"/>
        public bool IsMatching(Type t)
        {
            //TODO: Add unit test for this
            if (t == _target)
            {
                return true;
            }
            else if (_target.ContainsGenericParameters)
            {
                if (_target.IsClass)
                {
                    return CheckIfGenericBase(t);
                }
                else
                {
                    return IsAssignableFromGenericInterface(t);
                }

            }
            else
            {
                if (_target.IsClass)
                {
                    return CheckIfBase(t);
                }

                //Interface
                return _target.IsAssignableFrom(t);
            }
        }

        private bool IsAssignableFromGenericInterface(Type t)
        {
            var interfaceTypes = t.GetInterfaces();
            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == _target)
                    return true;
            }

            return false;
        }

        private bool CheckIfBase(Type source)
        {
            while (true)
            {
                if (source.BaseType == null) return false;
                if (source.BaseType == _target) return true;
                source = source.BaseType;
            }
        }

        private bool CheckIfGenericBase(Type source)
        {
            while (true)
            {
                if (source.BaseType == null) return false;
                if (source.BaseType.IsGenericType && source.BaseType.GetGenericTypeDefinition() == _target) return true;
                source = source.BaseType;
            }
        }
    }
}