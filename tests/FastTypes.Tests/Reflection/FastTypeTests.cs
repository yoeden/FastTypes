using FastTypes.Tests.Reflection.Properties;
using FluentAssertions;

namespace FastTypes.Tests.Reflection
{
    [Trait(Traits.FastType, "")]
    public class FastTypeTests
    {
        [Fact]
        public void Of_OnCreatingMultipleTimes_ReturnTheSameInstance()
        {
            var first = FastType.Of<StubPropertyClass>();
            var second = FastType.Of<StubPropertyClass>();

            first.Should().BeSameAs(second, "FastTypes should be cached.");
        }

        [Fact]
        public void Of_OnCreatingWithGenericAndWithType_ReturnTheSameInstance()
        {
            var first = FastType.Of<StubPropertyClass>();
            var second = FastType.Of(typeof(StubPropertyClass));

            first.Should().BeSameAs(second, "FastTypes should be cached.");
        }

        [Fact]
        public void Of_OnCreatingWithGeneric_Create()
        {
            var first = FastType.Of<StubPropertyClass>();

            first.Type.Should().Be(typeof(StubPropertyClass));
        }

        [Fact]
        public void Of_OnCreatingWithType_Create()
        {
            var first = FastType.Of(typeof(StubPropertyClass));

            first.Type.Should().Be(typeof(StubPropertyClass));
        }
    }
}
