using System;
using System.Collections.Generic;
using System.Text;

namespace FastTypes.Reflection
{
    /// <summary>
    /// Exception thrown when an unexpected method signature is encountered.
    /// </summary>
    public sealed class UnexpectedMethodSignatureException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedMethodSignatureException"/> class with the specified method and given types.
        /// </summary>
        /// <param name="method">The expected method signature.</param>
        /// <param name="given">The actual method signature.</param>
        public UnexpectedMethodSignatureException(Type method, Type given) : base($"Invalid invocation, method is with signature '{method}' but tried invoke as '{(given == null ? "null" : given)}'")
        {

        }
    }
}