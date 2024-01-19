using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FastTypes.Tests.Copy.New
{
    public class FastCopyClassTests : BaseFastCopyTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnClassWithPrimitive_ShouldClone(bool useGeneric)
        {
            //
            var source = new ClassWithPrimitiveStub(23);

            //
            var copy = InvokeDeepCopy(source, useGeneric);

            //
            copy.Should().NotBeSameAs(source);
            copy.Value.Should().Be(source.Value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnClassWithPureValueType_ShouldClone(bool useGeneric)
        {
            //
            var source = new ClassWithPureValueTypeStub(23);

            //
            var copy = InvokeDeepCopy(source, useGeneric);

            //
            copy.Should().NotBeSameAs(source);
            copy.Value.Should().Be(source.Value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnClassWithComplexValueType_ShouldClone(bool useGeneric)
        {
            //
            var source = new ClassWithComplexValueTypeStub(23);

            //
            var copy = InvokeDeepCopy(source, useGeneric);

            //
            copy.Should().NotBeSameAs(source);
            copy.ComplexValueType.RefType.Should().NotBeSameAs(source.ComplexValueType.RefType);
            copy.ComplexValueType.RefType.Value.Should().Be(source.ComplexValueType.RefType.Value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnClassWithNullRefValue_ShouldClone(bool useGeneric)
        {
            //
            var source = ClassWithInternalClassStub.Create(null);

            //
            var copy = InvokeDeepCopy(source, useGeneric);

            //
            copy.Should().NotBeSameAs(source);
            copy.Internal.Should().BeNull();
        }
    }
}
