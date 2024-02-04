using System;

namespace FastTypes.Reflection
{
    /// <summary>
    /// Exception thrown when the parameters passed to a method are not matching its signature
    /// </summary>
    public sealed class UnexpectedMethodParametersException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedMethodParametersException"/> class with the specified method and given types.
        /// </summary>
        /// <param name="method">The expected method signature.</param>
        /// <param name="given">The actual method signature.</param>
        public UnexpectedMethodParametersException(Type method, Type given) : base($"Invalid invocation, method is with signature '{method}' but tried invoke as '{(given == null ? "null" : given)}'")
        {

        }
    }
}