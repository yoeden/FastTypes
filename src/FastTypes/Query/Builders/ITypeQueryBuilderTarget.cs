using System;

namespace FastTypes.Query
{
    /// <summary>
    /// Allows the user to select from which types he wants to apply to scan on.
    /// </summary>
    public interface ITypeQueryBuilderTarget
    {
        /// <summary>
        /// Specifies which types are relevant targets.
        /// </summary>
        /// <param name="types">The type selector.</param>
        /// <returns>The type query builder modifiers.</returns>
        ITypeQueryBuilderCriterias Target(Action<ITypeSelector> types);
    }

    /// <summary>
    /// 
    /// </summary>
    public static class TypeQueryBuilderTypesExt
    {
        /// <summary>
        /// Sets the target type to classes.
        /// </summary>
        /// <param name="types">The type query builder target.</param>
        /// <returns>The type query builder modifiers.</returns>
        public static ITypeQueryBuilderCriterias TargetClasses(this ITypeQueryBuilderTarget types) =>
            types.Target(selector => selector.Classes());

        /// <summary>
        /// Sets the target type to interfaces.
        /// </summary>
        /// <param name="types">The type query builder target.</param>
        /// <returns>The type query builder modifiers.</returns>
        public static ITypeQueryBuilderCriterias TargetInterfaces(this ITypeQueryBuilderTarget types) =>
            types.Target(selector => selector.Interfaces());

        /// <summary>
        /// Sets the target type to value types.
        /// </summary>
        /// <param name="types">The type query builder target.</param>
        /// <returns>The type query builder modifiers.</returns>
        public static ITypeQueryBuilderCriterias TargetValueTypes(this ITypeQueryBuilderTarget types) =>
            types.Target(selector => selector.ValueTypes());

        /// <summary>
        /// Sets the target type to enums.
        /// </summary>
        /// <param name="types">The type query builder target.</param>
        /// <returns>The type query builder modifiers.</returns>
        public static ITypeQueryBuilderCriterias TargetEnums(this ITypeQueryBuilderTarget types) =>
            types.Target(selector => selector.Enums());

        /// <summary>
        /// Sets the target type to all types.
        /// </summary>
        /// <param name="types">The type query builder target.</param>
        /// <returns>The type query builder modifiers.</returns>
        public static ITypeQueryBuilderCriterias TargetAllTypes(this ITypeQueryBuilderTarget types) =>
            types.Target(selector => selector.Interfaces().Classes().Enums().Interfaces());
    }
}