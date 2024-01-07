using System.Collections;
using System.Collections.Generic;

namespace FastTypes.Query
{
    public sealed class QueryCriterias : IReadOnlyList<ITypeQueryCriteria>
    {
        private readonly IReadOnlyList<ITypeQueryCriteria> _criterias;

        public QueryCriterias(IReadOnlyList<ITypeQueryCriteria> criterias)
        {
            _criterias = criterias;
        }

        public IEnumerator<ITypeQueryCriteria> GetEnumerator() => _criterias.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _criterias.Count;

        public ITypeQueryCriteria this[int index] => _criterias[index];
    }
}