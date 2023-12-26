using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FastTypes.Tests.Reflection.Properties
{
    [Trait(Traits.Reflection,Traits.Reflection_Properties)]
    public class PropertyTests
    {
        private readonly IFastType<StubPropertyClass> _fastType;

        public PropertyTests()
        {
            _fastType = FastType.Of<StubPropertyClass>();
        }

        [Fact]
        public void Properties_OnClass_HaveAllProperties()
        {
            //
            var props = _fastType.Properties();

            //
            props.Any(property => property.Name == nameof(StubPropertyClass.ValueTypePropSetGet));
            props.Any(property => property.Name == nameof(StubPropertyClass.PropGetOnly));
            props.Any(property => property.Name == nameof(StubPropertyClass.PropSetOnly));
        }

        [Fact]
        public void Type_OnPropertyType_ReturnsItsType()
        {
            //
            var prop = _fastType.Property(nameof(StubPropertyClass.ValueTypePropSetGet));

            //
            prop.PropertyType.Should().Be(typeof(int));
        }

        [Fact]
        public void IsStatic_OnNonStaticProperty_ReturnsFalse()
        {
            //
            var prop = _fastType.Property(nameof(StubPropertyClass.ValueTypePropSetGet));

            //
            prop.IsStatic.Should().BeFalse();
        }
    }
}
