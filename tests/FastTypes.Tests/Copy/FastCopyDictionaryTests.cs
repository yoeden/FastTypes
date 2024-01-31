using System.Collections;
using FluentAssertions;

namespace FastTypes.Tests.Copy.New
{
    [Trait(Traits.Copy.Tag, Traits.Copy.Collections)]
    public class FastCopyDictionaryTests : BaseFastCopyTests
    {
        [Trait(Traits.Copy.Tag, Traits.Copy.Primitives)]
        [Fact]
        public void DeepCopy_OnIntKeyAndIntValueDictionary_ShouldClone()
        {
            //
            var src = new Dictionary<int, int>()
            {
                [10] = 100,
                [20] = 200,
                [30] = 300,
            };

            //
            var copy = InvokeDeepCopy(src);

            //
            copy.Should().BeEquivalentTo(src);
        }

        [Trait(Traits.Copy.Tag, Traits.Copy.RefTypes)]
        [Fact]
        public void DeepCopy_OnIntKeyAndRefValueDictionary_ShouldClone()
        {
            //
            var src = new Dictionary<int, ClassWithPrimitiveStub>()
            {
                [10] = new(100),
                [20] = new(200),
                [30] = new(300),
            };

            //
            var copy = InvokeDeepCopy(src);

            //
            copy.Should().BeEquivalentTo(src);
        }

    }
}
