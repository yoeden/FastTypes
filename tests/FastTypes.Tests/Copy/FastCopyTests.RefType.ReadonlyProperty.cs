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
        public void DeepCopyT_OnPublicRefTypeReadonlyProperty_ShouldClone()
        {
            //
            var source = new RefTypeReadonlyProperty(23);

            //
            var clone = InvokeGenericDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.Value.Should().NotBeSameAs(source.Value);
            clone.Value.Number.Should().Be(source.Value.Number);
        }

        [Fact]
        public void DeepCopy_OnPublicRefTypeReadonlyProperty_ShouldClone()
        {
            //
            var source = new RefTypeReadonlyProperty(23);

            //
            var clone = InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.Value.Should().NotBeSameAs(source.Value);
            clone.Value.Number.Should().Be(source.Value.Number);
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
            clone.GetPrivateValue().Number.Should().Be(source.GetPrivateValue().Number);
        }

        [Fact]
        public void DeepCopy_OnPrivateRefTypeReadonlyProperty_ShouldClone()
        {
            //
            var source = new RefTypeReadonlyProperty(23);

            //
            var clone = InvokeObjectDeepCopy(source);

            //
            clone.Should().NotBeSameAs(source);
            clone.GetPrivateValue().Should().NotBeSameAs(source.GetPrivateValue());
            clone.GetPrivateValue().Number.Should().Be(source.GetPrivateValue().Number);
        }

        private class RefTypeReadonlyProperty
        {
            public RefTypeReadonlyProperty(int v)
            {
                Value = new StubRefType() { Number = v };
                PValue = new StubRefType() { Number = v };
            }

            private RefTypeReadonlyProperty()
            {

            }

            // ReSharper disable once MemberCanBePrivate.Local
            public StubRefType Value { get; }
            private StubRefType PValue { get; }

            public StubRefType GetPrivateValue() => PValue;
        }


    }
}
