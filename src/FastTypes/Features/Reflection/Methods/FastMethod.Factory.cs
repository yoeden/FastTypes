using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FastTypes.Features.Reflection.Compiler;

namespace FastTypes.Features.Reflection.Methods
{
    public partial class FastMethod<TType>
    {
        internal static FastMethod<TType> Create(MethodInfo info)
        {
            return new FastMethod<TType>(
                ReflectionCompiler.Compiler.Method(info),
                info.ReturnType == typeof(void) ? null :  ReflectionCompiler.Compiler.MethodReturnObject(info),
                info);

            // Define the parameter expression for the instance of type TType
            var instanceParameter = Expression.Parameter(typeof(TType), "__instance_");

            //
            var parameters = info.GetParameters();
            var arguments = new List<Expression>(parameters.Length);
            var compiledArguments = new List<ParameterExpression>(parameters.Length + 1)
            {
                //
                instanceParameter
            };

            //
            for (var i = 0; i < arguments.Count; i++)
            {
                var p = Expression.Parameter(parameters[i].ParameterType, parameters[i].Name);

                arguments[i] = p;
                compiledArguments[i + 1] = p;
            }

            //
            var call = Expression.Call(info.IsStatic ? null : instanceParameter, info, arguments);

            // Compile the method call expression into a delegate
            var @delegate = Expression.Lambda(call, compiledArguments).Compile();

            //
            var returnAsObject = info.ReturnType == typeof(void) ? null : Expression.Lambda(Expression.Convert(call, typeof(object)), compiledArguments).Compile();

            // Create and return a new instance of the FastMethod<TType> class
            return new FastMethod<TType>(@delegate, returnAsObject, info);
        }
    }
}
