using System;
using System.Collections;
using System.Collections.Generic;
using FastTypes.Query;
using FastTypes.Reflection;

namespace FastTypes
{
    public interface IFastType
    {
        Type Type { get; }

        FastProperty Property(string prop);

        IEnumerable<FastProperty> Properties();

        FastMethod Method(string name);

        FastActivator Activator();
    }

    public interface IFastType<TType> : IFastType
    {
        new FastProperty<TType> Property(string prop);

        new IEnumerable<FastProperty<TType>> Properties();

        new FastMethod<TType> Method(string name);

        new FastActivator<TType> Activator();
    }

    public static class FastType
    {
        private static readonly Hashtable _cache = new();

        public static IFastType<T> Of<T>()
        {
            return (FastType<T>)Of(typeof(T));
        }

        public static IFastType Of(Type t)
        {
            lock (_cache)
            {
                if (_cache.ContainsKey(t)) return (IFastType)_cache[t];

                var fastType = (IFastType)System.Activator.CreateInstance(typeof(FastType<>).MakeGenericType(t), true);
                _cache.Add(t, fastType);
                return fastType;
            }
        }

        public static ITypeQueryBuilderAssembly Query()
        {
            return new TypeQueryBuilder();
        }
    }
}