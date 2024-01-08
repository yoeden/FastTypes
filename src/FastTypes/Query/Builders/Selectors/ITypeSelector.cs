namespace FastTypes.Query
{
    /// <summary>
    /// Represents a type selector.
    /// </summary>
    public interface ITypeSelector
    {
        /// <summary>
        /// Selects classes.
        /// </summary>
        /// <returns>A type selector.</returns>
        ITypeSelector Classes();

        /// <summary>
        /// Selects interfaces.
        /// </summary>
        /// <returns>A type selector.</returns>
        ITypeSelector Interfaces();

        /// <summary>
        /// Selects value types.
        /// </summary>
        /// <returns>A type selector.</returns>
        ITypeSelector ValueTypes();

        /// <summary>
        /// Selects enums.
        /// </summary>
        /// <returns>A type selector.</returns>
        ITypeSelector Enums();
    }
}