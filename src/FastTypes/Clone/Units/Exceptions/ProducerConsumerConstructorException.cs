using System;

namespace FastTypes.Clone
{
    /// <summary>
    /// Represents an exception thrown when a producer-consumer constructor is invalid.
    /// </summary>
    public sealed class ProducerConsumerConstructorException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProducerConsumerConstructorException"/> class with the specified type.
        /// </summary>
        /// <param name="t">The type that caused the exception.</param>
        public ProducerConsumerConstructorException(Type t) : base($"Type {t.Name} is expected to either have a ctor(IProducerConsumer) or ctor(IEnumerable),")
        {

        }
    }
}
