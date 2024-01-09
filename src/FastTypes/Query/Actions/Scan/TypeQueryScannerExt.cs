using System;
using System.Collections.Generic;

namespace FastTypes.Query
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeQueryScannerExt
    {
        /// <summary>
        /// Finds types based on the prepared query builder.
        /// </summary>
        /// <param name="preparation">The prepared query builder.</param>
        /// <returns>A read-only list of types.</returns>
        public static IReadOnlyList<Type> FindTypes(this ITypeQueryBuilderPreparation preparation)
        {
            var snapshot = preparation.Snapshot();
            return FindTypes(snapshot);
        }

        /// <summary>
        /// Finds types based on the prepared query builder.
        /// </summary>
        /// <param name="snapshot">The query snapshot to apply the scan on.</param>
        /// <returns>A read-only list of types.</returns>
        public static IReadOnlyList<Type> FindTypes(this TypeQuerySnapshot snapshot)
        {
            return TypeQueryScanner.ScanForTypes(snapshot);
        }

        /// <summary>
        /// Finds grouped types based on the prepared query builder.
        /// </summary>
        /// <param name="preparation">The prepared query builder.</param>
        /// <returns>A read-only list of read-only lists of types.</returns>
        public static IReadOnlyList<IReadOnlyList<Type>> FindGroupedTypes(this ITypeQueryBuilderPreparation preparation)
        {
            var snapshot = preparation.Snapshot();
            return FindGroupedTypes(snapshot);
        }

        /// <summary>
        /// Finds grouped types based on the prepared query builder.
        /// </summary>
        /// <param name="snapshot">The query snapshot to apply the scan on.</param>
        /// <returns>A read-only list of read-only lists of types.</returns>
        public static IReadOnlyList<IReadOnlyList<Type>> FindGroupedTypes(this TypeQuerySnapshot snapshot)
        {
            return TypeQueryScanner.ScanForGroupedTypes(snapshot);
        }
    }
}