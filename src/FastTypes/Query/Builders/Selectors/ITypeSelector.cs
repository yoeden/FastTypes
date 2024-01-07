namespace FastTypes.Query
{
    public interface ITypeSelector
    {
        ITypeSelector Classes();
        ITypeSelector Interfaces();
        ITypeSelector ValueTypes();
        ITypeSelector Enums();
    }
}