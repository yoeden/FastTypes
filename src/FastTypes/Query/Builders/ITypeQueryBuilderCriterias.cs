using System;

namespace FastTypes.Query
{
    /// <summary>
    /// Query modifiers
    /// </summary>
    public interface ITypeQueryBuilderCriterias : ITypeQueryBuilderPreparation
    {
        /// <summary>
        /// Adds a custom criteria to the query builder.
        /// </summary>
        /// <param name="criteria">The criteria to add.</param>
        ITypeQueryBuilderCriterias WithCriteria(ITypeQueryCriteria criteria);

        /// <summary>
        /// Adds a tag to the query builder.
        /// </summary>
        /// <typeparam name="T">The type of the tag.</typeparam>
        /// <param name="tag">The tag to add.</param>
        ITypeQueryBuilderCriterias Tag<T>(T tag);

        /// <summary>
        /// Sets the query builder to exclude non-public types.
        /// </summary>
        ITypeQueryBuilderCriterias NotPublic();

        /// <summary>
        /// Adds a property of the specified type to the query builder.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        ITypeQueryBuilderCriterias WithPropertyOfType<T>();

        /// <summary>
        /// Adds a property of the specified type to the query builder.
        /// </summary>
        /// <param name="t">The type of the property.</param>
        ITypeQueryBuilderCriterias WithPropertyOfType(Type t);

        /// <summary>
        /// Adds a method of the specified type to the query builder.
        /// </summary>
        /// <typeparam name="T">The type of the method.</typeparam>
        ITypeQueryBuilderCriterias WithMethodOfType<T>();

        /// <summary>
        /// Adds a method of the specified type to the query builder.
        /// </summary>
        /// <param name="t">The type of the method.</param>
        ITypeQueryBuilderCriterias WithMethodOfType(Type t);

        /// <summary>
        /// Adds an attribute of the specified type to the query builder.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        ITypeQueryBuilderCriterias WithAttribute<T>() where T : Attribute;

        /// <summary>
        /// Adds an attribute of the specified type to the query builder.
        /// </summary>
        /// <param name="t">The type of the attribute.</param>
        ITypeQueryBuilderCriterias WithAttribute(Type t);

        /// <summary>
        /// Adds a constraint that the type must be assignable to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to assign to.</typeparam>
        ITypeQueryBuilderCriterias AssignableTo<T>();

        /// <summary>
        /// Adds a constraint that the type must be assignable to the specified type.
        /// </summary>
        /// <param name="t">The type to assign to.</param>
        ITypeQueryBuilderCriterias AssignableTo(Type t);
    }
}