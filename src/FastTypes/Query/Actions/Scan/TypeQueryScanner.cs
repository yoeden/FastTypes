using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FastTypes.DataStructures;

namespace FastTypes.Query
{
    /// <summary>
    /// Assembly scanner based on a given query snapshot.
    /// </summary>
    internal static class TypeQueryScanner
    {
        // Cache to store types per assembly
        private static readonly ConcurrentDictionary<string, Type[]> TypeCache = new();

        // Options for parallel execution
        private static readonly ParallelOptions ParallelRunOptions =
            new()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount / 2
            };

        /// <summary>
        /// Scanning an assemblies from <see cref="TypeQuerySnapshot.Assemblies"/> and grouping the result based on <see cref="TypeQuerySnapshot.Groups"/>
        /// </summary>
        /// <param name="snapshot">The query snapshot containing the groups and assemblies to scan.</param>
        /// <returns>A list of grouped types.</returns>
        /// <remarks>
        /// Since each group is treated as its own unit, duplicate types may occur between groups (Group A and B may contain the same type).
        /// </remarks>
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

        /// <summary>
        /// Scanning an assemblies from <see cref="TypeQuerySnapshot.Assemblies"/> and aggregating the result based on <see cref="TypeQuerySnapshot.Groups"/>
        /// </summary>
        /// <param name="snapshot">The query snapshot containing the groups and assemblies to scan.</param>
        /// <returns>A list of types</returns>
        /// <remarks>
        /// The returned list is promised to be unique even if groups will duplicate the same result
        /// </remarks>
        public static IReadOnlyList<Type> ScanForTypes(TypeQuerySnapshot snapshot)
        {
            var result = new LockableSet<Type>();
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