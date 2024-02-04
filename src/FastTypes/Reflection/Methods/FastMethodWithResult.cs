using System.Reflection;

namespace FastTypes.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class FastMethodWithResult<TResult> : BaseFastMethod
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        protected FastMethodWithResult(MethodInfo info) : base(info)
        {
        }

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract TResult Invoke(object instance);

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract TResult Invoke<T1>(object instance, T1 arg1);

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract TResult Invoke<T1, T2>(object instance, T1 arg1, T2 arg2);

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract TResult Invoke<T1, T2, T3>(object instance, T1 arg1, T2 arg2, T3 arg3);

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract TResult Invoke<T1, T2, T3, T4>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        /// <summary>
        /// Invokes a method on a specified instance.
        /// </summary>
        /// <param name="instance">The instance on which to invoke the method. (null if static method)</param>
        /// <returns>The result of the method invocation.</returns>
        public abstract TResult Invoke<T1, T2, T3, T4, T5>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}