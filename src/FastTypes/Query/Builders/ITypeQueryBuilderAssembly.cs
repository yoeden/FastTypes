using System;
using System.Reflection;

namespace FastTypes.Query
{
    /// <summary>
    /// Allows the user to select from which assemblies he wants to apply to scan on.
    /// The entry point of the query API.
    /// </summary>
    public interface ITypeQueryBuilderAssembly
    {
        ITypeQueryBuilderTargets FromAssembly(Assembly assembly);

        ITypeQueryBuilderTargets FromAssemblies(params Assembly[] assemblies);

        ITypeQueryBuilderTargets FromAllAssemblies();

        ITypeQueryBuilderTargets AssemblyOfType<T>();

        ITypeQueryBuilderTargets AssemblyOfType(Type t);
    }
}