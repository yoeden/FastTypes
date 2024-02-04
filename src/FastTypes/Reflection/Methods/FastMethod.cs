using System;
using System.Reflection;

#pragma warning disable CS1712
#pragma warning disable CS1573

namespace FastTypes.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class FastMethod : BaseFastMethod
    {
        protected FastMethod(MethodInfo info) : base(info)
        {
        }

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract object Invoke(object instance);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract object Invoke<T1>(object instance, T1 arg1);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract object Invoke<T1, T2>(object instance, T1 arg1, T2 arg2);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract object Invoke<T1, T2, T3>(object instance, T1 arg1, T2 arg2, T3 arg3);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract object Invoke<T1, T2, T3, T4>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract object Invoke<T1, T2, T3, T4, T5>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract object Invoke(object instance, params object[] args);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class FastMethod<TType> : FastMethod
    {
        private readonly Delegate _delegate;

        internal FastMethod(MethodInfo source, Delegate @delegate) : base(source)
        {
            if (@delegate.Method.ReturnType != typeof(object)) throw new ArgumentException();
            if (@delegate.Method.GetParameters().Length == 0 || @delegate.Method.GetParameters()[0].ParameterType != typeof(TType)) throw new ArgumentException();

            _delegate = @delegate;
        }

        /// <inheritdoc />
        public override object Invoke(object instance) => Invoke((TType)instance);

        /// <inheritdoc />
        public override object Invoke<T1>(object instance, T1 arg1) => Invoke((TType)instance, arg1);

        /// <inheritdoc />
        public override object Invoke<T1, T2>(object instance, T1 arg1, T2 arg2) => Invoke((TType)instance, arg1, arg2);

        /// <inheritdoc />
        public override object Invoke<T1, T2, T3>(object instance, T1 arg1, T2 arg2, T3 arg3) => Invoke((TType)instance, arg1, arg2, arg3);

        /// <inheritdoc />
        public override object Invoke<T1, T2, T3, T4>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => Invoke((TType)instance, arg1, arg2, arg3, arg4);

        /// <inheritdoc />
        public override object Invoke<T1, T2, T3, T4, T5>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => Invoke((TType)instance, arg1, arg2, arg3, arg4, arg5);

        /// <inheritdoc />
        public override object Invoke(object instance, params object[] args) => Invoke((TType)instance, args);

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public object Invoke(TType instance)
        {
            ThrowIfInstanceIsNull(instance);

            Func<TType, object> func = _delegate as Func<TType, object>;
            if (func == null)
                ThrowHelper.UnexpectedMethodParameters(_delegate.GetType(), typeof(Func<TType, object>));

            return func(instance);
        }

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public object Invoke<T1>(TType instance, T1 arg1)
        {
            ThrowIfInstanceIsNull(instance);

            Func<TType, T1, object> func = _delegate as Func<TType, T1, object>;
            if (func == null)
                ThrowHelper.UnexpectedMethodParameters(_delegate.GetType(), typeof(Func<TType, T1, object>));

            return func(instance, arg1);
        }

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public object Invoke<T1, T2>(TType instance, T1 arg1, T2 arg2)
        {
            ThrowIfInstanceIsNull(instance);

            Func<TType, T1, T2, object> func = _delegate as Func<TType, T1, T2, object>;
            if (func == null)
                ThrowHelper.UnexpectedMethodParameters(_delegate.GetType(), typeof(Func<TType, T1, T2, object>));

            return func(instance, arg1, arg2);
        }

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public object Invoke<T1, T2, T3>(TType instance, T1 arg1, T2 arg2, T3 arg3)
        {
            ThrowIfInstanceIsNull(instance);

            Func<TType, T1, T2, T3, object> func = _delegate as Func<TType, T1, T2, T3, object>;
            if (func == null)
                ThrowHelper.UnexpectedMethodParameters(_delegate.GetType(), typeof(Func<TType, T1, T2, T3, object>));

            return func(instance, arg1, arg2, arg3);
        }

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public object Invoke<T1, T2, T3, T4>(TType instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            ThrowIfInstanceIsNull(instance);

            Func<TType, T1, T2, T3, T4, object> func = _delegate as Func<TType, T1, T2, T3, T4, object>;
            if (func == null)
                ThrowHelper.UnexpectedMethodParameters(_delegate.GetType(), typeof(Func<TType, T1, T2, T3, T4, object>));

            return func(instance, arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Invokes a void method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        public object Invoke<T1, T2, T3, T4, T5>(TType instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            ThrowIfInstanceIsNull(instance);

            Func<TType, T1, T2, T3, T4, T5, object> func = _delegate as Func<TType, T1, T2, T3, T4, T5, object>;
            if (func == null)
                ThrowHelper.UnexpectedMethodParameters(_delegate.GetType(), typeof(Func<TType, T1, T2, T3, T4, T5, object>));

            return func(instance, arg1, arg2, arg3, arg4, arg5);
        }

        //TODO:
        public object Invoke(TType instance, params object[] args)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class FastMethod<TType, TResult> : FastMethodWithResult<TResult>
    {
        private readonly Delegate _delegate;

        internal FastMethod(MethodInfo source, Delegate @delegate) : base(source)
        {
            if (@delegate.Method.ReturnType != typeof(TResult)) throw new ArgumentException();
            if (@delegate.Method.GetParameters().Length == 0 || @delegate.Method.GetParameters()[0].ParameterType != typeof(TType)) throw new ArgumentException();

            _delegate = @delegate;
        }

        public override TResult Invoke(object instance) => Invoke((TType)instance);

        public override TResult Invoke<T1>(object instance, T1 arg1) => Invoke((TType)instance, arg1);

        public override TResult Invoke<T1, T2>(object instance, T1 arg1, T2 arg2) => Invoke((TType)instance, arg1, arg2);

        public override TResult Invoke<T1, T2, T3>(object instance, T1 arg1, T2 arg2, T3 arg3) => Invoke((TType)instance, arg1, arg2, arg3);

        public override TResult Invoke<T1, T2, T3, T4>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => Invoke((TType)instance, arg1, arg2, arg3, arg4);

        public override TResult Invoke<T1, T2, T3, T4, T5>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => Invoke((TType)instance, arg1, arg2, arg3, arg4, arg5);

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public TResult Invoke(TType instance)
        {
            ThrowIfInstanceIsNull(instance);

            Func<TType, TResult> func = _delegate as Func<TType, TResult>;
            if (func == null)
                ThrowHelper.UnexpectedMethodParameters(_delegate.GetType(), typeof(Func<TType, TResult>));

            return func(instance);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public TResult Invoke<T1>(TType instance, T1 arg1)
        {
            ThrowIfInstanceIsNull(instance);

            Func<TType, T1, TResult> func = _delegate as Func<TType, T1, TResult>;
            if (func == null)
                ThrowHelper.UnexpectedMethodParameters(_delegate.GetType(), typeof(Func<TType, T1, TResult>));

            return func(instance, arg1);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public TResult Invoke<T1, T2>(TType instance, T1 arg1, T2 arg2)
        {
            ThrowIfInstanceIsNull(instance);

            Func<TType, T1, T2, TResult> func = _delegate as Func<TType, T1, T2, TResult>;
            if (func == null)
                ThrowHelper.UnexpectedMethodParameters(_delegate.GetType(), typeof(Func<TType, T1, T2, TResult>));

            return func(instance, arg1, arg2);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public TResult Invoke<T1, T2, T3>(TType instance, T1 arg1, T2 arg2, T3 arg3)
        {
            ThrowIfInstanceIsNull(instance);

            Func<TType, T1, T2, T3, TResult> func = _delegate as Func<TType, T1, T2, T3, TResult>;
            if (func == null)
                ThrowHelper.UnexpectedMethodParameters(_delegate.GetType(), typeof(Func<TType, T1, T2, T3, TResult>));

            return func(instance, arg1, arg2, arg3);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public TResult Invoke<T1, T2, T3, T4>(TType instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            ThrowIfInstanceIsNull(instance);

            Func<TType, T1, T2, T3, T4, TResult> func = _delegate as Func<TType, T1, T2, T3, T4, TResult>;
            if (func == null)
                ThrowHelper.UnexpectedMethodParameters(_delegate.GetType(), typeof(Func<TType, T1, T2, T3, T4, TResult>));

            return func(instance, arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public TResult Invoke<T1, T2, T3, T4, T5>(TType instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            ThrowIfInstanceIsNull(instance);

            Func<TType, T1, T2, T3, T4, T5, TResult> func = _delegate as Func<TType, T1, T2, T3, T4, T5, TResult>;
            if (func == null)
                ThrowHelper.UnexpectedMethodParameters(_delegate.GetType(), typeof(Func<TType, T1, T2, T3, T4, T5, TResult>));

            return func(instance, arg1, arg2, arg3, arg4, arg5);
        }
    }
}
