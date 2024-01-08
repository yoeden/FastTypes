using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastTypes.Reflection;
using FluentAssertions;

namespace FastTypes.Tests.Reflection.Properties
{
    [Trait(Traits.Reflection.Tag, Traits.Reflection.Properties)]
    [Trait(Traits.Reflection.Tag, Traits.Reflection.PropertiesSet)]
    public class PropertySet_Object
    {
        private readonly IFastType _fastType;
        private readonly StubPropertyClass _instance;

        public PropertySet_Object()
        {
            _fastType = FastType.Of<StubPropertyClass>();
            _instance = new StubPropertyClass();
        }

        [Fact]
        public void HasSetter_OnPropertyWithSetter_ReturnsTrue()
        {
            //
            var prop = _fastType.Property(nameof(StubPropertyClass.ValueTypePropSetGet));

            //
            prop.HasSetter.Should().BeTrue();
        }

        [Fact]
        public void HasSetter_OnPropertyWithoutSetter_ReturnsFalse()
        {
            //
            var prop = _fastType.Property(nameof(StubPropertyClass.PropGetOnly));

            //
            prop.HasSetter.Should().BeFalse();
        }

        [Fact]
        public void SetObject_OnPropertyWithSetter_ShouldAssign()
        {
            //
            var expected = ExpectedValues.RandomInt();
            var prop = _fastType.Property(nameof(StubPropertyClass.ValueTypePropSetGet));

            //
            prop.Set(_instance, (object)expected);

            //
            expected.Should().Be(_instance.ValueTypePropSetGet);
        }

        [Fact]
        public void SetT_OnPropertyWithSetter_ShouldAssign()
        {
            //
            var expected = ExpectedValues.RandomInt();
            var prop = _fastType.Property(nameof(StubPropertyClass.ValueTypePropSetGet));

            //
            prop.Set(_instance, expected);

            //
            expected.Should().Be(_instance.ValueTypePropSetGet);
        }

        [Fact]
        public void SetT_OnPropertyWithoutSetter_ShouldThrow()
        {
            //
            var prop = _fastType.Property(nameof(StubPropertyClass.PropGetOnly));

            //
            var action = () => prop.Set(_instance, 0);

            //
            action.Should().Throw<NoSetterFoundException>();
        }

        [Fact]
        public void SetT_OnPropertyWithUnexpectedType_ShouldThrow()
        {
            //
            var prop = _fastType.Property(nameof(StubPropertyClass.ValueTypePropSetGet));

            //
            var action = () => prop.Set(_instance, 0.0);

            //
            action.Should().Throw<UnexpectedMemberType>();
        }

        [Fact]
        public void SetObject_OnPropertyWithoutSetter_ShouldThrow()
        {
            //
            var prop = _fastType.Property(nameof(StubPropertyClass.PropGetOnly));

            //
            var action = () => prop.Set(_instance, (object)0);

            //
            action.Should().Throw<NoSetterFoundException>();
        }


        [Fact]
        public void SetObject_OnPropertyWithUnexpectedType_ShouldThrow()
        {
            //
            var prop = _fastType.Property(nameof(StubPropertyClass.ValueTypePropSetGet));

            //
            var action = () => prop.Set(_instance, (object)0.0);

            //
            action.Should().Throw<UnexpectedMemberType>();
        }
    }

}
