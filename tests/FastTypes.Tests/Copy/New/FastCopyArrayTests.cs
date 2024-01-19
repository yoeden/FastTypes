using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastTypes.Clone;
using FluentAssertions;

namespace FastTypes.Tests.Copy.New
{
    public class FastCopyArrayTests : BaseFastCopyTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnPureArray_ShouldClone(bool useGeneric)
        {
            //
            var source = new PureArray().Init();

            //
            var copy = InvokeDeepCopy(source, useGeneric);

            //
            source.Should().NotBeSameAs(copy);
            source.Values.Should().BeEquivalentTo(copy.Values);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnClassArray_ShouldClone(bool useGeneric)
        {
            //
            var source = new ClassArray();

            //
            var copy = InvokeDeepCopy(source, useGeneric);

            //
            source.Should().NotBeSameAs(copy);
            for (var i = 0; i < source.Values.Length; i++)
            {
                source.Values[i].Should().NotBeSameAs(copy.Values[i]);
                source.Values[i].Value.Should().Be(copy.Values[i].Value);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnClassArrayWithNullElement_ShouldClone(bool useGeneric)
        {
            //
            var source = new ClassArray();

            //
            var copy = InvokeDeepCopy(source, useGeneric);

            //
            source.Should().NotBeSameAs(copy);
            for (var i = 0; i < source.Values.Length; i++)
            {
                source.Values[i].Should().NotBeSameAs(copy.Values[i]);
                source.Values[i].Value.Should().Be(copy.Values[i].Value);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnPureValueTypeArray_ShouldClone(bool useGeneric)
        {
            //
            var source = new PureValueTypeArray().Init();

            //
            var copy = InvokeDeepCopy(source, useGeneric);

            //
            source.Should().NotBeSameAs(copy);
            for (var i = 0; i < source.Values.Length; i++)
            {
                source.Values[i].Should().NotBeSameAs(copy.Values[i]);
                source.Values[i].Value.Should().Be(copy.Values[i].Value);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeepCopy_OnComplexValueTypeArray_ShouldClone(bool useGeneric)
        {
            //
            var source = ComplexValueTypeArray.Create();

            //
            var copy = InvokeDeepCopy(source,useGeneric);

            //
            source.Should().NotBeSameAs(copy);
            for (var i = 0; i < source.Values.Length; i++)
            {
                source.Values[i].Should().NotBeSameAs(copy.Values[i]);
                source.Values[i].RefType.Should().NotBeSameAs(copy.Values[i].RefType);
                source.Values[i].RefType.Value.Should().Be(copy.Values[i].RefType.Value);
            }
        }

        private class PureArray
        {
            public PureArray()
            {

            }

            public PureArray Init()
            {
                Values = new int[] { 1, 2, 3 };
                return this;
            }

            public int[] Values { get; set; }
        }

        private class ClassArray
        {
            public ClassArray(bool withNull = false)
            {
                Values = new ClassWithPrimitiveStub[] { new(1), withNull ? null : new(2), new(3) };
            }

            private ClassArray()
            {

            }

            public ClassWithPrimitiveStub[] Values { get; set; }
        }

        private class PureValueTypeArray
        {
            public PureValueTypeArray()
            {

            }

            public PureValueTypeArray Init()
            {
                Values = new PureValueTypeStub[] { new(1), new(2), new(3) };
                return this;
            }

            public PureValueTypeStub[] Values { get; set; }
        }

        private class ComplexValueTypeArray
        {
            public static ComplexValueTypeArray Create()
            {
                return new()
                {
                    Values = new ComplexValueTypeStub[] { new(1), new(2), new(3) }
                };
            }

            public ComplexValueTypeStub[] Values { get; set; }
        }
    }
}
