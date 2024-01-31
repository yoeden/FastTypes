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
    public class FastCopyRefTypeReadonlyPropertyAccessTests : BaseFastCopyTests
    {
        //This class is testing only value types read write properties

        [Fact]
        public void DeepCopyT_OnPublicRefTypeReadonlyProperty_ShouldClone()
        {
            //
            var source = new RefTypeReadonlyProperty(23);

            //
            var clone = InvokeGenericDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.Value.Should().NotBeSameAs(source.Value);
            clone.Value.Num.Should().Be(source.Value.Num);
        }

        [Fact]
        public void DeepCopy_OnPublicRefTypeReadonlyProperty_ShouldClone()
        {
            //
            var source = new RefTypeReadonlyProperty(23);

            //
            var clone = (RefTypeReadonlyProperty)InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.Value.Should().NotBeSameAs(source.Value);
            clone.Value.Num.Should().Be(source.Value.Num);
        }


        [Fact]
        public void DeepCopyT_OnPrivateRefTypeReadonlyProperty_ShouldClone()
        {
            //
            var source = new RefTypeReadonlyProperty(23);

            //
            var clone = InvokeGenericDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.GetPrivateValue().Should().NotBeSameAs(source.GetPrivateValue());
            clone.GetPrivateValue().Num.Should().Be(source.GetPrivateValue().Num);
        }

        [Fact]
        public void DeepCopy_OnPrivateRefTypeReadonlyProperty_ShouldClone()
        {
            //
            var source = new RefTypeReadonlyProperty(23);

            //
            var clone = (RefTypeReadonlyProperty)InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.GetPrivateValue().Should().NotBeSameAs(source.GetPrivateValue());
            clone.GetPrivateValue().Num.Should().Be(source.GetPrivateValue().Num);
        }

        private class RefTypeReadonlyProperty
        {
            public RefTypeReadonlyProperty(int v)
            {
                Value = new ClassWithPrimitiveStub(v) ;
                PValue = new ClassWithPrimitiveStub(v) ;
            }

            private RefTypeReadonlyProperty()
            {

            }

            // ReSharper disable once MemberCanBePrivate.Local
            public ClassWithPrimitiveStub Value { get; }
            private ClassWithPrimitiveStub PValue { get; }

            public ClassWithPrimitiveStub GetPrivateValue() => PValue;
        }


    }
}
