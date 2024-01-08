using System;
using System.Collections.Generic;
using System.Text;

namespace FastTypes.Reflection
{
    /// <summary>
    /// Exception thrown when an instance is null for non-static members.
    /// </summary>
    public sealed class InstanceNullException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceNullException"/> class
        /// with the specified target.
        /// </summary>
        /// <param name="target">The target for which the instance is null.</param>
        public InstanceNullException(string target) : base($"Instance cannot be null for non-static members ({target}).")
        {

        }
    }
}