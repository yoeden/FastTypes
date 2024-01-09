using System;

namespace FastTypes.Query
{
    /// <summary>
    /// Criteria to check if the type is either public or non-public (internal, protected, private).
    /// </summary>
    public sealed class AccessModifierCriteria : ITypeQueryCriteria
    {
        private readonly bool _isPublic;

        internal AccessModifierCriteria(bool isPublic = true)
        {
            _isPublic = isPublic;
        }

        /// <inheritdoc cref="IsMatching"/>
        public bool IsMatching(Type t)
        {
            if (!t.IsNested)
            {
                return _isPublic ? t.IsPublic : !t.IsPublic;
            }
            else
            {
                return _isPublic ? t.IsNestedPublic : !t.IsNestedPublic;

            }
        }

        /// <inheritdoc cref="Priority"/>
        public int Priority => QueryCriteriaPriority.High;
    }
}