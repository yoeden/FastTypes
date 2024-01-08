using System;
using System.Reflection;

namespace FastTypes.Compiler
{
    internal interface IReflectionCompiler
    {
        Delegate Setter(PropertyInfo property);

        Delegate SetterWithObjectParameter(PropertyInfo property);

        Delegate Getter(PropertyInfo property);

        Delegate GetterWithObjectReturn(PropertyInfo property);

        Delegate Method(MethodInfo info);

        Delegate MethodReturnObject(MethodInfo info);

        Delegate Activator(ConstructorInfo info);
    }
}
