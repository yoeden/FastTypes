using System.Collections;
using System.Collections.Generic;

namespace FastTypes.Query
{
    /// <summary>
    /// Represents a collection of query criterias.
    /// </summary>
    public sealed class QueryCriterias : IReadOnlyList<ITypeQueryCriteria>
    {
        private readonly IReadOnlyList<ITypeQueryCriteria> _criterias;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCriterias"/> class.
        /// </summary>
        /// <param name="criterias">The list of query criterias.</param>
        public QueryCriterias(IReadOnlyList<ITypeQueryCriteria> criterias)
        {
            _criterias = criterias;
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
        public IEnumerator<ITypeQueryCriteria> GetEnumerator() => _criterias.GetEnumerator();

        /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc cref="IReadOnlyCollection{T}.Count"/>
        public int Count => _criterias.Count;

        /// <inheritdoc cref="IReadOnlyList{T}.this"/>
        public ITypeQueryCriteria this[int index] => _criterias[index];
    }
}