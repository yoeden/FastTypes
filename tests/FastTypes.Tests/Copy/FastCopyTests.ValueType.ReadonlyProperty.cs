using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTypes.Tests.Copy
{
    public partial class FastCopyTests
    {
        //This class is testing only value types read only properties

        [Fact]
        public void DeepCopyT_OnPublicValueTypeReadonlyProperty_ShouldClone()
        {
            //
            var source = new ValueTypeReadonlyProperty(23);

            //
            var clone = InvokeGenericDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.Value.Should().Be(source.Value);
        }

        [Fact]
        public void DeepCopy_OnPublicValueTypeReadonlyProperty_ShouldClone()
        {
            //
            var source = new ValueTypeReadonlyProperty(23);

            //
            var clone = InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.Value.Should().Be(source.Value);
        }


        [Fact]
        public void DeepCopyT_OnPrivateValueTypeReadonlyProperty_ShouldClone()
        {
            //
            var source = new ValueTypeReadonlyProperty(23);

            //
            var clone = InvokeGenericDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.GetPrivateValue().Should().Be(source.GetPrivateValue());
        }

        [Fact]
        public void DeepCopy_OnPrivateValueTypeReadonlyProperty_ShouldClone()
        {
            //
            var source = new ValueTypeReadonlyProperty(23);

            //
            var clone = InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.GetPrivateValue().Should().Be(source.GetPrivateValue());
        }

        private class ValueTypeReadonlyProperty
        {
            public ValueTypeReadonlyProperty(int value)
            {
                Value = value;
                PValue = value;
            }

            private ValueTypeReadonlyProperty()
            {

            }

            public int Value { get; }
            private int PValue { get; }

            public int GetPrivateValue() => PValue;
        }

    }
}
