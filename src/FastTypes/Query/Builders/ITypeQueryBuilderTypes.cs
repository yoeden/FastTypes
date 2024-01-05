using System;

namespace FastTypes.Query
{
    public interface ITypeQueryBuilderTypes
    {
        ITypeQueryBuilderModifiers Targeting(Action<ITypeSelector> types);
    }
}