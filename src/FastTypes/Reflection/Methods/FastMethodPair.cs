using System;
using System.Reflection;
using FastTypes.Compiler;
using FastTypes.Compiler.IL;

namespace FastTypes.Reflection
{
    public readonly struct FastMethodPair
    {
        public static BaseFastMethod CreateWithReturn<TType>(MethodInfo target)
        {
            var d = new DynamicMethodBuilder()
                .WithName($"{target.Name}")
                .WithReturnType(target.ReturnType)
                .WithParameters(target.GetParameters())
                .WithThisPointer(target.DeclaringType)
                .WithBody((il, info) => il
                    .LoadAllArguments(info)
                    .Call(target)
                    .Return())
                .Compile();

            //TODO: Compile il for this and cache it ??
            return (BaseFastMethod)typeof(FastMethod<,>)
                .MakeGenericType(typeof(TType), target.ReturnType)
                .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, new Type[] { typeof(MethodInfo), typeof(Delegate) }, null)
                .Invoke(new object[] { target, d });
        }

        public static BaseFastMethod CreateWithReturnObject<TType>(MethodInfo target)
        {
            var d = new DynamicMethodBuilder()
                .WithName($"{target.Name}")
                .WithReturnType(typeof(object))
                .WithParameters(target.GetParameters())
                .WithThisPointer(target.DeclaringType)
                .WithBody((il, info) => il
                    .LoadAllArguments(info)
                    .Call(target)
                    .BoxIfNeeded(target.ReturnType)
                    .Return())
                .Compile();

            return new FastMethod<TType>(target, d);
        }

        public static BaseFastMethod CreateVoid<TType>(MethodInfo target)
        {
            var d = new DynamicMethodBuilder()
                .WithName($"{target.Name}")
                .WithReturnType(typeof(object))
                .WithParameters(target.GetParameters())
                .WithThisPointer(target.DeclaringType)
                .WithBody((il, info) => il
                    .LoadAllArguments(info)
                    .Call(target)
                    .LoadNull()
                    .Return())
                .Compile();

            return new FastMethod<TType>(target, d);
        }
    }
}