using System;

namespace FastTypes.Query
{
    /// <summary>
    /// Represents a criteria for querying types.
    /// </summary>
    public interface ITypeQueryCriteria
    {
        /// <summary>
        /// Checks if the given type matches the criteria.
        /// </summary>
        /// <param name="t">The type to check.</param>
        /// <returns>True if the type matches the criteria, otherwise false.</returns>
        bool IsMatching(Type t);

        /// <summary>
        /// Gets the priority of the criteria.
        /// Use <see cref="QueryCriteriaPriority"/> .
        /// </summary>
        int Priority { get; }
    }
}