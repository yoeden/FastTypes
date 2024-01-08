using System;
using System.Collections.Generic;
using System.Text;

namespace FastTypes.Reflection
{
    /// <summary>
    /// Exception thrown when no getter is found for a property.
    /// </summary>
    public sealed class NoGetterFoundException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoGetterFoundException"/> class with the specified property name.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public NoGetterFoundException(string name) : base($"No getter found for property '{name}'")
        {

        }
    }
}