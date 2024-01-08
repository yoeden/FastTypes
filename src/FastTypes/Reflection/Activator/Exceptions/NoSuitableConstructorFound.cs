using System;

namespace FastTypes.Reflection
{
    /// <summary>
    /// Custom exception class for when no suitable constructor is found.
    /// </summary>
    public sealed class NoSuitableConstructorFound : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoSuitableConstructorFound"/> class
        /// with a message that includes the type name.
        /// </summary>
        /// <param name="t">The type for which no suitable constructor was found.</param>
        public NoSuitableConstructorFound(Type t) : base($"No suitable constructor found that matches: {t}")
        {

        }
    }
}