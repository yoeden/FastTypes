using System;

namespace FastTypes.Reflection
{
    public abstract class FastMethod : FastMember
    {
        public bool IsVoid => ReturnType == typeof(void);
        public abstract Type ReturnType { get; }

        public abstract TResult InvokeWithResult<TResult>(object instance);
        public abstract TResult InvokeWithResult<T1, TResult>(object instance, T1 arg1);
        public abstract TResult InvokeWithResult<T1, T2, TResult>(object instance, T1 arg1, T2 arg2);
        public abstract TResult InvokeWithResult<T1, T2, T3, TResult>(object instance, T1 arg1, T2 arg2, T3 arg3);
        public abstract TResult InvokeWithResult<T1, T2, T3, T4, TResult>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
        public abstract TResult InvokeWithResult<T1, T2, T3, T4, T5, TResult>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

        public abstract object InvokeWithResult(object instance);
        public abstract object InvokeWithResult<T1>(object instance, T1 arg1);
        public abstract object InvokeWithResult<T1, T2>(object instance, T1 arg1, T2 arg2);
        public abstract object InvokeWithResult<T1, T2, T3>(object instance, T1 arg1, T2 arg2, T3 arg3);
        public abstract object InvokeWithResult<T1, T2, T3, T4>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
        public abstract object InvokeWithResult<T1, T2, T3, T4, T5>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

        public abstract void Invoke(object instance);
        public abstract void Invoke<T1>(object instance, T1 arg1);
        public abstract void Invoke<T1, T2>(object instance, T1 arg1, T2 arg2);
        public abstract void Invoke<T1, T2, T3>(object instance, T1 arg1, T2 arg2, T3 arg3);
        public abstract void Invoke<T1, T2, T3, T4>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
        public abstract void Invoke<T1, T2, T3, T4, T5>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }

}
