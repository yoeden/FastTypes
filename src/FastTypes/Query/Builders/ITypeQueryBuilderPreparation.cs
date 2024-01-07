namespace FastTypes.Query
{
    public interface ITypeQueryBuilderPreparation : ITypeQueryBuilderAppend
    {
        TypeQuerySnapshot Prepare();
    }
}