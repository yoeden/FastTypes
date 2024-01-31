using System;
using System.Collections;
using System.Collections.Generic;
using FastTypes.Clone;
using FastTypes.Query;
using FastTypes.Reflection;

namespace FastTypes
{
    /// <summary>
    /// This type contains all fast information needed.
    /// </summary>
    /// <remarks>Try avoid using this base class unless it is absolutely necessary or if type can be used in compile time, always prefer <see cref="IFastType{TType}"/></remarks>
    public interface IFastType
    {
        /// <summary>
        /// Gets the Type represented by this IFastType.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Gets the FastProperty with the specified property name.
        /// </summary>
        /// <param name="prop">The property name.</param>
        /// <returns>The FastProperty.</returns>
        FastProperty Property(string prop);

        /// <summary>
        /// Gets all the FastProperties of this IFastType.
        /// </summary>
        /// <returns>An IEnumerable of FastProperties.</returns>
        IEnumerable<FastProperty> Properties();

        /// <summary>
        /// Gets the FastMethod with the specified method name.
        /// </summary>
        /// <param name="name">The method name.</param>
        /// <returns>The FastMethod.</returns>
        FastMethod Method(string name);

        /// <summary>
        /// Gets the FastActivator for creating instances of this IFastType.
        /// </summary>
        /// <returns>The FastActivator.</returns>
        FastActivator Activator();
    }

    /// <summary>
    /// This type contains all fast information needed.
    /// </summary>
    /// <remarks>Prefer using this interface over <see cref="IFastType"/> whenever possible, this types eliminates unnecessary allocations</remarks>
    public interface IFastType<TType> : IFastType
    {
        /// <summary>
        /// Gets the FastProperty with the specified property name.
        /// </summary>
        /// <param name="prop">The property name.</param>
        /// <returns>The FastProperty.</returns>
        new FastProperty<TType> Property(string prop);

        /// <summary>
        /// Gets all the FastProperties of this IFastType.
        /// </summary>
        /// <returns>An IEnumerable of FastProperties.</returns>
        new IEnumerable<FastProperty<TType>> Properties();

        /// <summary>
        /// Gets the FastMethod with the specified method name.
        /// </summary>
        /// <param name="name">The method name.</param>
        /// <returns>The FastMethod.</returns>
        new FastMethod<TType> Method(string name);

        /// <summary>
        /// Gets the FastActivator for creating instances of this IFastType.
        /// </summary>
        /// <returns>The FastActivator.</returns>
        new FastActivator<TType> Activator();
    }

    /// <summary>
    /// FastType factory entry point class.
    /// </summary>
    public static class FastType
    {
        //Hashtable for some reason seem to be faster than `Dictionary<string,object>()`
        private static readonly Hashtable Cache = new();

        /// <summary>
        /// Returns an instance of FastType for the specified type T
        /// </summary>
        /// <typeparam name="T">The type to reflect</typeparam>
        /// <returns>FastType instance</returns>
        public static IFastType<T> Of<T>()
        {
            return (FastType<T>)Of(typeof(T));
        }

        /// <summary>
        /// Returns an instance of FastType for the specified type Type
        /// </summary>
        /// <returns>FastType instance (assignable to <see cref="IFastType{TType}"/>)</returns>
        public static IFastType Of(Type t)
        {
            lock (Cache)
            {
                if (Cache.ContainsKey(t)) return (IFastType)Cache[t];

                var fastType = (IFastType)Activator.CreateInstance(typeof(FastType<>).MakeGenericType(t), true);
                Cache.Add(t, fastType);
                return fastType;
            }
        }

        /// <summary>
        /// Starts a query builder.
        /// </summary>
        /// <returns>A query builder</returns>
        public static ITypeQueryBuilderAssembly Query()
        {
            return new TypeQueryBuilder();
        }

        /// <summary>
        /// Creates a deep copy of the state of the given object (fields and properties).
        /// </summary>
        /// <param name="src">The object to clone</param>
        /// <returns>A deep cloned copy of the given object</returns>
        public static T DeepCopy<T>(T src) => FastCopy.DeepCopy(src);

        /// <summary>
        /// Creates a deep copy of the state of the given object (fields and properties).
        /// </summary>
        /// <param name="src">The object to clone</param>
        /// <returns>A deep cloned copy of the given object</returns>
        public static object DeepCopy(object src) => FastCopy.DeepCopy(src);
    }
}