using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastTypes.DataStructures;

namespace FastTypes.Reflection
{
    internal sealed class FastType<TType> : IFastType<TType>
    {
        private readonly FastActivator<TType> _activator;
        private readonly UnmodifiableFastDictionaryByName<FastProperty<TType>> _props;

        private readonly UnmodifiableFastDictionaryByName<BaseFastField> _fieldsAsObjects;
        private readonly UnmodifiableFastDictionaryByName<BaseFastField> _fields;

        private readonly UnmodifiableFastDictionaryByName<BaseFastMethod> _voidMethods;
        private readonly UnmodifiableFastDictionaryByName<BaseFastMethod> _returnMethods;

        internal FastType()
        {
            //
            var type = typeof(TType);

            //
            _props = PrepareProperties(type);

            //
            (_voidMethods, _returnMethods) = PrepareMethods(type);

            //
            (_fieldsAsObjects, _fields) = PrepareFields(type);

            //
            _activator = FastActivator<TType>.Factory();
        }

        private static (UnmodifiableFastDictionaryByName<BaseFastMethod> voidMethods, UnmodifiableFastDictionaryByName<BaseFastMethod> returnableMethods) PrepareMethods(Type type)
        {
            //TODO: Measure creation time of FastType
            //TODO: Get rid of LINQ
            var methods = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(info => !info.IsSpecialName && info.DeclaringType != typeof(object))
                .ToArray();

            var voidMethods = new KeyValuePair<string, BaseFastMethod>[methods.Length];
            var returnableMethods = new KeyValuePair<string, BaseFastMethod>[methods.Length];

            for (int i = 0; i < methods.Length; i++)
            {
                var method = methods[i];

                if (method.ReturnType == typeof(void))
                {
                    voidMethods[i] = new(method.Name, FastMethodPair.CreateVoid<TType>(method));
                    returnableMethods[i] = new(method.Name, null);
                }
                else
                {
                    voidMethods[i] = new(method.Name, FastMethodPair.CreateWithReturnObject<TType>(method));
                    returnableMethods[i] = new(method.Name, FastMethodPair.CreateWithReturn<TType>(method));
                }
            }

            return new(UnmodifiableFastDictionaryByName<BaseFastMethod>.Create(voidMethods), UnmodifiableFastDictionaryByName<BaseFastMethod>.Create(returnableMethods));
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

        private static (UnmodifiableFastDictionaryByName<BaseFastField> fieldsAsObjects, UnmodifiableFastDictionaryByName<BaseFastField> fields) PrepareFields(Type type)
        {
            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            var fastFieldsAsObjects = new KeyValuePair<string, BaseFastField>[fields.Length];
            var fastFields = new KeyValuePair<string, BaseFastField>[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                fastFields[i] = new KeyValuePair<string, BaseFastField>(fields[i].Name, FastFieldFactory.WithReturnType<TType>(field));
                fastFieldsAsObjects[i] = new KeyValuePair<string, BaseFastField>(fields[i].Name, FastFieldFactory.WithObjectType<TType>(field));
            }

            return (UnmodifiableFastDictionaryByName<BaseFastField>.Create(fastFieldsAsObjects), UnmodifiableFastDictionaryByName<BaseFastField>.Create(fastFields));
        }

        public Type Type => typeof(TType);

        public IEnumerable<FastProperty<TType>> Properties() => _props.Values;

        public FastMethod<TType> Method(string name)
        {
            if (!_voidMethods.ContainsKey(name)) ThrowHelper.MethodDoesntExists(Type, name);
            return (FastMethod<TType>)_voidMethods[name];
        }

        public FastMethod<TType, TResult> Method<TResult>(string name)
        {
            if (!_returnMethods.ContainsKey(name)) ThrowHelper.MethodDoesntExists(Type, name);

            var method = _returnMethods[name];
            if (method.ReturnType != typeof(TResult)) ThrowHelper.UnexpectedReturnType(name, typeof(TResult), method.ReturnType);

            return (FastMethod<TType, TResult>)method;
        }

        public FastField<TType> Field(string name)
        {
            if (!_fieldsAsObjects.ContainsKey(name)) ThrowHelper.FieldNotFound(name);

            return (FastField<TType>)_fieldsAsObjects[name];
        }

        public FastField<TType, TValue> Field<TValue>(string name)
        {
            if (!_fields.ContainsKey(name)) ThrowHelper.FieldNotFound(name);


            return (FastField<TType, TValue>)_fields[name];
        }

        public FastActivator<TType> Activator() => _activator;

        public FastProperty<TType> Property(string prop) => _props[prop];

        //

        FastProperty IFastType.Property(string prop) => Property(prop);

        IEnumerable<FastProperty> IFastType.Properties() => Properties();

        FastMethod IFastType.Method(string name) => Method(name);

        FastMethodWithResult<TResult> IFastType.Method<TResult>(string name) => Method<TResult>(name);

        FastField IFastType.Field(string name) => Field(name);

        FastFieldKnownValue<TValue> IFastType.Field<TValue>(string name) => Field<TValue>(name);

        FastActivator IFastType.Activator() => Activator();
    }
}
