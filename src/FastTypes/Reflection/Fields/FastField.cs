using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FastTypes.Compiler;
using FastTypes.Compiler.IL;

namespace FastTypes.Reflection
{
    public abstract class BaseFastField : FastMember
    {
        protected BaseFastField(FieldInfo info)
        {
            IsReadonly = info.IsInitOnly;
            IsPublic = info.IsPublic;
            Name = info.Name;
            Type = info.FieldType;
            IsBackingField = IsMemberBackingField(info);
        }

        public bool IsReadonly { get; }
        public bool IsPublic { get; }
        public override string Name { get; }
        public override bool IsStatic { get; }
        public Type Type { get; }
        public bool IsBackingField { get; }

        /// <summary>
        /// Throws if the given instance is null (valid only for non value types).
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="instance"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ThrowIfUnexpectedType(Type expectedType, Type givenType)
        {
            if (expectedType != givenType) ThrowHelper.UnexpectedFieldType(expectedType, givenType, Name);
        }

        private static bool IsMemberBackingField(MemberInfo info) =>
            info.Name.EndsWith("k__BackingField") &&
            info.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
    }


    public abstract class FastField : BaseFastField
    {
        protected FastField(FieldInfo info) : base(info)
        {

        }

        public abstract object GetValue(object instance);

        public abstract void SetValue(object instance, object value);
    }

    public class FastField<TType> : FastField
    {
        private readonly Lazy<Func<TType, object>> _get;
        private readonly Lazy<Action<TType, object>> _set;

        public FastField(FieldInfo field) : base(field)
        {
            _get = new Lazy<Func<TType, object>>((Func<TType, object>)new DynamicMethodBuilder()
                .WithParameters(typeof(TType))
                .WithReturnType(typeof(object))
                .WithName($"{field.Name}__Get")
                .WithBody((il, _) =>
                {
                    il
                        .LoadInstanceIfNeeded(field, 0)
                        .LoadField(field)
                        .BoxIfNeeded(field.FieldType)
                        .Return();

                })
                .Compile());

            _set = new Lazy<Action<TType, object>>((Action<TType, object>)new DynamicMethodBuilder()
                .WithParameters(typeof(TType), typeof(object))
                .WithName($"{field.Name}__Set")
                .WithBody((il, _) =>
                {
                    il
                        .LoadInstanceIfNeeded(field, 0)
                        .LoadArgument(1)
                        .UnBoxIfNeeded(field.FieldType)
                        .StoreField(field)
                        .Return();

                })
                .Compile());
        }

        public override object GetValue(object instance)
        {
            return GetValue((TType)instance);
        }

        public override void SetValue(object instance, object value)
        {
            SetValue((TType)instance, value);
        }

        public object GetValue(TType instance)
        {
            ThrowIfInstanceIsNull(instance);

            return _get.Value(instance);
        }

        public void SetValue(TType instance, object value)
        {
            ThrowIfInstanceIsNull(instance);
            ThrowIfUnexpectedType(Type, value.GetType());

            var set = _set.Value;
            set(instance, value);
        }
    }

    public abstract class FastFieldKnownValue<TValue> : BaseFastField
    {
        protected FastFieldKnownValue(FieldInfo info) : base(info)
        {

        }

        public abstract TValue GetValue(object instance);

        public abstract void SetValue(object instance, TValue value);
    }

    public class FastField<TType, TValue> : FastFieldKnownValue<TValue>
    {
        private readonly Lazy<Func<TType, TValue>> _get;
        private readonly Lazy<Action<TType, TValue>> _set;

        internal FastField(FieldInfo field) : base(field)
        {
            _get = new Lazy<Func<TType, TValue>>((Func<TType, TValue>)new DynamicMethodBuilder()
                .WithParameters(typeof(TType))
                .WithReturnType(typeof(TValue))
                .WithName($"{field.Name}__Get")
                .WithBody((il, _) =>
                {
                    il
                        .LoadInstanceIfNeeded(field, 0)
                        .LoadField(field)
                        .Return();

                })
                .Compile());

            _set = new Lazy<Action<TType, TValue>>((Action<TType, TValue>)new DynamicMethodBuilder()
                .WithParameters(typeof(TType), typeof(TValue))
                .WithName($"{field.Name}__Set")
                .WithBody((il, _) =>
                {
                    il
                        .LoadInstanceIfNeeded(field, 0)
                        .LoadArgument(1)
                        .StoreField(field)
                        .Return();

                })
                .Compile());
        }

        public override TValue GetValue(object instance) => GetValue((TType)instance);

        public override void SetValue(object instance, TValue value) => SetValue((TType)instance, value);

        public TValue GetValue(TType instance)
        {
            ThrowIfInstanceIsNull(instance);

            return _get.Value(instance);
        }

        public void SetValue(TType instance, TValue value)
        {
            ThrowIfInstanceIsNull(instance);

            _set.Value(instance, value);
        }
    }

    internal static class FastFieldFactory
    {
        public static BaseFastField WithReturnType<TType>(FieldInfo info)
        {
            var type = typeof(FastField<,>).MakeGenericType(typeof(TType), info.FieldType);
            return (BaseFastField)
                type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(FieldInfo) }, null)
                .Invoke(new[] { info });
        }

        public static BaseFastField WithObjectType<TType>(FieldInfo info)
        {
            return new FastField<TType>(info);
        }
    }
}
