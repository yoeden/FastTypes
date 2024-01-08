namespace FastTypes.Query
{
    /// <summary>
    /// Represents a group of query criteria and tags for a type query.
    /// </summary>
    public sealed record TypeQueryGroup(QueryCriterias Criterias, QueryTags Tags);
}