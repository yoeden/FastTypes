namespace FastTypes.Reflection
{
    public partial class FastMethod<TType>
    {
        /// <inheritdoc />
        public override TResult InvokeWithResult<TResult>(object instance)
        {
            return InvokeWithResult<TResult>((TType)instance);
        }

        /// <inheritdoc />
		public override TResult InvokeWithResult<T1, TResult>(object instance, T1 arg1)
        {
            return InvokeWithResult<T1, TResult>((TType)instance, arg1);
        }

        /// <inheritdoc />
		public override TResult InvokeWithResult<T1, T2, TResult>(object instance, T1 arg1, T2 arg2)
        {
            return InvokeWithResult<T1, T2, TResult>((TType)instance, arg1, arg2);
        }

        /// <inheritdoc />
		public override TResult InvokeWithResult<T1, T2, T3, TResult>(object instance, T1 arg1, T2 arg2, T3 arg3)
        {
            return InvokeWithResult<T1, T2, T3, TResult>((TType)instance, arg1, arg2, arg3);
        }

        /// <inheritdoc />
		public override TResult InvokeWithResult<T1, T2, T3, T4, TResult>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return InvokeWithResult<T1, T2, T3, T4, TResult>((TType)instance, arg1, arg2, arg3, arg4);
        }

        /// <inheritdoc />
		public override TResult InvokeWithResult<T1, T2, T3, T4, T5, TResult>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return InvokeWithResult<T1, T2, T3, T4, T5, TResult>((TType)instance, arg1, arg2, arg3, arg4, arg5);
        }

        /// <inheritdoc />
		public override object InvokeWithResult(object instance)
        {
            return InvokeWithResult((TType)instance);
        }

        /// <inheritdoc />
		public override object InvokeWithResult<T1>(object instance, T1 arg1)
        {
            return InvokeWithResult((TType)instance, arg1);
        }

        /// <inheritdoc />
		public override object InvokeWithResult<T1, T2>(object instance, T1 arg1, T2 arg2)
        {
            return InvokeWithResult((TType)instance, arg1, arg2);
        }

        /// <inheritdoc />
		public override object InvokeWithResult<T1, T2, T3>(object instance, T1 arg1, T2 arg2, T3 arg3)
        {
            return InvokeWithResult((TType)instance, arg1, arg2, arg3);
        }

        /// <inheritdoc />
		public override object InvokeWithResult<T1, T2, T3, T4>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return InvokeWithResult((TType)instance, arg1, arg2, arg3, arg4);
        }

        /// <inheritdoc />
		public override object InvokeWithResult<T1, T2, T3, T4, T5>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return InvokeWithResult((TType)instance, arg1, arg2, arg3, arg4, arg5);
        }
    }
}
