using System;

#pragma warning disable CS1573

namespace FastTypes.Reflection
{
    public partial class FastMethod<TType>
    {
        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public void Invoke(TType instance)
        {
            ThrowIfInstanceIsNull(instance);

            if (_del is Action<TType> action) action(instance);
            else if (_delReturnsObject is Func<TType, object> func) func(instance);
            else ThrowHelper.UnexpectedMethodSignature(_del.GetType(), typeof(Action<TType>));
        }

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public void Invoke<T1>(TType instance, T1 arg1)
        {
            ThrowIfInstanceIsNull(instance);

            if (_del is Action<TType, T1> action) action(instance, arg1);
            else if (_delReturnsObject is Func<TType, T1, object> func) func(instance, arg1);
            else ThrowHelper.UnexpectedMethodSignature(_del.GetType(), typeof(Action<TType, T1>));
        }

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public void Invoke<T1, T2>(TType instance, T1 arg1, T2 arg2)
        {
            ThrowIfInstanceIsNull(instance);

            if (_del is Action<TType, T1, T2> action) action(instance, arg1, arg2);
            else if (_delReturnsObject is Func<TType, T1, T2, object> func) func(instance, arg1, arg2);
            else ThrowHelper.UnexpectedMethodSignature(_del.GetType(), typeof(Action<TType, T1, T2>));
        }

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public void Invoke<T1, T2, T3>(TType instance, T1 arg1, T2 arg2, T3 arg3)
        {
            ThrowIfInstanceIsNull(instance);

            if (_del is Action<TType, T1, T2, T3> action) action(instance, arg1, arg2, arg3);
            else if (_delReturnsObject is Func<TType, T1, T2, T3, object> func) func(instance, arg1, arg2, arg3);
            else ThrowHelper.UnexpectedMethodSignature(_del.GetType(), typeof(Action<TType, T1, T2, T3>));
        }

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public void Invoke<T1, T2, T3, T4>(TType instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            ThrowIfInstanceIsNull(instance);

            if (_del is Action<TType, T1, T2, T3, T4> action) action(instance, arg1, arg2, arg3, arg4);
            else if (_delReturnsObject is Func<TType, T1, T2, T3, T4, object> func) func(instance, arg1, arg2, arg3, arg4);
            else ThrowHelper.UnexpectedMethodSignature(_del.GetType(), typeof(Action<TType, T1, T2, T3, T4>));
        }

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public void Invoke<T1, T2, T3, T4, T5>(TType instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            ThrowIfInstanceIsNull(instance);

            if (_del is Action<TType, T1, T2, T3, T4, T5> action) action(instance, arg1, arg2, arg3, arg4, arg5);
            else if (_delReturnsObject is Func<TType, T1, T2, T3, T4, T5, object> func) func(instance, arg1, arg2, arg3, arg4, arg5);
            else ThrowHelper.UnexpectedMethodSignature(_del.GetType(), typeof(Action<TType, T1, T2, T3, T4, T5>));
        }
    }
}
