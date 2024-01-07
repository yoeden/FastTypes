using System;

namespace FastTypes.Query
{
    public interface ITypeQueryBuilderTargets
    {
        ITypeQueryBuilderModifiers Targeting(Action<ITypeSelector> types);
    }

    public static class TypeQueryBuilderTypesExt
    {
        public static ITypeQueryBuilderModifiers AllClasses(this ITypeQueryBuilderTargets types) =>
            types.Targeting(selector => selector.Classes());

        public static ITypeQueryBuilderModifiers AllInterfaces(this ITypeQueryBuilderTargets types) =>
            types.Targeting(selector => selector.Interfaces());
    }
}