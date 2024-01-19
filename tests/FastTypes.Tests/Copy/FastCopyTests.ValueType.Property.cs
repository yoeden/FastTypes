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
        //This class is testing only value types read write properties

        [Fact]
        public void DeepCopyT_OnPublicValueTypeProperty_ShouldClone()
        {
            //
            var source = new ValueTypeReadWriteProperty();
            source.Value = 23;

            //
            var clone = InvokeGenericDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.Value.Should().Be(source.Value);
        }

        [Fact]
        public void DeepCopy_OnPublicValueTypeProperty_ShouldClone()
        {
            //
            var source = new ValueTypeReadWriteProperty();
            source.Value = 23;

            //
            var clone = InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.Value.Should().Be(source.Value);
        }


        [Fact]
        public void DeepCopyT_OnPrivateValueTypeProperty_ShouldClone()
        {
            //
            var source = new ValueTypeReadWriteProperty();
            source.SetPrivateValue(23);

            //
            var clone = InvokeGenericDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.GetPrivateValue().Should().Be(source.GetPrivateValue());
        }

        [Fact]
        public void DeepCopy_OnPrivateValueTypeProperty_ShouldClone()
        {
            //
            var source = new ValueTypeReadWriteProperty();
            source.SetPrivateValue(23);

            //
            var clone = InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.GetPrivateValue().Should().Be(source.GetPrivateValue());
        }

        private class ValueTypeReadWriteProperty
        {
            public int Value { get; set; }
            private int PValue { get; set; }

            public int GetPrivateValue() => PValue;
            public void SetPrivateValue(int v) => PValue = v;
        }

    }
}
