using System;
using System.Collections.Generic;
using System.Text;

namespace FastTypes.Query
{
    /// <summary>
    /// Exception thrown when failed to instantiate a type.
    /// </summary>
    public sealed class FailedToInstanciateTypeException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToInstanciateTypeException"/> class.
        /// </summary>
        /// <param name="t">The type that failed to be instantiated.</param>
        /// <param name="ex">The exception that caused the failure.</param>
        public FailedToInstanciateTypeException(Type t, Exception ex) : base($"Failed to instanciate type: {t}", ex)
        {

        }
    }
}