using System;

namespace FastTypes.Query
{
    public interface ITypeQueryCriteria
    {
        bool IsMatching(Type t);

        int Priority => QueryCriteriaPriority.Mid;
    }

    public static class QueryCriteriaPriority
    {
        /// <summary>
        /// Light and general criterias.
        /// For example, Public or NorPublic criterias.
        /// </summary>
        public const int High = 100;

        /// <summary>
        /// 
        /// </summary>
        public const int Mid = 10000;

        /// <summary>
        /// Wasteful and time consuming criterias.
        /// For example, attribute criteria, should only be checked after all others since its wasteful.
        /// </summary>
        public const int Low = 1000000;
    }
}