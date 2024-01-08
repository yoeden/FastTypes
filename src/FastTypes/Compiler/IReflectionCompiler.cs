using System;
using System.Reflection;

namespace FastTypes.Compiler
{
    /// <summary>
    /// Represents a compiler for reflection-based operations.
    /// </summary>
    internal interface IReflectionCompiler
    {
        /// <summary>
        /// Compiles a delegate for setting the value of a property.
        /// </summary>
        /// <param name="property">The property to set the value for.</param>
        /// <returns>A delegate for setting the value of the property.</returns>
        Delegate Setter(PropertyInfo property);

        /// <summary>
        /// Compiles a delegate for setting the value of a property with the value parameter given as an object.
        /// </summary>
        /// <param name="property">The property to set the value for.</param>
        /// <returns>A delegate for setting the value of the property with an object parameter.</returns>
        Delegate SetterWithObjectParameter(PropertyInfo property);

        /// <summary>
        /// Compiles a delegate for getting the value of a property.
        /// </summary>
        /// <param name="property">The property to get the value from.</param>
        /// <returns>A delegate for getting the value of the property.</returns>
        Delegate Getter(PropertyInfo property);

        /// <summary>
        /// Compiles a delegate for getting the value of a property with the return value as an object.
        /// </summary>
        /// <param name="property">The property to get the value from.</param>
        /// <returns>A delegate for getting the value of the property with an object return.</returns>
        Delegate GetterWithObjectReturn(PropertyInfo property);

        /// <summary>
        /// Compiles a delegate for invoking a method.
        /// </summary>
        /// <param name="info">The method to invoke.</param>
        /// <returns>A delegate for invoking the method.</returns>
        Delegate Method(MethodInfo info);

        /// <summary>
        /// Compiles a delegate for invoking a method with an object return.
        /// </summary>
        /// <param name="info">The method to invoke.</param>
        /// <returns>A delegate for invoking the method with an object return.</returns>
        Delegate MethodReturnObject(MethodInfo info);

        /// <summary>
        /// Compiles a delegate for creating an instance of a type using a constructor.
        /// </summary>
        /// <param name="info">The constructor to use for creating the instance.</param>
        /// <returns>A delegate for creating an instance of the type using the constructor.</returns>
        Delegate Activator(ConstructorInfo info);
    }
}