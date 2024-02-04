using System;

namespace FastTypes.Reflection
{
    public sealed class MethodDoesntExistsException : Exception
    {
        public MethodDoesntExistsException(Type owner, string name) : base($"Method {name} doesn't exists in type {owner.Name}")
        {
        }
    }
}