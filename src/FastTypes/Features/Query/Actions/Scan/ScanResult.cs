using System;
using System.Collections.Generic;
using System.Reflection;

namespace FastTypes.Features.Query
{
    public sealed record ScanResult(IReadOnlyList<Assembly> Assemblies, IReadOnlyList<ITypeQueryCriteria> Criterias, IReadOnlyList<Type> Types);
}