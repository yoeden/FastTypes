using FastTypes.Clone;
using FluentAssertions;

namespace FastTypes.Tests.Copy.New
{
    [Trait(Traits.Copy.Tag, Traits.Copy.Collections)]
    public class FastCopyGenericICollectionTests : BaseFastCopyTests
    {
        [Theory]
        [InlineData(typeof(List<int>))]
        [InlineData(typeof(HashSet<int>))]
        [InlineData(typeof(LinkedList<int>))]
        [InlineData(typeof(SortedSet<int>))]
        [Trait(Traits.Copy.Tag, Traits.Copy.Primitives)]
        public void DeepCopy_OnPureValueTypeGenericCollection_ShouldClone(Type type)
        {
            //
            ICollection<int> source = (ICollection<int>)Activator.CreateInstance(type)!;
            source.Add(1);
            source.Add(2);
            source.Add(3);

            //
            var copy = FastCopy.DeepCopy((object)source);

            //
            copy.Should().NotBeSameAs(source);
            copy.Should().BeEquivalentTo(source);
        }

        [Theory]
        [InlineData(typeof(List<ClassWithPrimitiveStub>))]
        [InlineData(typeof(HashSet<ClassWithPrimitiveStub>))]
        [InlineData(typeof(LinkedList<ClassWithPrimitiveStub>))]
        //Were not working with SortedSet because it is required that the T will implement IComperable
        [Trait(Traits.Copy.Tag, Traits.Copy.RefTypes)]
        public void DeepCopy_OnRefTypeGenericCollection_ShouldClone(Type type)
        {
            //
            var source = (ICollection<ClassWithPrimitiveStub>)Activator.CreateInstance(type)!;
            source.Add(new(1));
            source.Add(new(2));
            source.Add(new(3));

            //
            var copy = FastCopy.DeepCopy((object)source);

            //
            copy.Should().NotBeSameAs(source);
            copy.Should().BeEquivalentTo(source);
        }
    }
}