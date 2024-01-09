namespace FastTypes.Query
{
    /// <summary>
    /// Creates a new query group
    /// </summary>
    public interface ITypeQueryBuilderAppend
    {
        /// <summary>
        /// Sealing the current query in a group, and creating a new query from the current assembly configurations.
        /// </summary>
        /// <returns>A new instance of the query builder.</returns>
        ITypeQueryBuilderTarget And();
    }
}