using System;

namespace FastTypes.Reflection.Exceptions
{
    /// <summary>
    /// Exception thrown when a member's type is unexpected.
    /// </summary>
    public sealed class UnexpectedMemberType : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedMemberType"/> class
        /// with the specified given and expected types.
        /// </summary>
        /// <param name="given">The given type of the member.</param>
        /// <param name="expected">The expected type of the member.</param>
        public UnexpectedMemberType(Type given, Type expected)
            : base($"Member expected type '{expected.Name}' but received '{given.Name}'")
        {

        }
    }
}