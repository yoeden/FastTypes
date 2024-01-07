using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FastTypes.Query
{
    public static class TypeQueryScannerExt
    {
        public static IReadOnlyList<Type> FindTypes(this ITypeQueryBuilderPreparation preparation)
        {
            var snapshot = preparation.Prepare();
            return TypeQueryScanner.ScanForTypes(snapshot);
        }

        public static IReadOnlyList<IReadOnlyList<Type>> FindGroupedTypes(this ITypeQueryBuilderPreparation preparation)
        {
            var snapshot = preparation.Prepare();
            return TypeQueryScanner.ScanForGroupedTypes(snapshot);
        }
    }
}