using System.Collections.Generic;
using System.Reflection;

namespace FastTypes.Query
{
    public sealed record TypeQueryContext(IReadOnlyList<Assembly> Assemblies, IReadOnlyList<ITypeQueryCriteria> Criterias);
}