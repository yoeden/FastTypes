using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FastTypes.DataStructures;

namespace FastTypes.Query
{
    public static class TypeQueryScanner
    {
        private static readonly ConcurrentDictionary<string, Type[]> TypeCache = new();

        private static readonly ParallelOptions ParallelRunOptions =
            new()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount / 2
            };

        public static IReadOnlyList<IReadOnlyList<Type>> ScanForGroupedTypes(TypeQuerySnapshot snapshot)
        {
            var result = new List<LockableList<Type>>();
            var groups = snapshot.Groups;
            var assemblies = snapshot.Assemblies;

            for (var i = 0; i < groups.Count; i++)
            {
                result.Add(new LockableList<Type>());
            }

            if (snapshot.Assemblies.Count == 1)
            {
                var types = GetAssemblyTypes(assemblies[0]);

                for (var i = 0; i < groups.Count; i++)
                {
                    var group = groups[i];
                    FillListWithMatchingTypes(types, group, result[i]);
                }
            }

            Parallel.ForEach(assemblies, ParallelRunOptions, assembly =>
            {
                var types = GetAssemblyTypes(assembly);
                var t = new List<Type>();

                for (var i = 0; i < groups.Count; i++)
                {
                    var group = groups[i];
                    FillListWithMatchingTypes(types, group, t);

                    result[i].AddRange(t);
                    t.Clear();
                }
            });

            return result;
        }

        public static IReadOnlyList<Type> ScanForTypes(TypeQuerySnapshot snapshot)
        {
            var result = new LockableList<Type>();
            var groups = snapshot.Groups;
            var assemblies = snapshot.Assemblies;

            if (assemblies.Count == 1)
            {
                var types = GetAssemblyTypes(assemblies[0]);

                for (var i = 0; i < groups.Count; i++)
                {
                    var group = groups[i];
                    FillListWithMatchingTypes(types, group, result);
                }

                return result;
            }

            Parallel.ForEach(assemblies, ParallelRunOptions, assembly =>
            {
                var types = GetAssemblyTypes(assembly);
                var t = new List<Type>();

                for (var i = 0; i < groups.Count; i++)
                {
                    var group = groups[i];
                    FillListWithMatchingTypes(types, group, t);

                    result.AddRange(t);
                    t.Clear();
                }
            });

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void FillListWithMatchingTypes(IReadOnlyList<Type> types, TypeQueryGroup group, ICollection<Type> t)
        {
            for (var i = 0; i < types.Count; i++)
            {
                Type type = types[i];
                if (IsTypeMatchingCriterias(group.Criterias, type)) t.Add(type);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Type[] GetAssemblyTypes(Assembly assembly)
        {
            return !assembly.IsDynamic ? TypeCache.GetOrAdd(assembly.FullName, s => assembly.GetTypes()) : assembly.GetTypes();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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