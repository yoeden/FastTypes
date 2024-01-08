using System.Collections.Generic;
using System.Reflection;

namespace FastTypes.Query
{
    /// <summary>
    /// Represents a query snapshot of type queries.
    /// </summary>
    /// <remarks>
    /// This can be used to understand the query criterias, assembly targets and perform an assembly scan.
    /// </remarks>
    public sealed record TypeQuerySnapshot(IReadOnlyList<Assembly> Assemblies, IReadOnlyList<TypeQueryGroup> Groups);
}