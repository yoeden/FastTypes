using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using FastTypes.Clone.AnotherTake;
using FastTypes.Clone.Metadata;
using FastTypes.Clone.Units;

namespace FastTypes.Clone
{
    public static class FastCopy<T>
    {
        private static readonly Func<object, object> _objectCopyFunc;
        private static readonly Func<T, T> _copyFunc;

        static FastCopy()
        {
            _copyFunc = (Func<T, T>)CopyCompiler.Compile(typeof(T));
            _objectCopyFunc = CopyCompiler.CompileObjectSignature(typeof(T), _copyFunc.Method);
        }

        public static T DeepCopy(T src)
        {
            if (src == null) return default;

            var type = src.GetType();
            if (type.IsPureType()) return src;

            return _copyFunc(src);
        }

        public static object DeepCopyObject(object src)
        {
            return _objectCopyFunc(src);
        }

    }

    public static class FastCopy
    {
        public static ImmutableDictionary<Type, Func<object, object>> _objectDelegates = ImmutableDictionary<Type, Func<object, object>>.Empty;
        public static ImmutableDictionary<Type, Delegate> _genericDelegates = ImmutableDictionary<Type, Delegate>.Empty;

        internal static MethodInfo GenericDeepCopy = typeof(FastCopy).GetMethods()
            .First(info => info.Name == nameof(DeepCopy) && info.IsGenericMethodDefinition);

        public static object DeepCopy(object src)
        {
            return typeof(FastCopy<>).MakeGenericType(src.GetType())
                .GetMethod(nameof(FastCopy<CopyTarget>.DeepCopyObject)).Invoke(null, new[] { src });
            //var type = src.GetType();
            //Func<object, object> func;

            //if (!_objectDelegates.ContainsKey(type))
            //{
            //    func = CopyCompiler.CompileObjectSignature(type, GenericDeepCopy.MakeGenericMethod(type));
            //    _objectDelegates = _objectDelegates.Add(type, func);
            //}
            //else
            //{
            //    func = _objectDelegates[type];
            //}

            //return func(src);
        }

        public static T DeepCopy<T>(T src)
        {
            return FastCopy<T>.DeepCopy(src);
            //if (src == null) return default;

            //var type = src.GetType();
            //if (type.IsPureType()) return src;

            //var func = (Func<T, T>)GetGenericDelegate(type);
            //return func(src);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Delegate GetGenericDelegate(Type type)
        {
            if (!_genericDelegates.ContainsKey(type)) _genericDelegates = _genericDelegates.Add(type, CopyCompiler.Compile(type));
            return _genericDelegates[type];
        }
    }

    internal static class CopyCompiler
    {
        internal static Func<object, object> CompileObjectSignature(Type t, MethodInfo methodInfo)
        {
            DynamicMethod method = new(
                "DeepCopy_Obj",
                typeof(object),
                new[] { typeof(object) }
            );

            var il = method.GetILGenerator();

            if (t.IsValueType)
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Unbox_Any, t);
                il.Emit(OpCodes.Call, methodInfo);
                il.Emit(OpCodes.Box, t);

                il.Emit(OpCodes.Ret);
            }
            else
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Castclass, t);
                il.Emit(OpCodes.Call, methodInfo);

                il.Emit(OpCodes.Ret);
            }

            return (Func<object, object>)method.CreateDelegate(typeof(Func<object, object>));
        }

        public static Delegate Compile(Type t)
        {
            //
            DynamicMethod method = new(
                "DeepCopy",
                t,
                new[] { t }
            );
            var il = method.GetILGenerator();

            CopyILEmitter.EmitCopy(il, t);

            var compiled = method.CreateDelegate(typeof(Func<,>).MakeGenericType(t, t));
            return compiled;
        }
    }

    //

    internal static class FieldInfoExt
    {
        public static bool IsBackingField(this FieldInfo info) =>
            info.Name.EndsWith("k__BackingField") &&
            info.GetCustomAttribute<CompilerGeneratedAttribute>() != null;

        public static bool IsBackingFieldOf(this FieldInfo info, PropertyInfo prop) =>
            info.Name.EndsWith($"<{prop.Name}>k__BackingField") &&
            info.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
    }

    public sealed class DeepCopySettings
    {
        //TODO:
        // Pitfals :
        //   Private members ?
        //   No empty ctor (either public or private)
        //   Readonly fields
        //   Readonly properties
        //   Write only properties
        //   Value objects composing reference types
        //   Dictionary
        //   Custom collections
    }
}
