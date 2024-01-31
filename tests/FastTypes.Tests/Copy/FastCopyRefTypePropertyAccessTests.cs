using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastTypes.Tests.Copy.New;

namespace FastTypes.Tests.Copy
{
    [Trait(Traits.Copy.Tag, Traits.Copy.RefTypes)]
    [Trait(Traits.Copy.Tag, Traits.Copy.Access)]
    public class FastCopyRefTypePropertyAccessTests : BaseFastCopyTests
    {
        //This class is testing only ref types read write properties

        [Fact]
        public void DeepCopyT_OnPublicRefTypeProperty_ShouldClone()
        {
            //
            var source = new RefTypeReadWriteProperty(23);

            //
            var clone = InvokeGenericDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.Value.Should().NotBeSameAs(source.Value);
            clone.Value.Num.Should().Be(source.Value.Num);
        }

        [Fact]
        public void DeepCopy_OnPublicRefTypeProperty_ShouldClone()
        {
            //
            var source = new RefTypeReadWriteProperty(23);

            //
            var clone = (RefTypeReadWriteProperty)InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.Value.Should().NotBeSameAs(source.Value);
            clone.Value.Num.Should().Be(source.Value.Num);
        }


        [Fact]
        public void DeepCopyT_OnPrivateRefTypeProperty_ShouldClone()
        {
            //
            var source = new RefTypeReadWriteProperty(23);

            //
            var clone = InvokeGenericDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.GetPrivateValue().Should().NotBeSameAs(source.GetPrivateValue());
            clone.GetPrivateValue().Num.Should().Be(source.GetPrivateValue().Num);
        }

        [Fact]
        public void DeepCopy_OnPrivateRefTypeProperty_ShouldClone()
        {
            //
            var source = new RefTypeReadWriteProperty(23);

            //
            var clone = (RefTypeReadWriteProperty)InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.GetPrivateValue().Should().NotBeSameAs(source.GetPrivateValue());
            clone.GetPrivateValue().Num.Should().Be(source.GetPrivateValue().Num);
        }

        private class RefTypeReadWriteProperty
        {
            public RefTypeReadWriteProperty(int v)
            {
                Value = new ClassWithPrimitiveStub(v);
                PValue = new ClassWithPrimitiveStub(v);
            }

            private RefTypeReadWriteProperty()
            {

            }

            // ReSharper disable once MemberCanBePrivate.Local
            public ClassWithPrimitiveStub Value { get; set; }
            private ClassWithPrimitiveStub PValue { get; set; }

            public ClassWithPrimitiveStub GetPrivateValue() => PValue;
        }


    }
}
