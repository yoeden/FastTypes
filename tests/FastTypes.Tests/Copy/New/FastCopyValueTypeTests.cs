using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastTypes.Clone;
using FluentAssertions;

namespace FastTypes.Tests.Copy.New
{
    public class FastCopyValueTypeTests : BaseFastCopyTests
    {
        [Fact]
        public void DeepCopy_OnPureValueType_ShouldClone()
        {
            //
            var source = new PureValueTypeStub(23);

            //
            var copy = FastCopy.DeepCopy(source);

            //
            source.Value.Should().Be(copy.Value);
        }

        [Fact]
        public void DeepCopy_OnComplexValueType_ShouldClone()
        {
            //
            var source = new ComplexValueTypeStub(23);

            //
            var copy = InvokeDeepCopy(source);

            //
            copy.RefType.Should().NotBeSameAs(source.RefType);
            copy.RefType.Value.Should().Be(source.RefType.Value);
        }


    }
}
