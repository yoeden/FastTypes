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
    public class FastCopyValueTypeReadonlyPropertyAccessTests : BaseFastCopyTests
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
            clone.Value.Should().NotBeSameAs(source.Value);
            clone.Value.Num.Should().Be(source.Value.Num);
        }

        [Fact]
        public void DeepCopy_OnPublicValueTypeReadonlyProperty_ShouldClone()
        {
            //
            var source = new ValueTypeReadonlyProperty(23);

            //
            var clone = (ValueTypeReadonlyProperty)InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.Value.Should().NotBeSameAs(source.Value);
            clone.Value.Num.Should().Be(source.Value.Num);
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
            var clone = (ValueTypeReadonlyProperty)InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.GetPrivateValue().Should().Be(source.GetPrivateValue());
        }

        private readonly struct ValueTypeReadonlyProperty
        {
            public ValueTypeReadonlyProperty(int value)
            {
                Value = new ClassWithPrimitiveStub(value);
                PValue = new ClassWithPrimitiveStub(value);
            }

            public ClassWithPrimitiveStub Value { get; }
            private ClassWithPrimitiveStub PValue { get; }

            public int GetPrivateValue() => PValue.Num;
        }

    }
}
