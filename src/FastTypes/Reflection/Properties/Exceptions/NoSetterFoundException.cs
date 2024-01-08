using System;

namespace FastTypes.Reflection
{
    /// <summary>
    /// Exception thrown when no setter is found for a property.
    /// </summary>
    public sealed class NoSetterFoundException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoSetterFoundException"/> class with the specified property name.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public NoSetterFoundException(string name) : base($"No setter found for property '{name}'")
        {

        }
    }
}