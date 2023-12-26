using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FastTypes.Tests.Reflection.Properties
{
    [Trait(Traits.Reflection, Traits.Reflection_Properties_Get)]
    public class PropertyGet_Generic
    {
        private readonly IFastType<StubPropertyClass> _fastType;
        private readonly StubPropertyClass _instance;

        public PropertyGet_Generic()
        {
            _fastType = FastType.Of<StubPropertyClass>();
            _instance = new StubPropertyClass();
        }

        [Fact]
        public void HasGetter_OnPropertyWithGetter_ReturnsTrue()
        {
            //
            var prop = _fastType.Property(nameof(StubPropertyClass.ValueTypePropSetGet));

            //
            prop.HasGetter.Should().BeTrue();
        }

        [Fact]
        public void HasGetter_OnPropertyWithoutGetter_ReturnsFalse()
        {
            //
            var prop = _fastType.Property(nameof(StubPropertyClass.PropSetOnly));

            //
            prop.HasGetter.Should().BeFalse();
        }

        [Fact]
        public void GetObject_OnPropertyWithGetter_ShouldReturn()
        {
            //
            var expected = ExpectedValues.RandomInt();
            _instance.ValueTypePropSetGet = expected;
            var prop = _fastType.Property(nameof(StubPropertyClass.ValueTypePropSetGet));

            //
            var value = prop.Get(_instance);

            //
            expected.Should().Be((int)value);
        }

        [Fact]
        public void GetT_OnPropertyWithGetter_ShouldReturn()
        {
            //
            var expected = ExpectedValues.RandomInt();
            _instance.ValueTypePropSetGet = expected;
            var prop = _fastType.Property(nameof(StubPropertyClass.ValueTypePropSetGet));

            //
            var value = prop.Get<int>(_instance);

            //
            expected.Should().Be(value);
        }

        [Fact]
        public void GetT_OnPropertyWithoutGetter_ShouldThrow()
        {
            //
            var prop = _fastType.Property(nameof(StubPropertyClass.PropSetOnly));

            //
            var action = () => prop.Get<int>(_instance);

            //
            action.Should().Throw<NoGetterFoundException>();
        }

        [Fact]
        public void GetObject_OnPropertyWithoutGetter_ShouldThrow()
        {
            //
            var prop = _fastType.Property(nameof(StubPropertyClass.PropSetOnly));

            //
            var action = () => prop.Get(_instance);

            //
            action.Should().Throw<NoGetterFoundException>();
        }
    }
}
