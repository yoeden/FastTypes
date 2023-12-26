using System;
using System.Reflection;

namespace FastTypes.Features.Reflection.Properties
{
    public abstract class FastProperty : FastMember
    {
        public abstract bool HasSetter { get; }
        public abstract bool HasGetter { get; }
        public abstract Type PropertyType { get; }

        public abstract void Set<T>(object instance, T value);
        public abstract void Set(object instance, object value);
        public abstract object Get(object instance);
        public abstract TResult Get<TResult>(object instance);
    }

    public sealed partial class FastProperty<TType> : FastProperty
    {
        private readonly Delegate _getter;
        private readonly Delegate _getterReturnAsObject;
        private readonly Delegate _setter;
        private readonly Delegate _setterAsObject;

        private readonly string _name;
        private readonly Type _type;

        private FastProperty(Delegate getter, Delegate getterReturnAsObject, Delegate setter, Delegate setterAsObject, PropertyInfo info)
        {
            _getter = getter;
            _getterReturnAsObject = getterReturnAsObject;
            _setter = setter;
            _setterAsObject = setterAsObject;
            _name = info.Name;
            _type = info.PropertyType;
            IsStatic = info.GetMethod != null ? info.GetMethod.IsStatic : info.SetMethod.IsStatic;
        }

        public override string Name => _name;
        public override bool HasSetter => _setter != null;
        public override bool HasGetter => _getter != null;
        public override bool IsStatic { get; }
        public override Type PropertyType => _type;

        public override object Get(object instance)
        {
            return Get((TType)instance);
        }

        public override TResult Get<TResult>(object instance)
        {
            return Get<TResult>((TType)instance);
        }

        public T Get<T>(TType instance)
        {
            if (!IsStatic && instance == null) ThrowHelper.InstanceCantBeNullOfNonStaticMembers(Name);
            if (typeof(T) != _type) ThrowHelper.UnexpectedPropertyType(typeof(T), this);
            if (_getter == null) ThrowHelper.NoGetterFound(this);

            return ((Func<TType, T>)_getter)(instance);
        }

        public object Get(TType instance)
        {
            if (!IsStatic && instance == null) ThrowHelper.InstanceCantBeNullOfNonStaticMembers(Name);
            if (_getter == null) ThrowHelper.NoGetterFound(this);

            return ((Func<TType, object>)_getterReturnAsObject)(instance);
        }

        public override void Set<T>(object instance, T value)
        {
            Set((TType)instance, value);
        }

        public override void Set(object instance, object value)
        {
            Set((TType)instance, value);
        }

        public void Set<T>(TType instance, T value)
        {
            if (!IsStatic && instance == null) ThrowHelper.InstanceCantBeNullOfNonStaticMembers(Name);
            if (typeof(T) != _type) ThrowHelper.UnexpectedPropertyType(typeof(T), this);
            if (_setter == null) ThrowHelper.NoSetterFound(this);

            ((Action<TType, T>)_setter)(instance, value);

        }

        public void Set(TType instance, object value)
        {
            if (!IsStatic && instance == null) ThrowHelper.InstanceCantBeNullOfNonStaticMembers(Name);
            if (value.GetType() != _type) ThrowHelper.UnexpectedPropertyType(value.GetType(), this);
            if (_setter == null) ThrowHelper.NoSetterFound(this);

            ((Action<TType, object>)_setterAsObject)(instance, value);
        }
    }
}