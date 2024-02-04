using System;
using FastTypes.Clone;
using FastTypes.Query;
using FastTypes.Reflection;

namespace FastTypes
{
    internal static class ThrowHelper
    {
        public static void UnexpectedPropertyType<TType>(Type type, FastProperty<TType> fastProperty) => throw new UnexpectedMemberType(type, fastProperty.PropertyType);

        public static void UnexpectedFieldType(Type expectedType, Type givenType, string name) => throw new UnexpectedFieldTypeException(expectedType, givenType, name);

        public static void NoSetterFound<TType>(FastProperty<TType> fastProperty) => throw new NoSetterFoundException(fastProperty.Name);

        public static void NoGetterFound<TType>(FastProperty<TType> fastProperty) => throw new NoGetterFoundException(fastProperty.Name);

        public static void InstanceCantBeNullOfNonStaticMembers(string name) => throw new InstanceNullException(name);

        public static void UnexpectedMethodParameters(Type expected, Type given) => throw new UnexpectedMethodParametersException(expected, given);

        public static void UnexpectedReturnType(string name, Type returnType, Type expectedType) => throw new UnexpectedMethodReturnTypeException(name, returnType, expectedType);

        public static void NoSuitableConstructorFound(Type expected) => throw new NoSuitableConstructorFound(expected);

        public static void FailedToInstanciateType(Type type, Exception exception) => throw new FailedToInstanciateTypeException(type, exception);

        public static void NoDefaultConstructorFound(Type t) => throw new NoDefaultConstructorFoundException(t);

        public static void ProducerConsumerConstructor(Type t) => throw new ProducerConsumerConstructorException(t);

        public static void MethodDoesntExists(Type owner, string name) => throw new MethodDoesntExistsException(owner, name);

        public static void FieldNotFound(string name)
        {
            throw new FieldNotFoundException(name);
        }
    }
}
