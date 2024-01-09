using FastTypes.DataStructures;
using FluentAssertions;

namespace FastTypes.Tests.DataStructures
{
    [Trait(Traits.DataStructures.Tag, Traits.DataStructures.UnmodifiableFastDictionaryByInt)]
    public class UnmodifiableFastDictionaryByIntTests
    {
        [Fact]
        public void Create_OnEmptyValues_ShouldReturnEmptyDictionary()
        {
            // Arrange
            var values = new List<KeyValuePair<int, int>>();

            // Act
            var dictionary = UnmodifiableFastDictionaryByInt<int>.Create(values);

            // Assert
            dictionary.ContainsKey(42).Should().BeFalse();
        }

        [Fact]
        public void Create_OnNonEmptyValues_ShouldReturnDictionaryWithCorrectValues()
        {
            // Arrange
            var values = new List<KeyValuePair<int, int>>
            {
                new(1, 42),
                new(2, 99),
                new(3, 77)
            };

            // Act
            var dictionary = UnmodifiableFastDictionaryByInt<int>.Create(values);

            // Assert
            dictionary[1].Should().Be(42);
            dictionary[2].Should().Be(99);
            dictionary[3].Should().Be(77);
        }

        [Fact]
        public void ContainsKey_OnDictionaryWithKey_ShouldReturnTrue()
        {
            // Arrange
            var values = new List<KeyValuePair<int, string>>
            {
                new(1, "One"),
                new(2, "Two"),
                new(3, "Three")
            };

            var dictionary = UnmodifiableFastDictionaryByInt<string>.Create(values);

            // Act
            var containsKey = dictionary.ContainsKey(2);

            // Assert
            containsKey.Should().BeTrue();
        }

        [Fact]
        public void ContainsKey_OnDictionaryWithoutKey_ShouldReturnFalse()
        {
            // Arrange
            var values = new List<KeyValuePair<int, string>>
            {
                new(1, "One"),
                new(2, "Two"),
                new(3, "Three")
            };

            var dictionary = UnmodifiableFastDictionaryByInt<string>.Create(values);

            // Act
            var containsKey = dictionary.ContainsKey(4);

            // Assert
            containsKey.Should().BeFalse();
        }

        [Fact]
        public void AccessingNonExistingKey_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var values = new List<KeyValuePair<int, string>>
            {
                new(1, "One"),
                new(2, "Two"),
                new(3, "Three")
            };

            var dictionary = UnmodifiableFastDictionaryByInt<string>.Create(values);

            // Act & Assert
            FluentActions.Invoking(() => dictionary[4]).Should().Throw<KeyNotFoundException>();
        }
    }
}