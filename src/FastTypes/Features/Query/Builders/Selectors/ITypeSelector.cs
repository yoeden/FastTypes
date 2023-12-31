namespace FastTypes.Features.Query
{
    public interface ITypeSelector
    {
        ITypeSelector Classes();
        ITypeSelector Interfaces();
        ITypeSelector ValueTypes();
        ITypeSelector Enums();
    }
}