using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastTypes.Features.Query
{
    public static class TypeQueryScannerExt
    {
        public static ScanResult Scan(this ITypeQueryBuilderPreparation preparation)
        {
            var context = preparation.Prepare();

            if (context.Assemblies.Count == 1)
            {
                return new ScanResult(context.Assemblies, context.Criterias, ScanAssembly(context));
            }
            else
            {
                var types = ScanMultipleAssemblies(context);
                return new ScanResult(context.Assemblies, context.Criterias, types);
            }
        }

        public static IReadOnlyList<Type> FindTypes(this ITypeQueryBuilderPreparation preparation)
        {
            var context = preparation.Prepare();

            if (context.Assemblies.Count == 1)
            {
                return ScanAssembly(context);
            }
            else
            {
                return ScanMultipleAssemblies(context);
            }
        }

        private static IReadOnlyList<Type> ScanAssembly(TypeQueryContext context)
        {
            var result = new List<Type>();
            var assembly = context.Assemblies[0];
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (IsTypeMatchingCriterias(context.Criterias, type)) result.Add(type);
            }

            return result;
        }

        private static IReadOnlyList<Type> ScanMultipleAssemblies(TypeQueryContext context)
        {
            var concurrentBag = new ConcurrentBag<Type>();
            Parallel.ForEach(context.Assemblies, new ParallelOptions()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount / 2,
            }, assembly =>
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (IsTypeMatchingCriterias(context.Criterias, type)) concurrentBag.Add(type);
                }
            });

            return concurrentBag.ToList();
        }

        private static bool IsTypeMatchingCriterias(IReadOnlyList<ITypeQueryCriteria> criterias, Type t)
        {
            var isMatch = true;
            for (var i = 0; i < criterias.Count; i++)
            {
                var criteria = criterias[i];
                if (!criteria.IsMatching(t))
                {
                    isMatch = false;
                    break;
                }
            }

            return isMatch;
        }
    }
}