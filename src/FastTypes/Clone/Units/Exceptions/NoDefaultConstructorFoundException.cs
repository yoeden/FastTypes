using System;

namespace FastTypes.Clone
{

    /// <summary>
    /// Exception thrown when no default constructor is found for cloning a type.
    /// </summary>
    public sealed class NoDefaultConstructorFoundException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the NoDefaultConstructorFound class with the specified type.
        /// </summary>
        /// <param name="t">The type that must have a default constructor to be cloned.</param>
        public NoDefaultConstructorFoundException(Type t) : base($"Type {t.Name} must have a default (public or private) constructor to be cloned.")
        {

        }
    }
}
