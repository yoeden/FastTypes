using System;
using System.Collections.Generic;
using System.Reflection;

namespace FastTypes.Query
{
    public sealed record ScanResult(IReadOnlyList<Assembly> Assemblies, IReadOnlyList<ITypeQueryCriteria> Criterias, IReadOnlyList<Type> Types);
}