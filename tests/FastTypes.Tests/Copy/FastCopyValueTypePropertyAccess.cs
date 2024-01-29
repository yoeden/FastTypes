using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastTypes.Tests.Copy.New;

namespace FastTypes.Tests.Copy
{
    [Trait(Traits.Copy.Tag, Traits.Copy.ComplexValueType)]
    [Trait(Traits.Copy.Tag, Traits.Copy.Access)]
    public class FastCopyValueTypePropertyAccess : BaseFastCopyTests
    {
        //This class is testing only value types read write properties

        [Fact]
        public void DeepCopyT_OnPublicValueTypeProperty_ShouldClone()
        {
            //
            var source = new ValueTypeReadWriteProperty();
            source.Value = new ClassWithPrimitiveStub(23);

            //
            var clone = InvokeGenericDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.Value.Should().NotBeSameAs(source.Value);
            clone.Value.Num.Should().Be(source.Value.Num);
        }

        [Fact]
        public void DeepCopy_OnPublicValueTypeProperty_ShouldClone()
        {
            //
            var source = new ValueTypeReadWriteProperty();
            source.Value = new ClassWithPrimitiveStub(23);

            //
            var clone = (ValueTypeReadWriteProperty)InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.Value.Should().NotBeSameAs(source.Value);
            clone.Value.Num.Should().Be(source.Value.Num);
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
            var clone = (ValueTypeReadWriteProperty)InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.GetPrivateValue().Should().Be(source.GetPrivateValue());
        }

        private struct ValueTypeReadWriteProperty
        {
            public ClassWithPrimitiveStub Value { get; set; }
            private ClassWithPrimitiveStub PValue { get; set; }

            public int GetPrivateValue() => PValue.Num;
            public void SetPrivateValue(int v) => PValue = new(v);
        }
    }
}
