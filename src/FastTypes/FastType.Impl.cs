using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastTypes.DataStructures;
using FastTypes.Reflection;

namespace FastTypes
{
    public sealed class FastType<TType> : IFastType<TType>
    {
        private readonly FastActivator<TType> _activator;
        private readonly UnmodifiableFastDictionaryByName<FastProperty<TType>> _props;
        private readonly UnmodifiableFastDictionaryByName<FastMethod<TType>> _methods;

        internal FastType()
        {
            var type = typeof(TType);

            //
            _props = PrepareProperties(type);

            //
            _methods = PrepareMethods(type);

            //
            _activator = FastActivator<TType>.Factory();
        }

        private static UnmodifiableFastDictionaryByName<FastMethod<TType>> PrepareMethods(Type type)
        {
            var methods = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(info => !info.IsSpecialName && info.DeclaringType != typeof(object))
                .ToArray();

            var fastMethods = new KeyValuePair<string, FastMethod<TType>>[methods.Length];
            for (int i = 0; i < methods.Length; i++)
            {
                fastMethods[i] = new KeyValuePair<string, FastMethod<TType>>(methods[i].Name, FastMethod<TType>.Create(methods[i]));
            }

            return UnmodifiableFastDictionaryByName<FastMethod<TType>>.Create(fastMethods);
        }

        private static UnmodifiableFastDictionaryByName<FastProperty<TType>> PrepareProperties(Type type)
        {
            var props = type.GetProperties();
            var fastProps = new KeyValuePair<string, FastProperty<TType>>[props.Length];
            for (int i = 0; i < props.Length; i++)
            {
                fastProps[i] = new KeyValuePair<string, FastProperty<TType>>(props[i].Name, FastProperty<TType>.Create(props[i]));
            }

            return UnmodifiableFastDictionaryByName<FastProperty<TType>>.Create(fastProps);

        }

        public Type Type => typeof(TType);

        public IEnumerable<FastProperty<TType>> Properties() => _props.Values;

        public FastMethod<TType> Method(string name) => _methods[name];

        public FastActivator<TType> Activator() => _activator;

        public FastProperty<TType> Property(string prop) => _props[prop];

        //

        FastProperty IFastType.Property(string prop) => Property(prop);

        IEnumerable<FastProperty> IFastType.Properties() => Properties();

        FastMethod IFastType.Method(string name) => Method(name);

        FastActivator IFastType.Activator() => Activator();
    }
}
