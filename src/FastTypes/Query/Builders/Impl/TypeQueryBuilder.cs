using System;
using System.Collections.Generic;
using System.Reflection;

namespace FastTypes.Query
{
    internal sealed partial class TypeQueryBuilder : 
        ITypeQueryBuilderAssembly, 
        ITypeQueryBuilderTarget, 
        ITypeQueryBuilderCriterias
    {
        private readonly List<Assembly> _assemblies = new();
        private readonly List<TypeQueryGroup> _groups = new();

        private readonly List<ITypeQueryCriteria> _currentCriteriasScope = new();
        private readonly Dictionary<Type, object> _tags = new();
        private bool _isNotPublicSet;

        public TypeQuerySnapshot Snapshot()
        {
            SealCurrentScope();

            return new TypeQuerySnapshot(_assemblies.AsReadOnly(), _groups.AsReadOnly());
        }

        public ITypeQueryBuilderTarget And()
        {
            SealCurrentScope();

            _currentCriteriasScope.Clear();
            _tags.Clear();

            return this;
        }

        private void SealCurrentScope()
        {
            if (!_isNotPublicSet) _currentCriteriasScope.Add(new AccessModifierCriteria(isPublic: true));

            _isNotPublicSet = false;

            var criterias = new List<ITypeQueryCriteria>(_currentCriteriasScope.Count);
            criterias.AddRange(_currentCriteriasScope);
            criterias.TrimExcess();

            var tags = new Dictionary<Type, object>(_tags);
            tags.TrimExcess();

            _groups.Add(new TypeQueryGroup(new QueryCriterias(criterias), QueryTags.FromDictionary(tags)));
        }
    }
}