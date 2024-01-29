using System.Collections.Immutable;
using FastTypes.Clone;
using FluentAssertions;

namespace FastTypes.Tests.Copy.New
{
    [Trait(Traits.Copy.Tag, Traits.Copy.Collections)]
    [Trait(Traits.Copy.Tag, Traits.Copy.Immutable)]
    public class FastCopyImmutableCollectionsTests : BaseFastCopyTests
    {
        [Trait(Traits.Copy.Tag, Traits.Copy.Primitives)]
        [Theory]
        [MemberData(nameof(SplitCountData))]
        public void DeepCopy_OnPureValueTypeGenericCollection_ShouldClone(IEnumerable<int> source)
        {
            //
            var copy = FastCopy.DeepCopy((object)source);

            //
            copy.Should().NotBeSameAs(source);
            copy.Should().BeEquivalentTo(source);
        }

        public static IEnumerable<object[]> SplitCountData =>
            new List<object[]>
            {
                new object[] { ImmutableList.Create(1,2,3) },
                new object[] { ImmutableStack.Create(1,2,3) },
            };
    }
}