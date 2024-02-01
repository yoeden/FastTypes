using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FastTypes.Tests.Copy.New
{
    [Trait(Traits.Copy.Tag, Traits.Copy.Arrays)]
    public class FastCopyArrayTests : BaseFastCopyTests
    {
        [Trait(Traits.Copy.Tag, Traits.Copy.Primitives)]
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnPureArray_ShouldClone(bool useGeneric)
        {
            //
            var source = new int[] { 1, 2, 3, 4, 5 };

            //
            var copy = InvokeDeepCopy(source, useGeneric);

            //
            copy.Should().NotBeSameAs(source);
            copy.Should().BeEquivalentTo(source);
        }

        [Trait(Traits.Copy.Tag, Traits.Copy.RefTypes)]
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnClassArray_ShouldClone(bool useGeneric)
        {
            //
            var source = new ClassWithPrimitiveStub[] { new(1), new(2), new(3) };

            //
            var copy = InvokeDeepCopy(source, useGeneric);

            //
            source.Should().NotBeSameAs(copy);
            for (var i = 0; i < source.Length; i++)
            {
                source[i].Should().NotBeSameAs(copy[i]);
                source[i].Num.Should().Be(copy[i].Num);
            }
        }

        [Trait(Traits.Copy.Tag, Traits.Copy.RefTypes)]
        [Trait(Traits.Copy.Tag, Traits.Copy.Null)]
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnClassArrayWithNullElement_ShouldClone(bool useGeneric)
        {
            //
            var source = new ClassWithPrimitiveStub[] { new(1), null, new(3) };

            //
            var copy = InvokeDeepCopy(source, useGeneric);

            //
            source.Should().NotBeSameAs(copy);
            for (var i = 0; i < source.Length; i++)
            {
                source[i].Should().BeEquivalentTo(copy[i]);
            }
        }

        [Trait(Traits.Copy.Tag, Traits.Copy.ComplexValueType)]
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnPureValueTypeArray_ShouldClone(bool useGeneric)
        {
            //
            var source = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), };

            //
            var copy = InvokeDeepCopy(source, useGeneric);

            //
            source.Should().NotBeSameAs(copy);
            for (var i = 0; i < source.Length; i++)
            {
                source[i].Should().Be(copy[i]);
            }
        }

        [Trait(Traits.Copy.Tag, Traits.Copy.ComplexValueType)]
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnComplexValueTypeArray_ShouldClone(bool useGeneric)
        {
            //
            var source = new ComplexValueTypeStub[] { new(1), new(2), new(3) };

            //
            var copy = InvokeDeepCopy(source, useGeneric);

            //
            source.Should().NotBeSameAs(copy);
            for (var i = 0; i < source.Length; i++)
            {
                source[i].Should().NotBeSameAs(copy[i]);
                source[i].RefType.Should().NotBeSameAs(copy[i].RefType);
                source[i].RefType.Num.Should().Be(copy[i].RefType.Num);
            }
        }
    }
}
