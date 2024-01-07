using System;

namespace FastTypes.Query
{
    public sealed class AccessModifierCriteria : ITypeQueryCriteria
    {
        private readonly bool _isPublic;

        internal AccessModifierCriteria(bool isPublic = true)
        {
            _isPublic = isPublic;
        }

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
    }
}