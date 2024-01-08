using System;
using System.Reflection;

namespace FastTypes.Reflection
{
    /// <summary>
    /// This class provides support for setting and getting a property value.
    /// </summary>
    /// <remarks>If type is known at compile time prefer using <see cref="FastProperty{TType}"/></remarks>
    public abstract class FastProperty : FastMember
    {
        /// <summary>
        /// Indicating whether this property has a setter.
        /// </summary>
        public abstract bool HasSetter { get; }

        /// <summary>
        /// Indicating whether this property has a getter.
        /// </summary>
        public abstract bool HasGetter { get; }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        public abstract Type PropertyType { get; }

        /// <summary>
        /// Sets the value of the property for a specified instance.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="instance">The instance to set the property value on.</param>
        /// <param name="value">The value to set.</param>
        /// <remarks>If type is known at compile time prefer using <see cref="FastProperty{TType}"/> to avoid boxing and extra allocations.</remarks>
        public abstract void Set<T>(object instance, T value);

        /// <summary>
        /// Sets the value of the property for a specified instance.
        /// </summary>
        /// <param name="instance">The instance to set the property value on.</param>
        /// <param name="value">The value to set.</param>
        /// <remarks>If type is known at compile time prefer using <see cref="FastProperty{TType}"/> to avoid boxing and extra allocations.</remarks>
        public abstract void Set(object instance, object value);

        /// <summary>
        /// Gets the value of the property for the a instance.
        /// </summary>
        /// <param name="instance">The instance to get the property value from.</param>
        /// <returns>The value of the property.</returns>
        /// <remarks>If type is known at compile time prefer using <see cref="FastProperty{TType}"/> to avoid boxing and extra allocations.</remarks>
        public abstract object Get(object instance);

        /// <summary>
        /// Gets the value of the property for the a instance.
        /// </summary>
        /// <typeparam name="TResult">The type of the value.</typeparam>
        /// <param name="instance">The instance to get the property value from.</param>
        /// <returns>The value of the property.</returns>
        /// <remarks>If type is known at compile time prefer using <see cref="FastProperty{TType}"/> to avoid boxing and extra allocations.</remarks>
        public abstract TResult Get<TResult>(object instance);
    }

    /// <summary>
    /// This class provides support for setting and getting a property value.
    /// </summary>
    public sealed partial class FastProperty<TType> : FastProperty
    {
        private readonly Delegate _getter;
        private readonly Delegate _getterReturnAsObject;
        private readonly Delegate _setter;
        private readonly Delegate _setterAsObject;

        private readonly Type _type;

        private FastProperty(Delegate getter, Delegate getterReturnAsObject, Delegate setter, Delegate setterAsObject, PropertyInfo info)
        {
            _getter = getter;
            _getterReturnAsObject = getterReturnAsObject;
            _setter = setter;
            _setterAsObject = setterAsObject;
            Name = info.Name;
            _type = info.PropertyType;
            IsStatic = info.GetMethod != null ? info.GetMethod.IsStatic : info.SetMethod.IsStatic;
        }

        /// <inheritdoc />
        public override string Name { get; }

        /// <inheritdoc />
        public override bool HasSetter => _setter != null;

        /// <inheritdoc />
        public override bool HasGetter => _getter != null;

        /// <inheritdoc />
        public override bool IsStatic { get; }

        /// <inheritdoc />
        public override Type PropertyType => _type;

        /// <inheritdoc />
        public override object Get(object instance)
        {
            return Get((TType)instance);
        }

        /// <inheritdoc />
        public override TResult Get<TResult>(object instance)
        {
            return Get<TResult>((TType)instance);
        }

        /// <summary>
        /// Gets the value of the property for the a instance.
        /// </summary>
        /// <param name="instance">The instance to get the property value from.</param>
        /// <returns>The value of the property.</returns>
        public T Get<T>(TType instance)
        {
            if (!IsStatic && instance == null) ThrowHelper.InstanceCantBeNullOfNonStaticMembers(Name);
            if (typeof(T) != _type) ThrowHelper.UnexpectedPropertyType(typeof(T), this);
            if (_getter == null) ThrowHelper.NoGetterFound(this);

            return ((Func<TType, T>)_getter)(instance);
        }

        /// <summary>
        /// Gets the value of the property for the a instance.
        /// </summary>
        /// <param name="instance">The instance to get the property value from.</param>
        /// <returns>The value of the property.</returns>
        /// <remarks>If type is known at compile time prefer using <see cref="Get{T}(TType)"/> to avoid boxing and extra allocations.</remarks>
        public object Get(TType instance)
        {
            if (!IsStatic && instance == null) ThrowHelper.InstanceCantBeNullOfNonStaticMembers(Name);
            if (_getter == null) ThrowHelper.NoGetterFound(this);

            return ((Func<TType, object>)_getterReturnAsObject)(instance);
        }

        /// <inheritdoc />
        public override void Set<T>(object instance, T value)
        {
            Set((TType)instance, value);
        }

        /// <inheritdoc />
        public override void Set(object instance, object value)
        {
            Set((TType)instance, value);
        }

        /// <summary>
        /// Sets the value of the property for a specified instance.
        /// </summary>
        /// <param name="instance">The instance to set the property value on.</param>
        /// <param name="value">The value to set.</param>
        public void Set<T>(TType instance, T value)
        {
            if (!IsStatic && instance == null) ThrowHelper.InstanceCantBeNullOfNonStaticMembers(Name);
            if (typeof(T) != _type) ThrowHelper.UnexpectedPropertyType(typeof(T), this);
            if (_setter == null) ThrowHelper.NoSetterFound(this);

            ((Action<TType, T>)_setter)(instance, value);

        }

        /// <summary>
        /// Sets the value of the property for a specified instance.
        /// </summary>
        /// <param name="instance">The instance to set the property value on.</param>
        /// <param name="value">The value to set.</param>
        /// <remarks>If type is known at compile time prefer using <see cref="Set{T}(TType,T)"/> to avoid boxing and extra allocations.</remarks>
        public void Set(TType instance, object value)
        {
            if (!IsStatic && instance == null) ThrowHelper.InstanceCantBeNullOfNonStaticMembers(Name);
            if (value.GetType() != _type) ThrowHelper.UnexpectedPropertyType(value.GetType(), this);
            if (_setter == null) ThrowHelper.NoSetterFound(this);

            ((Action<TType, object>)_setterAsObject)(instance, value);
        }
    }
}