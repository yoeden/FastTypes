using System;
using System.Reflection;
using System.Reflection.Emit;

namespace FastTypes.Compiler
{
    internal sealed class ILCompiler : IReflectionCompiler
    {
        /// <inheritdoc />
        public Delegate Setter(PropertyInfo property)
        {
            //
            DynamicMethod method = new(
                $"__SETTER_{property.Name}",
                null,
                new[] { property.DeclaringType, property.PropertyType });

            //
            var il = method.GetILGenerator();

            //Load the instance argument
            il.Emit(OpCodes.Ldarg_0);

            //Load the actual value
            il.Emit(OpCodes.Ldarg_1);

            //Call the set method
            il.Emit(OpCodes.Callvirt, property.SetMethod);

            //Return
            il.Emit(OpCodes.Ret);

            //
            return method.CreateDelegate(typeof(Action<,>).MakeGenericType(property.DeclaringType, property.PropertyType));
        }

        /// <inheritdoc />
        public Delegate SetterWithObjectParameter(PropertyInfo property)
        {
            //
            var method = new DynamicMethod(
                $"__SETTER_{property.Name}",
                null,
                new Type[] { property.DeclaringType, typeof(object) });

            //
            var il = method.GetILGenerator();

            //Load the instance argument
            il.Emit(OpCodes.Ldarg_0);

            //Load the actual value
            il.Emit(OpCodes.Ldarg_1);

            //We received object, if the original value was value type we need to unbox it
            if (property.PropertyType.IsValueType) il.Emit(OpCodes.Unbox_Any, property.PropertyType);

            //If the original value is a reference, we can simply cast it
            else il.Emit(OpCodes.Castclass, property.PropertyType);

            //Call the set method
            il.Emit(OpCodes.Callvirt, property.SetMethod);
            //Return
            il.Emit(OpCodes.Ret);

            //
            return method.CreateDelegate(typeof(Action<,>).MakeGenericType(property.DeclaringType, typeof(object)));
        }

        /// <inheritdoc />
        public Delegate Getter(PropertyInfo property)
        {
            //
            DynamicMethod method = new DynamicMethod(
                $"__GETTER_{property.Name}",
                property.PropertyType,
                new Type[] { property.DeclaringType });

            //
            var il = method.GetILGenerator();

            //Load the instance argument
            il.Emit(OpCodes.Ldarg_0);

            //Call the get method
            il.Emit(OpCodes.Callvirt, property.GetMethod);

            //Return
            il.Emit(OpCodes.Ret);

            //
            return method.CreateDelegate(typeof(Func<,>).MakeGenericType(property.DeclaringType, property.PropertyType));
        }

        /// <inheritdoc />
        public Delegate GetterWithObjectReturn(PropertyInfo property)
        {
            //
            var method = new DynamicMethod(
                $"__GETTER_{property.Name}",
                typeof(object),
                new Type[] { property.DeclaringType });

            //
            var il = method.GetILGenerator();

            //Load the instance argument
            il.Emit(OpCodes.Ldarg_0);

            //Call the get method
            il.Emit(OpCodes.Callvirt, property.GetMethod);

            //Since we want to return object, we need to box a value type
            if (property.PropertyType.IsValueType) il.Emit(OpCodes.Box, property.PropertyType);

            //Return
            il.Emit(OpCodes.Ret);

            //
            return method.CreateDelegate(typeof(Func<,>).MakeGenericType(property.DeclaringType, typeof(object)));
        }

        /// <inheritdoc />
        public Delegate Method(MethodInfo info)
        {
            //
            Type[] args = PrepareMethodArgs(info);

            //
            var method = new DynamicMethod(
                $"__GEN_{info.Name}",
                info.ReturnType,
                args);

            //
            var il = method.GetILGenerator();

            //
            EmitLoadArgs(args, il);

            //
            il.Emit(OpCodes.Callvirt, info);

            //
            il.Emit(OpCodes.Ret);

            //
            if (info.ReturnType == typeof(void))
            {
                return CreateVoidDelegate(method, args);
            }
            else
            {
                return CreateTDelegate(method, args);
            }
        }

        /// <inheritdoc />
        public Delegate MethodReturnObject(MethodInfo info)
        {
            //
            if (info.ReturnType == typeof(void)) throw new InvalidOperationException();

            //
            Type[] args = PrepareMethodArgs(info);

            //
            var method = new DynamicMethod(
                $"__GEN_{info.Name}",
                typeof(object),
                args);

            //
            var il = method.GetILGenerator();

            //
            EmitLoadArgs(args, il);

            //
            il.Emit(OpCodes.Callvirt, info);

            //
            if (info.ReturnType.IsValueType) il.Emit(OpCodes.Box, info.ReturnType);

            //
            il.Emit(OpCodes.Ret);

            //
            return CreateTDelegate(method, args);
        }

        /// <inheritdoc />
        public Delegate Activator(ConstructorInfo info)
        {
            //
            var parameters = info.GetParameters();
            var args = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                args[i] = parameters[i].ParameterType;
            }

            //
            DynamicMethod method = new("", info.DeclaringType, args);

            //
            var il = method.GetILGenerator();

            //
            for (int i = 0; i < args.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_S, i);
            }

            //
            il.Emit(OpCodes.Newobj, info);

            //
            il.Emit(OpCodes.Ret);

            return CreateTDelegate(method, args);
        }

        private static void EmitLoadArgs(Type[] args, ILGenerator il)
        {
            //Order matters
            switch (args.Length)
            {
                //We always have a instance argument, even if in static method
                case 1:
                    //Always load instance
                    il.Emit(OpCodes.Ldarg_0);
                    break;
                case 2:
                    //Always load instance
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldarg_1);
                    break;
                case 3:
                    //Always load instance
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Ldarg_2);
                    break;
                case 4:
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Ldarg_2);
                    il.Emit(OpCodes.Ldarg_3);
                    break;
                default:
                    for (int i = 0; i < args.Length; i++)
                    {
                        il.Emit(OpCodes.Ldarg_S, i);
                    }

                    break;
            }
        }

        private static Type[] PrepareMethodArgs(MethodBase info)
        {
            var parameters = info.GetParameters();
            var args = new Type[parameters.Length + 1];
            args[0] = info.DeclaringType;
            for (int i = 0; i < parameters.Length; i++)
            {
                args[i + 1] = parameters[i].ParameterType;
            }

            return args;
        }

        private static Delegate CreateVoidDelegate(MethodInfo method, Type[] args)
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

        private static Delegate CreateTDelegate(MethodInfo method, Type[] args)
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