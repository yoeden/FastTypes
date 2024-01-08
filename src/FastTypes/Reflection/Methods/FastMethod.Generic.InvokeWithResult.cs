using System;

#pragma warning disable CS1573

namespace FastTypes.Reflection
{
    public partial class FastMethod<TType>
    {
        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        /// <remarks>If type is known during compile time prefer using <see cref="FastMethod{TType}.InvokeWithResult{TResult}(TType)"/> to avoid boxing and extra allocation.</remarks>
        public object InvokeWithResult(TType instance)
        {
            ThrowIfInstanceIsNull(instance);
            ThrowIfMethodDoesntSupportObjectReturn();
            ThrowIfDelegateIsNot(_delReturnsObject, typeof(Func<TType, object>));

            return ((Func<TType, object>)_delReturnsObject)(instance);
        }
        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        /// <remarks>If type is known during compile time prefer using <see cref="FastMethod{TType}.InvokeWithResult{TResult}(TType)"/> to avoid boxing and extra allocation.</remarks>
        public object InvokeWithResult<T1>(TType instance, T1 arg1)
        {
            ThrowIfInstanceIsNull(instance);
            ThrowIfMethodDoesntSupportObjectReturn();
            ThrowIfDelegateIsNot(_delReturnsObject, typeof(Func<TType, T1, object>));

            return ((Func<TType, T1, object>)_delReturnsObject)(instance, arg1);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        /// <remarks>If type is known during compile time prefer using <see cref="FastMethod{TType}.InvokeWithResult{TResult}(TType)"/> to avoid boxing and extra allocation.</remarks>
        public object InvokeWithResult<T1, T2>(TType instance, T1 arg1, T2 arg2)
        {
            ThrowIfInstanceIsNull(instance);
            ThrowIfMethodDoesntSupportObjectReturn();
            ThrowIfDelegateIsNot(_delReturnsObject, typeof(Func<TType, T1, T2, object>));

            return ((Func<TType, T1, T2, object>)_delReturnsObject)(instance, arg1, arg2);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        /// <remarks>If type is known during compile time prefer using <see cref="FastMethod{TType}.InvokeWithResult{TResult}(TType)"/> to avoid boxing and extra allocation.</remarks>
        public object InvokeWithResult<T1, T2, T3>(TType instance, T1 arg1, T2 arg2, T3 arg3)
        {
            ThrowIfInstanceIsNull(instance);
            ThrowIfMethodDoesntSupportObjectReturn();
            ThrowIfDelegateIsNot(_delReturnsObject, typeof(Func<TType, T1, T2, T3, object>));

            return ((Func<TType, T1, T2, T3, object>)_delReturnsObject)(instance, arg1, arg2, arg3);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        /// <remarks>If type is known during compile time prefer using <see cref="FastMethod{TType}.InvokeWithResult{TResult}(TType)"/> to avoid boxing and extra allocation.</remarks>
        public object InvokeWithResult<T1, T2, T3, T4>(TType instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            ThrowIfInstanceIsNull(instance);
            ThrowIfMethodDoesntSupportObjectReturn();
            ThrowIfDelegateIsNot(_delReturnsObject, typeof(Func<TType, T1, T2, T3, T4, object>));

            return ((Func<TType, T1, T2, T3, T4, object>)_delReturnsObject)(instance, arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        /// <remarks>If type is known during compile time prefer using <see cref="FastMethod{TType}.InvokeWithResult{TResult}(TType)"/> to avoid boxing and extra allocation.</remarks>
        public object InvokeWithResult<T1, T2, T3, T4, T5>(TType instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            ThrowIfInstanceIsNull(instance);
            ThrowIfMethodDoesntSupportObjectReturn();
            ThrowIfDelegateIsNot(_delReturnsObject, typeof(Func<TType, T1, T2, T3, T4, T5, object>));

            return ((Func<TType, T1, T2, T3, T4, T5, object>)_delReturnsObject)(instance, arg1, arg2, arg3, arg4, arg5);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public TResult InvokeWithResult<TResult>(TType instance)
        {
            ThrowIfInstanceIsNull(instance);
            ThrowIfDelegateIsNot(_del, typeof(Func<TType, TResult>));

            return ((Func<TType, TResult>)_del)(instance);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public TResult InvokeWithResult<T1, TResult>(TType instance, T1 arg1)
        {
            ThrowIfInstanceIsNull(instance);
            ThrowIfDelegateIsNot(_del, typeof(Func<TType, T1, TResult>));

            return ((Func<TType, T1, TResult>)_del)(instance, arg1);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public TResult InvokeWithResult<T1, T2, TResult>(TType instance, T1 arg1, T2 arg2)
        {
            ThrowIfInstanceIsNull(instance);
            ThrowIfDelegateIsNot(_del, typeof(Func<TType, T1, T2, TResult>));

            return ((Func<TType, T1, T2, TResult>)_del)(instance, arg1, arg2);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public TResult InvokeWithResult<T1, T2, T3, TResult>(TType instance, T1 arg1, T2 arg2, T3 arg3)
        {
            ThrowIfInstanceIsNull(instance);
            ThrowIfDelegateIsNot(_del, typeof(Func<TType, T1, T2, T3, TResult>));

            return ((Func<TType, T1, T2, T3, TResult>)_del)(instance, arg1, arg2, arg3);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public TResult InvokeWithResult<T1, T2, T3, T4, TResult>(TType instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            ThrowIfInstanceIsNull(instance);
            ThrowIfDelegateIsNot(_del, typeof(Func<TType, T1, T2, T3, T4, TResult>));

            return ((Func<TType, T1, T2, T3, T4, TResult>)_del)(instance, arg1, arg2, arg3, arg4);
        }
        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public TResult InvokeWithResult<T1, T2, T3, T4, T5, TResult>(TType instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            ThrowIfInstanceIsNull(instance);
            ThrowIfDelegateIsNot(_del, typeof(Func<TType, T1, T2, T3, T4, T5, TResult>));

            return ((Func<TType, T1, T2, T3, T4, T5, TResult>)_del)(instance, arg1, arg2, arg3, arg4, arg5);
        }
    }
}
