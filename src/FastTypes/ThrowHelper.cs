using System;
using FastTypes.Query;
using FastTypes.Reflection;
using FastTypes.Reflection.Exceptions;

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

        public static void FailedToInstanciateType(Type type, Exception exception)
        {
            throw new FailedToInstanciateTypeException(type,exception);
        }
    }
}
