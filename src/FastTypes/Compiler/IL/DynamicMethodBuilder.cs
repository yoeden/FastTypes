using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FastTypes.Compiler.IL
{
    internal sealed class DynamicMethodBuilder
    {
        private Type _returnType;
        private Type[] _parameters;
        private Action<FluentIL, MethodInfo> _body;
        private string _name;

        public DynamicMethodBuilder()
        {
            _name = string.Empty;
            _returnType = typeof(void);
            _parameters = Type.EmptyTypes;

            _body = static (il, _) =>
            {
                il.Nop("No body was provided");
                il.Return();
            };
        }

        public DynamicMethodBuilder WithReturnType(Type returnType)
        {
            _returnType = returnType;
            return this;
        }

        public DynamicMethodBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public DynamicMethodBuilder WithParameters(params Type[] parameters)
        {
            _parameters = parameters;
            return this;
        }

        public DynamicMethodBuilder WithThisPointer(Type declaringType)
        {
            var parameters = new Type[_parameters.Length + 1];
            parameters[0] = declaringType;
            for (int i = 0; i < _parameters.Length; i++)
            {
                parameters[i + 1] = _parameters[i];
            }

            _parameters = parameters;

            return this;
        }

        public DynamicMethodBuilder WithParameters(params ParameterInfo[] parameters)
        {
            _parameters = new Type[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                _parameters[i] = parameters[i].ParameterType;
            }

            return this;
        }

        public DynamicMethodBuilder WithBody(Action<FluentIL, MethodInfo> body)
        {
            _body = body;
            return this;
        }

        public Delegate Compile()
        {
            var method = new DynamicMethod(_name, _returnType, _parameters);

            var il = method.GetILGenerator();
#if DEBUG
            var fluentIL = new PrintableFluentIL(il);
            _body(fluentIL, method);

            Debug.WriteLine($"{_returnType.Name} {_name}({string.Join(", ", _parameters.Select((t, i) => $"{t.Name} {i}"))}): ");
            Debug.WriteLine(fluentIL.GetIL());
#else
            var fluentIL = new FluentIL(il);
            _body(fluentIL, method);
#endif

            return CreateDelegate(method, _parameters);
        }

        private static Delegate CreateDelegate(MethodInfo method, Type[] args)
        {
            if (method.ReturnType == typeof(void))
            {
                switch (args.Length)
                {
                    case 0:
                        return method.CreateDelegate(typeof(Action));
                    case 1:
                        return method.CreateDelegate(typeof(Action<>).MakeGenericType(args));
                    case 2:
                        return method.CreateDelegate(typeof(Action<,>).MakeGenericType(args));
                    case 3:
                        return method.CreateDelegate(typeof(Action<,,>).MakeGenericType(args));
                    case 4:
                        return method.CreateDelegate(typeof(Action<,,,>).MakeGenericType(args));
                    case 5:
                        return method.CreateDelegate(typeof(Action<,,,,>).MakeGenericType(args));
                    case 6:
                        return method.CreateDelegate(typeof(Action<,,,,,>).MakeGenericType(args));
                    default:
                        throw new NotSupportedException();
                }
            }
            else
            {
                var genericArgs = new Type[args.Length + 1];
                Array.Copy(args, 0, genericArgs, 0, args.Length);
                genericArgs[^1] = method.ReturnType;
                switch (args.Length)
                {
                    case 0:
                        return method.CreateDelegate(typeof(Func<>).MakeGenericType(genericArgs));
                    case 1:
                        return method.CreateDelegate(typeof(Func<,>).MakeGenericType(genericArgs));
                    case 2:
                        return method.CreateDelegate(typeof(Func<,,>).MakeGenericType(genericArgs));
                    case 3:
                        return method.CreateDelegate(typeof(Func<,,,>).MakeGenericType(genericArgs));
                    case 4:
                        return method.CreateDelegate(typeof(Func<,,,,>).MakeGenericType(genericArgs));
                    case 5:
                        return method.CreateDelegate(typeof(Func<,,,,,>).MakeGenericType(genericArgs));
                    case 6:
                        return method.CreateDelegate(typeof(Func<,,,,,,>).MakeGenericType(genericArgs));
                    default:
                        throw new NotSupportedException();
                }
            }

        }
    }
}
