using System;

namespace FastTypes.Reflection
{
    internal static class TypesHashCode
    {
        public static int Hash<T>() => typeof(ValueTuple<T>).GetHashCode();
        public static int Hash<T1, T2>() => typeof(ValueTuple<T1, T2>).GetHashCode();
        public static int Hash<T1, T2, T3>() => typeof(ValueTuple<T1, T2, T3>).GetHashCode();
        public static int Hash<T1, T2, T3, T4>() => typeof(ValueTuple<T1, T2, T3, T4>).GetHashCode();
        public static int Hash<T1, T2, T3, T4, T5>() => typeof(ValueTuple<T1, T2, T3, T4, T5>).GetHashCode();

        public static int Hash(params Type[] types)
        {
            //TODO: Possible bottleneck
            if (types.Length == 0) return 0;
            return GetValueType(types.Length).MakeGenericType(types).GetHashCode();

            static Type GetValueType(int n)
            {
                switch (n)
                {
                    case 1: return typeof(ValueTuple<>);
                    case 2: return typeof(ValueTuple<,>);
                    case 3: return typeof(ValueTuple<,,>);
                    case 4: return typeof(ValueTuple<,,,>);
                    case 5: return typeof(ValueTuple<,,,,>);
                    case 0:
                    default: throw new InvalidOperationException();
                }
            }
        }
    }
}