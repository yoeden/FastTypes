using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FastTypes.Compiler
{
    /// <summary>
    /// Expression tree compiler
    /// </summary>
    internal sealed class ExpressionTreeCompiler : IReflectionCompiler
    {
        /// <inheritdoc />
        public Delegate Setter(PropertyInfo property)
        {
            var instance = Expression.Parameter(property.DeclaringType, "instance");
            var value = Expression.Parameter(property.PropertyType, "value");
            return Expression.Lambda(typeof(Action<,>).MakeGenericType(property.DeclaringType, property.PropertyType), Expression.Call(property.SetMethod.IsStatic ? null : instance, property.SetMethod, value), instance, value).Compile();
        }

        /// <inheritdoc />
        public Delegate SetterWithObjectParameter(PropertyInfo property)
        {
            var instance = Expression.Parameter(property.DeclaringType, "instance");
            var value = Expression.Parameter(typeof(object), "value");
            return Expression.Lambda(typeof(Action<,>).MakeGenericType(property.DeclaringType, typeof(object)), Expression.Call(property.SetMethod.IsStatic ? null : instance, property.SetMethod, Expression.Convert(value, property.PropertyType)), instance, value).Compile();
        }

        /// <inheritdoc />
        public Delegate Getter(PropertyInfo property)
        {
            var instance = Expression.Parameter(property.DeclaringType, "instance");
            var lambdaType = typeof(Func<,>).MakeGenericType(property.DeclaringType, property.PropertyType);
            return Expression.Lambda(lambdaType, Expression.Call(property.GetMethod.IsStatic ? null : instance, property.GetMethod), instance).Compile();
        }

        /// <inheritdoc />
        public Delegate GetterWithObjectReturn(PropertyInfo property)
        {
            var instance = Expression.Parameter(property.DeclaringType, "instance");
            return Expression.Lambda(typeof(Func<,>).MakeGenericType(property.DeclaringType, typeof(object)), Expression.Convert(Expression.Call(property.GetMethod.IsStatic ? null : instance, property.GetMethod), typeof(object)), instance).Compile();
        }

        /// <inheritdoc />
        public Delegate Method(MethodInfo info)
        {
            var instance = Expression.Parameter(info.DeclaringType, "instance");
            var parameters = info.GetParameters();

            var args = new ParameterExpression[parameters.Length];
            var compileArgs = new ParameterExpression[parameters.Length + 1];
            compileArgs[0] = instance;

            for (int i = 0; i < args.Length; i++)
            {
                var p = Expression.Parameter(parameters[i].ParameterType, $"i{i}");
                args[i] = p;
                compileArgs[i + 1] = p;
            }

            var call = Expression.Call(info.IsStatic ? null : instance, info, args);
            return Expression.Lambda(call, compileArgs).Compile();
        }

        /// <inheritdoc />
        public Delegate MethodReturnObject(MethodInfo info)
        {
            var instance = Expression.Parameter(info.DeclaringType, "instance");
            var parameters = info.GetParameters();

            var args = new ParameterExpression[parameters.Length];
            var compileArgs = new ParameterExpression[parameters.Length + 1];
            compileArgs[0] = instance;

            for (int i = 0; i < args.Length; i++)
            {
                var p = Expression.Parameter(parameters[i].ParameterType, $"i{i}");
                args[i] = p;
                compileArgs[i + 1] = p;
            }

            var call = Expression.Call(info.IsStatic ? null : instance, info, args);
            return Expression.Lambda(Expression.Convert(call, typeof(object)), compileArgs).Compile();
        }

        /// <inheritdoc />
        public Delegate Activator(ConstructorInfo info)
        {
            var parameters = info.GetParameters();
            var args = new ParameterExpression[parameters.Length];

            for (int i = 0; i < args.Length; i++)
            {
                args[i] = Expression.Parameter(parameters[i].ParameterType);
            }

            return Expression.Lambda(Expression.New(info, args), args).Compile();
        }
    }
}
