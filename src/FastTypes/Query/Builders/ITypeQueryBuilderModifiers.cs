using System;

namespace FastTypes.Query
{
    /// <summary>
    /// Creates a new query group
    /// </summary>
    public interface ITypeQueryBuilderAppend
    {
        /// <summary>
        /// Sealing the current query in a group, and creating a new query from the current assembly configurations.
        /// </summary>
        /// <returns>A new instance of the query builder.</returns>
        ITypeQueryBuilderTarget And();
    }

    /// <summary>
    /// Query modifiers
    /// </summary>
    public interface ITypeQueryBuilderModifiers : ITypeQueryBuilderPreparation
    {
        /// <summary>
        /// Adds a custom criteria to the query builder.
        /// </summary>
        /// <param name="criteria">The criteria to add.</param>
        ITypeQueryBuilderModifiers WithCriteria(ITypeQueryCriteria criteria);

        /// <summary>
        /// Adds a tag to the query builder.
        /// </summary>
        /// <typeparam name="T">The type of the tag.</typeparam>
        /// <param name="tag">The tag to add.</param>
        ITypeQueryBuilderModifiers Tag<T>(T tag);

        /// <summary>
        /// Sets the query builder to exclude non-public types.
        /// </summary>
        ITypeQueryBuilderModifiers NotPublic();

        /// <summary>
        /// Adds a property of the specified type to the query builder.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        ITypeQueryBuilderModifiers WithPropertyOfType<T>();

        /// <summary>
        /// Adds a property of the specified type to the query builder.
        /// </summary>
        /// <param name="t">The type of the property.</param>
        ITypeQueryBuilderModifiers WithPropertyOfType(Type t);

        /// <summary>
        /// Adds a method of the specified type to the query builder.
        /// </summary>
        /// <typeparam name="T">The type of the method.</typeparam>
        ITypeQueryBuilderModifiers WithMethodOfType<T>();

        /// <summary>
        /// Adds a method of the specified type to the query builder.
        /// </summary>
        /// <param name="t">The type of the method.</param>
        ITypeQueryBuilderModifiers WithMethodOfType(Type t);

        /// <summary>
        /// Adds an attribute of the specified type to the query builder.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        ITypeQueryBuilderModifiers WithAttribute<T>() where T : Attribute;

        /// <summary>
        /// Adds an attribute of the specified type to the query builder.
        /// </summary>
        /// <param name="t">The type of the attribute.</param>
        ITypeQueryBuilderModifiers WithAttribute(Type t);

        /// <summary>
        /// Adds a constraint that the type must be assignable to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to assign to.</typeparam>
        ITypeQueryBuilderModifiers AssignableTo<T>();

        /// <summary>
        /// Adds a constraint that the type must be assignable to the specified type.
        /// </summary>
        /// <param name="t">The type to assign to.</param>
        ITypeQueryBuilderModifiers AssignableTo(Type t);
    }
}