using System;
using FastTypes.Features.Reflection.Properties;

namespace FastTypes
{
    internal static class ThrowHelper
    {
        public static void UnexpectedPropertyType<TType>(Type type, FastProperty<TType> fastProperty)
        {
            throw new UnexpectedMemberType(type, fastProperty.PropertyType);
        }

        public static void NoSetterFound<TType>(FastProperty<TType> fastProperty)
        {
            throw new NoSetterFoundException(fastProperty.Name);
        }

        public static void NoGetterFound<TType>(FastProperty<TType> fastProperty)
        {
            throw new NoGetterFoundException(fastProperty.Name);
        }

        public static void InstanceCantBeNullOfNonStaticMembers(string name)
        {
            throw new InstanceNullException(name);
        }

        public static void UnexpectedMethodSignature(Type method, Type given)
        {
            throw new UnexpectedMethodSignatureException(method, given);
        }

        public static void MethodExpectedVoid(string name, Type returnType)
        {
            throw new MethodIsVoidButExpectedReturnException(name, returnType);
        }

        public static void NoSuitableConstructorFound(Type expected)
        {
            throw new NoSuitableConstructorFound(expected);
        }
    }

    public sealed class NoSuitableConstructorFound : InvalidOperationException
    {
        public NoSuitableConstructorFound(Type t) : base($"No suitable constructor found that matching : {t}")
        {

        }
    }

    public sealed class UnexpectedMethodSignatureException : InvalidOperationException
    {
        public UnexpectedMethodSignatureException(Type method, Type given) : base($"Invalid invocation, method is with signature '{method}' but tried invoke as '{(given == null ? "null" : given)}'")
        {

        }
    }

    public sealed class MethodIsVoidButExpectedReturnException : InvalidOperationException
    {
        public MethodIsVoidButExpectedReturnException(string name, Type returnType) : base($"{name} is void, but invocation expected return return {returnType.Name} (Func<..,{returnType.Name}>)")
        {

        }
    }

    public sealed class InstanceNullException : InvalidOperationException
    {
        public InstanceNullException(string target) : base($"Instance cannot be null for non-static members ({target}).")
        {

        }
    }

    public sealed class UnexpectedMemberType : InvalidOperationException
    {
        public UnexpectedMemberType(Type given, Type expected)
            : base($"Member expected type '{expected.Name}' but received '{given.Name}'")
        {

        }
    }

    public sealed class NoGetterFoundException : InvalidOperationException
    {
        public NoGetterFoundException(string name) : base($"No getter found for property '{name}'")
        {

        }
    }

    public sealed class NoSetterFoundException : InvalidOperationException
    {
        public NoSetterFoundException(string name) : base($"No setter found for property '{name}'")
        {

        }
    }
}
