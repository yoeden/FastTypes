using System.Reflection;
using FastTypes.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Tags
{
    [Trait(Traits.Query.Tag,Traits.Query.Tags)]
    public class QueryTagsTests
    {
        [Fact]
        public void New_EmptyTags_ReturnsEmpty()
        {
            //
            var tags = QueryTags.FromDictionary(new Dictionary<Type, object>());

            //
            tags.Should().Be(QueryTags.Empty);
        }

        [Fact]
        public void New_NullTags_Throws()
        {
            //
            var tags = () => QueryTags.FromDictionary(null);

            //
            tags.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void TryGet_ExistingTag_ReturnsTrue()
        {
            //
            var tags = QueryTags.FromDictionary(new Dictionary<Type, object>()
            {
                [typeof(AssemblyNameFlags)] = AssemblyNameFlags.PublicKey
            });

            //
            var exists = tags.TryGet<AssemblyNameFlags>(out var result);

            //
            exists.Should().BeTrue();
            result.Should().Be(AssemblyNameFlags.PublicKey);
        }

        [Fact]
        public void TryGet_NonExistingTag_ReturnsFalse()
        {
            //
            var tags = QueryTags.FromDictionary(new Dictionary<Type, object>());

            //
            var exists = tags.TryGet<AssemblyNameFlags>(out _);

            //
            exists.Should().BeFalse();
        }
    }
}
