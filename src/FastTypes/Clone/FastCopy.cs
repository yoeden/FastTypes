using System;
using System.Linq;
using System.Reflection;

namespace FastTypes.Clone
{
    // Fun hack to gain near runtime performance, just keep all the type related stuff inside a static generic class of the same type
    internal static class FastCopy<T>
    {
        private static readonly Func<object, object> ObjectCopyFunc;
        private static readonly Func<T, T> CopyFunc;

        static FastCopy()
        {
            CopyFunc = (Func<T, T>)CopyCompiler.Compile(typeof(T));
            ObjectCopyFunc = CopyCompiler.CompileObjectSignature(typeof(T), FastCopy.GenericDeepCopy.MakeGenericMethod(typeof(T)));
        }

        public static T DeepCopy(T src) => CopyFunc(src);

        public static object DeepCopyObject(object src) => ObjectCopyFunc(src);
    }

    internal static class FastCopy
    {
        internal static MethodInfo GenericDeepCopy = typeof(FastCopy)
            .GetMethods()
            .First(info => info.Name == nameof(DeepCopy) && info.IsGenericMethodDefinition);

        public static object DeepCopy(object src)
        {
            if (src == null) return default;

            //TODO: Cache it ?
            return typeof(FastCopy<>)
                .MakeGenericType(src.GetType())
                .GetMethod(nameof(FastCopy<CopyTarget>.DeepCopyObject))
                .Invoke(null, new[] { src });
        }

        public static T DeepCopy<T>(T src)
        {
            if (src == null) return default;
            return FastCopy<T>.DeepCopy(src);
        }
    }
}
