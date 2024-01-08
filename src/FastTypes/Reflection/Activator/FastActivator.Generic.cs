using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FastTypes.DataStructures;

namespace FastTypes.Reflection
{
    /// <summary>
    /// This class provides support for instanciating a given Type.
    /// </summary>
    /// <typeparam name="TType">Reflected Type</typeparam>
    public sealed class FastActivator<TType> : FastActivator
    {
        internal static FastActivator<TType> Factory()
        {
            var ctors = typeof(TType).GetConstructors();
            var result = new KeyValuePair<int, Delegate>[ctors.Length];
            for (var i = 0; i < ctors.Length; i++)
            {
                ConstructorInfo ctor = ctors[i];
                var @delegate = ctor.GetParameters().Length == 0 ? Expression.Lambda(Expression.New(ctor)).Compile() : ReflectionCompiler.Compiler.Activator(ctor);
                var key = TypesHashCode.Hash(ctor.GetParameters().Select(p => p.ParameterType).ToArray());

                result[i] = new KeyValuePair<int, Delegate>(key, @delegate);
            }

            return new FastActivator<TType>(UnmodifiableFastDictionaryByInt<Delegate>.Create(result));
        }

        private readonly UnmodifiableFastDictionaryByInt<Delegate> _ctors;
        private readonly Func<TType> _fastCtor;

        private FastActivator(UnmodifiableFastDictionaryByInt<Delegate> ctors)
        {
            _ctors = ctors;
            _fastCtor = ctors.ContainsKey(0) ? (Func<TType>)ctors[0] : null;
        }

        /// <inheritdoc />
        public override object NewObject()
        {
            return New();
        }

        /// <inheritdoc />
        public override object NewObject<T1>(T1 arg1)
        {
            return New(arg1);
        }

        /// <inheritdoc />
        public override object NewObject<T1, T2>(T1 arg1, T2 arg2)
        {
            return New(arg1, arg2);
        }

        /// <inheritdoc />
        public override object NewObject<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
        {
            return New(arg1, arg2, arg3);
        }

        /// <inheritdoc />
        public override object NewObject<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return New(arg1, arg2, arg3, arg4);
        }

        /// <inheritdoc />
        public override object NewObject<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return New(arg1, arg2, arg3, arg4, arg5);
        }

        /// <summary>
        /// Invokes the public empty constructor.
        /// </summary>
        /// <returns>A new instance of current Type as <typeparamref name="TType"/></returns>
        public TType New()
        {
            if (_fastCtor == null) ThrowHelper.NoSuitableConstructorFound(typeof(Func<TType>));
            return _fastCtor();
        }

        /// <summary>
        /// Creates a new instance of the specified type using 1 argument constructor.
        /// </summary>
        /// <returns>A new instance of current Type as <typeparamref name="TType"/></returns>
        public TType New<T1>(T1 arg1)
        {
            var key = TypesHashCode.Hash<T1>();
            if (!_ctors.ContainsKey(key)) ThrowHelper.NoSuitableConstructorFound(typeof(Func<T1, TType>));

            return ((Func<T1, TType>)_ctors[key])(arg1);
        }

        /// <summary>
        /// Creates a new instance of the specified type using 2 arguments constructor.
        /// </summary>
        /// <returns>A new instance of current Type as <typeparamref name="TType"/></returns>
        public TType New<T1, T2>(T1 arg1, T2 arg2)
        {
            var key = TypesHashCode.Hash<T1, T2>();
            if (!_ctors.ContainsKey(key)) ThrowHelper.NoSuitableConstructorFound(typeof(Func<T1, T2, TType>));

            return ((Func<T1, T2, TType>)_ctors[key])(arg1, arg2);
        }

        /// <summary>
        /// Creates a new instance of the specified type using 3 arguments constructor.
        /// </summary>
        /// <returns>A new instance of current Type as <typeparamref name="TType"/></returns>
        public TType New<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
        {
            var key = TypesHashCode.Hash<T1, T2, T3>();
            if (!_ctors.ContainsKey(key)) ThrowHelper.NoSuitableConstructorFound(typeof(Func<T1, T2, T3, TType>));

            return ((Func<T1, T2, T3, TType>)_ctors[key])(arg1, arg2, arg3);
        }

        /// <summary>
        /// Creates a new instance of the specified type using 4 arguments constructor.
        /// </summary>
        /// <returns>A new instance of current Type as <typeparamref name="TType"/></returns>
        public TType New<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var key = TypesHashCode.Hash<T1, T2, T3, T4>();
            if (!_ctors.ContainsKey(key)) ThrowHelper.NoSuitableConstructorFound(typeof(Func<T1, T2, T3, T4, TType>));

            return ((Func<T1, T2, T3, T4, TType>)_ctors[key])(arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Creates a new instance of the specified type using 5 arguments constructor.
        /// </summary>
        /// <returns>A new instance of current Type as <typeparamref name="TType"/></returns>
        public TType New<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var key = TypesHashCode.Hash<T1, T2, T3, T4, T5>();
            if (!_ctors.ContainsKey(key)) ThrowHelper.NoSuitableConstructorFound(typeof(Func<T1, T2, T3, T4, T5, TType>));

            return ((Func<T1, T2, T3, T4, T5, TType>)_ctors[key])(arg1, arg2, arg3, arg4, arg5);
        }
    }
}
