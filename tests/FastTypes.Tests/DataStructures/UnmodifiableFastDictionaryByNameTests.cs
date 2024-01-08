using FastTypes.DataStructures;
using FluentAssertions;

namespace FastTypes.Tests.DataStructures
{
    [Trait(Traits.DataStructures.Tag, Traits.DataStructures.UnmodifiableFastDictionaryByString)]
    public class UnmodifiableFastDictionaryByNameTests
    {
        [Fact]
        public void Create_OnEmptyValues_ShouldReturnEmptyDictionary()
        {
            // Arrange
            var values = new List<KeyValuePair<string, int>>();

            // Act
            var dictionary = UnmodifiableFastDictionaryByName<int>.Create(values);

            // Assert
            dictionary.ContainsKey("Key").Should().BeFalse();
            dictionary.Values.Should().BeEmpty();
        }

        [Fact]
        public void Create_OnNonEmptyValues_ShouldReturnDictionaryWithCorrectValues()
        {
            // Arrange
            var values = new List<KeyValuePair<string, int>>
            {
                new("One", 1),
                new("Two", 2),
                new("Three", 3)
            };

            // Act
            var dictionary = UnmodifiableFastDictionaryByName<int>.Create(values);

            // Assert
            dictionary["One"].Should().Be(1);
            dictionary["Two"].Should().Be(2);
            dictionary["Three"].Should().Be(3);

            dictionary.ContainsKey("Four").Should().BeFalse();
            dictionary.Values.Should().Equal(1, 2, 3);
        }

        [Fact]
        public void ContainsKey_OnDictionaryWithKey_ShouldReturnTrue()
        {
            // Arrange
            var values = new List<KeyValuePair<string, string>>
            {
                new("One", "ValueOne"),
                new("Two", "ValueTwo"),
                new("Three", "ValueThree")
            };

            var dictionary = UnmodifiableFastDictionaryByName<string>.Create(values);

            // Act
            var containsKey = dictionary.ContainsKey("Two");

            // Assert
            containsKey.Should().BeTrue();
        }

        [Fact]
        public void ContainsKey_OnDictionaryWithoutKey_ShouldReturnFalse()
        {
            // Arrange
            var values = new List<KeyValuePair<string, string>>
            {
                new("One", "ValueOne"),
                new("Two", "ValueTwo"),
                new("Three", "ValueThree")
            };

            var dictionary = UnmodifiableFastDictionaryByName<string>.Create(values);

            // Act
            var containsKey = dictionary.ContainsKey("Four");

            // Assert
            containsKey.Should().BeFalse();
        }

        [Fact]
        public void AccessingNonExistingKey_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var values = new List<KeyValuePair<string, string>>
            {
                new("One", "ValueOne"),
                new("Two", "ValueTwo"),
                new("Three", "ValueThree")
            };

            var dictionary = UnmodifiableFastDictionaryByName<string>.Create(values);

            // Act & Assert
            FluentActions.Invoking(() => dictionary["Four"]).Should().Throw<KeyNotFoundException>();
        }
    }
}