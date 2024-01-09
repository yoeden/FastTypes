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
        /// <summary>
        /// Specifies a single assembly to scan on.
        /// </summary>
        /// <param name="assembly">The assembly to scan on.</param>
        /// <returns>A type query builder for selecting targets.</returns>
        ITypeQueryBuilderTarget FromAssembly(Assembly assembly);

        /// <summary>
        /// Specifies multiple assemblies to scan on.
        /// </summary>
        /// <param name="assemblies">The assemblies to scan on.</param>
        /// <returns>A type query builder for selecting targets.</returns>
        ITypeQueryBuilderTarget FromAssemblies(params Assembly[] assemblies);

        /// <summary>
        /// Specifies to scan on executing assembly (will be FastType assembly).
        /// </summary>
        ITypeQueryBuilderTarget FromExecutingAssembly();

        /// <summary>
        /// Specifies to scan on calling assembly.
        /// </summary>
        ITypeQueryBuilderTarget FromCallingAssembly();

        /// <summary>
        /// Specifies to scan on entry assembly.
        /// </summary>
        ITypeQueryBuilderTarget FromEntryAssembly();

        /// <summary>
        /// Specifies to scan on all available assemblies.
        /// </summary>
        /// <returns>A type query builder for selecting targets.</returns>
        ITypeQueryBuilderTarget FromAllAssemblies();

        /// <summary>
        /// Specifies to scan on the assembly containing a specific type.
        /// </summary>
        /// <typeparam name="T">The type contained in the assembly to scan on.</typeparam>
        /// <returns>A type query builder for selecting targets.</returns>
        ITypeQueryBuilderTarget AssemblyContaining<T>();

        /// <summary>
        /// Specifies to scan on the assembly containing a specific type.
        /// </summary>
        /// <param name="t">The type contained in the assembly to scan on.</param>
        /// <returns>A type query builder for selecting targets.</returns>
        ITypeQueryBuilderTarget AssemblyContaining(Type t);

    }
}