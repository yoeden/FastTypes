using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTypes.Reflection
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
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        FastMethodWithResult<TResult> Method<TResult>(string name);

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
        /// Gets the FastMethod with the specified method name.
        /// </summary>
        /// <param name="name">The method name.</param>
        /// <returns>The FastMethod.</returns>
        new FastMethod<TType, TResult> Method<TResult>(string name);

        /// <summary>
        /// Gets the FastActivator for creating instances of this IFastType.
        /// </summary>
        /// <returns>The FastActivator.</returns>
        new FastActivator<TType> Activator();
    }
}
