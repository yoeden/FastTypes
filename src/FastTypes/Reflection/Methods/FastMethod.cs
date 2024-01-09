using System;

#pragma warning disable CS1712
#pragma warning disable CS1573

namespace FastTypes.Reflection
{
    /// <summary>
    /// This class provides support for invoking methods a given Type.
    /// </summary>
    public abstract class FastMethod : FastMember
    {
        /// <summary>
        /// Checks if the method has no return type (void).
        /// </summary>
        public bool IsVoid => ReturnType == typeof(void);

        /// <summary>
        /// The return type of the give method.
        /// </summary>
        /// <remarks>In case of void the type will be <see cref="Void"/></remarks>
        public abstract Type ReturnType { get; }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract TResult InvokeWithResult<TResult>(object instance);

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract TResult InvokeWithResult<T1, TResult>(object instance, T1 arg1);

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract TResult InvokeWithResult<T1, T2, TResult>(object instance, T1 arg1, T2 arg2);

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract TResult InvokeWithResult<T1, T2, T3, TResult>(object instance, T1 arg1, T2 arg2, T3 arg3);

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract TResult InvokeWithResult<T1, T2, T3, T4, TResult>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract TResult InvokeWithResult<T1, T2, T3, T4, T5, TResult>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract object InvokeWithResult(object instance);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract object InvokeWithResult<T1>(object instance, T1 arg1);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract object InvokeWithResult<T1, T2>(object instance, T1 arg1, T2 arg2);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract object InvokeWithResult<T1, T2, T3>(object instance, T1 arg1, T2 arg2, T3 arg3);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract object InvokeWithResult<T1, T2, T3, T4>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract object InvokeWithResult<T1, T2, T3, T4, T5>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public abstract void Invoke(object instance);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public abstract void Invoke<T1>(object instance, T1 arg1);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public abstract void Invoke<T1, T2>(object instance, T1 arg1, T2 arg2);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public abstract void Invoke<T1, T2, T3>(object instance, T1 arg1, T2 arg2, T3 arg3);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public abstract void Invoke<T1, T2, T3, T4>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public abstract void Invoke<T1, T2, T3, T4, T5>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }

}
