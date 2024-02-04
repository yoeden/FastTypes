using System;

namespace FastTypes.Reflection
{
    public sealed class UnexpectedFieldTypeException : InvalidOperationException
    {
        public UnexpectedFieldTypeException(Type expectedType, Type givenType, string name)
            : base($"Field '{name}' is of type {expectedType.Name} but given {givenType.Name}")
        {
        }
    }
}