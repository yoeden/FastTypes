using System.Collections.Generic;
using System.Reflection;

namespace FastTypes.Query
{
    public sealed record TypeQuerySnapshot(IReadOnlyList<Assembly> Assemblies, IReadOnlyList<TypeQueryGroup> Groups);
}