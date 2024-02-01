using System;
using System.Collections.Generic;

namespace FastTypes.Clone
{
    internal sealed class SeenTypes
    {
        private readonly HashSet<Type> _seen = new();

        internal void MarkAsSeen(Type type) => _seen.Add(type);

        public bool HasBeenSeen(Type t) => _seen.Contains(t);
    }
}