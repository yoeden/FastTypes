using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastTypes.Query
{
    public static class TypeQueryScanner
    {
        //TODO: Create an IReadOnlyList array proxy class and use List from object pool
        public static IReadOnlyList<IReadOnlyList<Type>> ScanForGroupedTypes(TypeQuerySnapshot snapshot)
        {
            var result = new List<ConcurrentBag<Type>>();
            var groups = snapshot.Groups;
            var assemblies = snapshot.Assemblies;

            for (var i = 0; i < groups.Count; i++)
            {
                result.Add(new ConcurrentBag<Type>());
            }

            Parallel.ForEach(assemblies, new ParallelOptions()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount / 2
            }, assembly =>
            {
                var types = assembly.GetTypes();
                var t = new List<Type>();

                for (var i = 0; i < groups.Count; i++)
                {
                    var group = groups[i];
                    foreach (Type type in types)
                    {
                        if (IsTypeMatchingCriterias(group.Criterias, type)) t.Add(type);
                    }

                    if (t.Count != 0)
                    {
                        for (var i1 = 0; i1 < t.Count; i1++)
                        {
                            result[i].Add(t[i1]);
                        }
                    }
                    t.Clear();
                }
            });

            return result.Select(bag => new List<Type>(bag)).ToArray();
        }

        public static IReadOnlyList<Type> ScanForTypes(TypeQuerySnapshot snapshot)
        {
            var result = new ConcurrentBag<Type>();
            var groups = snapshot.Groups;
            var assemblies = snapshot.Assemblies;

            Parallel.ForEach(assemblies, new ParallelOptions()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount / 2
            }, assembly =>
            {
                var types = assembly.GetTypes();
                var t = new List<Type>();

                for (var i = 0; i < groups.Count; i++)
                {
                    var group = groups[i];
                    foreach (Type type in types)
                    {
                        if (IsTypeMatchingCriterias(group.Criterias, type)) t.Add(type);
                    }

                    if (t.Count != 0)
                    {
                        for (var i1 = 0; i1 < t.Count; i1++)
                        {
                            result.Add(t[i1]);
                        }
                    }
                    t.Clear();
                }
            });

            return new List<Type>(result);
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