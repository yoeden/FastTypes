namespace FastTypes.Reflection
{
    public partial class FastMethod<TType>
    {
        public override void Invoke(object instance)
        {
            Invoke((TType)instance);
        }

        public override void Invoke<T>(object instance, T arg1)
        {
            Invoke((TType)instance, arg1);
        }

        public override void Invoke<T1, T2>(object instance, T1 arg1, T2 arg2)
        {
            Invoke((TType)instance, arg1, arg2);
        }

        public override void Invoke<T1, T2, T3>(object instance, T1 arg1, T2 arg2, T3 arg3)
        {
            Invoke((TType)instance, arg1, arg2, arg3);
        }

        public override void Invoke<T1, T2, T3, T4>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Invoke((TType)instance, arg1, arg2, arg3, arg4);
        }

        public override void Invoke<T1, T2, T3, T4, T5>(object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            Invoke((TType)instance, arg1, arg2, arg3, arg4, arg5);
        }
    }
}
