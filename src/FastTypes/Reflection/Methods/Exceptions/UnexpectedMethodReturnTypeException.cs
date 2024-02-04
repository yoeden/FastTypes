using System;

namespace FastTypes.Reflection
{
    /// <summary>
    /// Exception thrown when a method is void but an expected return type is specified.
    /// </summary>
    public sealed class UnexpectedMethodReturnTypeException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedMethodReturnTypeException"/> class with the specified method name and return type.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="returnType">The expected return type.</param>
        public UnexpectedMethodReturnTypeException(string name, Type returnType,Type expectedType) : base($"{name} is returning {returnType.Name}, but expected return {expectedType.Name}.")
        {

        }
    }
}