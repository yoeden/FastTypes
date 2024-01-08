using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FastTypes.Compiler;

namespace FastTypes.Reflection
{
    public partial class FastMethod<TType>
    {
        internal static FastMethod<TType> Create(MethodInfo info)
        {
            return new FastMethod<TType>(
                ReflectionCompiler.Compiler.Method(info),
                info.ReturnType == typeof(void) ? null : ReflectionCompiler.Compiler.MethodReturnObject(info),
                info);
        }
    }
}
