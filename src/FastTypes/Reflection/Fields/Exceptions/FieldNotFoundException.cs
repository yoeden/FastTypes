using System;

namespace FastTypes.Reflection
{
    public sealed class FieldNotFoundException : InvalidOperationException
    {
        public FieldNotFoundException(string name) : base($"Field '{name}' not found.")
        {

        }
    }
}