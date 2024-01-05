using System;

namespace FastTypes.Features.Query
{
    public interface ITypeQueryBuilderTypes
    {
        ITypeQueryBuilderModifiers Targeting(Action<ITypeSelector> types);
    }
}