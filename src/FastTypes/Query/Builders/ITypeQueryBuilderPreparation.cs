namespace FastTypes.Query
{
    /// <summary>
    /// Allows the user to prepare the query for execution by using snapshots
    /// </summary>
    public interface ITypeQueryBuilderPreparation : ITypeQueryBuilderAppend
    {
        /// <summary>
        /// Grabs a snapshot of the current query builder
        /// </summary>
        /// <returns></returns>
        TypeQuerySnapshot Snapshot();
    }
}