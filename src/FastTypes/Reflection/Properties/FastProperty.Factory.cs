using System;
using System.Reflection;
using FastTypes.Compiler;

namespace FastTypes.Reflection
{
    public partial class FastProperty<TType>
    {
        internal static FastProperty<TType> Create(PropertyInfo info)
        {
            Delegate getter = null;
            Delegate getterReturnAsObject = null;

            Delegate setter = null;
            Delegate setterAsObject = null;

            if (info.GetMethod != null && info.GetMethod.IsPublic)
            {
                getter = ReflectionCompiler.Compiler.Getter(info);
                getterReturnAsObject = ReflectionCompiler.Compiler.GetterWithObjectReturn(info);
                //getter = ReflectionILGenerator<TType>.CreateGetter(info);
            }
            if (info.SetMethod != null && info.SetMethod.IsPublic)
            {
                setter = ReflectionCompiler.Compiler.Setter(info);
                setterAsObject = ReflectionCompiler.Compiler.SetterWithObjectParameter(info);
                //setter = ReflectionILGenerator<TType>.CreateSetter(info);
            }

            return new FastProperty<TType>(getter, getterReturnAsObject, setter, setterAsObject ,info);
        }
    }
}
