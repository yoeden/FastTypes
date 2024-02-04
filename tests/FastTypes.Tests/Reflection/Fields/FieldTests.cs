using FastTypes.Reflection;
using FluentAssertions;

namespace FastTypes.Tests.Reflection.Fields
{
    public abstract class FieldTests<T>
    {
        public abstract T GetValue<TType>(TType instance, string name);
        public abstract void SetValue<TType>(TType instance, string name, object value);
        public abstract BaseFastField GetField<TType>(string name);

        public abstract T Value();
        public abstract object BadValue();


        //
        // Properties
        //

        [Fact]
        public void IsReadonly_OnReadonlyField_True()
        {
            //
            var field = GetField<InstanceFieldsStubT<T>>(nameof(InstanceFieldsStubT<T>.PublicReadonly));

            //
            field.IsReadonly.Should().BeTrue();
        }

        [Fact]
        public void IsReadonly_OnField_False()
        {
            //
            var field = GetField<InstanceFieldsStubT<T>>(nameof(InstanceFieldsStubT<T>.Public));

            //
            field.IsReadonly.Should().BeFalse();
        }

        [Fact]
        public void IsPublic_OnPublicField_True()
        {
            //
            var field = GetField<InstanceFieldsStubT<T>>(nameof(InstanceFieldsStubT<T>.PublicReadonly));

            //
            field.IsPublic.Should().BeTrue();
        }

        [Fact]
        public void IsPublic_OnPrivateField_False()
        {
            //
            var field = GetField<InstanceFieldsStubT<T>>(InstanceFieldsStubT<T>.PrivateFieldName);

            //
            field.IsPublic.Should().BeFalse();
        }

        [Fact]
        public void Type_OnField()
        {
            //
            var field = GetField<InstanceFieldsStubT<T>>(nameof(InstanceFieldsStubT<T>.Public));

            //
            field.Type.Should().Be(typeof(T));
        }

        [Fact]
        public void Name_OnField()
        {
            //
            var field = GetField<InstanceFieldsStubT<T>>(nameof(InstanceFieldsStubT<T>.PublicReadonly));

            //
            field.Name.Should().Be(nameof(InstanceFieldsStubT<T>.PublicReadonly));
        }

        [Fact]
        public void Name_OnBackingField()
        {
            //
            var field = GetField<InstanceFieldsStubT<T>>($"<{nameof(InstanceFieldsStubT<T>.PropertyForBackingField)}>k__BackingField");

            //
            field.Name.Should().Be($"<{nameof(InstanceFieldsStubT<T>.PropertyForBackingField)}>k__BackingField");
        }

        [Fact]
        public void IsBackingField_OnBackingField_True()
        {
            //
            var field = GetField<InstanceFieldsStubT<T>>($"<{nameof(InstanceFieldsStubT<T>.PropertyForBackingField)}>k__BackingField");

            //
            field.IsBackingField.Should().BeTrue();
        }

        [Fact]
        public void IsBackingField_OnField_False()
        {
            //
            var field = GetField<InstanceFieldsStubT<T>>(nameof(InstanceFieldsStubT<T>.Public));

            //
            field.IsBackingField.Should().BeFalse();
        }

        [Fact]
        public void Field_NonExistentField_Throw()
        {
            //
            var action = () => GetField<InstanceFieldsStubT<T>>("akaskaskd");

            //
            action.Should().Throw<FieldNotFoundException>();
        }

        //
        // SetValue tests
        //

        [Fact]
        public void SetValue_OnPublicReadonlyField()
        {
            //
            T value = Value();
            var instance = new InstanceFieldsStubT<T>(value);

            //
            SetValue(instance, nameof(InstanceFieldsStubT<T>.PublicReadonly), value);

            //
            instance.PublicReadonly.Should().Be(value, $"Public readonly field value is expected to be {value}");
        }

        [Fact]
        public void SetValue_OnPublicField()
        {
            //
            T value = Value();
            var instance = new InstanceFieldsStubT<T>(value);

            //
            SetValue(instance, nameof(InstanceFieldsStubT<T>.Public), value);

            //
            instance.Public.Should().Be(value, $"Public field value is expected to be {value}");
        }

        [Fact]
        public void SetValue_OnPrivateField()
        {
            //
            T value = Value();
            var instance = new InstanceFieldsStubT<T>(value);

            //
            SetValue(instance, InstanceFieldsStubT<T>.PrivateFieldName, value);

            //
            instance.GetPrivateValue().Should().Be(value, $"Private field value is expected to be {value}");
        }

        [Fact]
        public void SetValue_OnPrivateReadonlyField()
        {
            //
            T value = Value();
            var instance = new InstanceFieldsStubT<T>(value);

            //
            SetValue(instance, InstanceFieldsStubT<T>.PrivateReadonlyFieldName, value);

            //
            instance.GetPrivateReadonlyValue().Should().Be(value, $"Private readonly field value is expected to be {value}");
        }

        [Fact]
        public void SetValue_OnNullInstance_Throw()
        {
            //
            T value = Value();

            //
            var action = () => SetValue<InstanceFieldsStubT<T>>(null, nameof(InstanceFieldsStubT<T>.PublicReadonly), value);

            //
            action.Should().Throw<InstanceNullException>();
        }

        // 
        // GetValue tests
        //

        [Fact]
        public void GetValue_OnPublicReadonlyField()
        {
            //
            T value = Value();
            var instance = new InstanceFieldsStubT<T>(value);

            //
            GetValue(instance, nameof(InstanceFieldsStubT<T>.PublicReadonly)).Should().Be(value, $"Get public readonly field value should be {value}");
        }

        [Fact]
        public void GetValue_OnPublicField()
        {
            //
            T value = Value();
            var instance = new InstanceFieldsStubT<T>(value);

            //
            GetValue(instance, nameof(InstanceFieldsStubT<T>.Public)).Should().Be(value, $"Get public field value should be {value}");
        }

        [Fact]
        public void GetValue_OnPrivateField()
        {
            //
            T value = Value();
            var instance = new InstanceFieldsStubT<T>(value);

            //
            GetValue(instance, InstanceFieldsStubT<T>.PrivateFieldName).Should().Be(value, $"Get private field value should be {value}");
        }

        [Fact]
        public void GetValue_OnPrivateReadonlyField()
        {
            //
            T value = Value();
            var instance = new InstanceFieldsStubT<T>(value);

            //
            GetValue(instance, InstanceFieldsStubT<T>.PrivateReadonlyFieldName).Should().Be(value, $"Get private field value should be {value}");
        }

        [Fact]
        public void GetValue_OnNullInstance_Throws()
        {
            //
            T value = Value();

            //
            var action = () => GetValue<InstanceFieldsStubT<T>>(null, InstanceFieldsStubT<T>.PrivateReadonlyFieldName).Should().Be(value, $"Get private field value should be {value}");

            //
            action.Should().Throw<InstanceNullException>();
        }

        private sealed class InstanceFieldsStubT<T>
        {
            public static readonly string PrivateFieldName = nameof(_private);
            public static readonly string PrivateReadonlyFieldName = nameof(_privateReadonly);

            public InstanceFieldsStubT(T x = default)
            {
                Public = PublicReadonly = _private = _privateReadonly = x;
            }

            private readonly T _privateReadonly;
            // ReSharper disable once FieldCanBeMadeReadOnly.Local
            private T _private;

            public readonly T PublicReadonly;
            public T Public;

            public T PropertyForBackingField { get; set; }

            public T GetPrivateReadonlyValue() => _privateReadonly;
            public T GetPrivateValue() => _private;
        }
    }
}