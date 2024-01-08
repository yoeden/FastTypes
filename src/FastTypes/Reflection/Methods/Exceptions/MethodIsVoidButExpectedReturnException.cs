using System;

namespace FastTypes.Reflection
{
    /// <summary>
    /// Exception thrown when a method is void but an expected return type is specified.
    /// </summary>
    public sealed class MethodIsVoidButExpectedReturnException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodIsVoidButExpectedReturnException"/> class with the specified method name and return type.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="returnType">The expected return type.</param>
        public MethodIsVoidButExpectedReturnException(string name, Type returnType) : base($"{name} is void, but invocation expected return return {returnType.Name} (Func<..,{returnType.Name}>)")
        {

        }
    }
}