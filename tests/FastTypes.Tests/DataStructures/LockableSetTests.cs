using FastTypes.DataStructures;
using FluentAssertions;

namespace FastTypes.Tests.DataStructures
{
    [Trait(Traits.DataStructures.Tag, Traits.DataStructures.LockableSet)]
    public class LockableSetTests
    {
        [Fact]
        public void Add_OnEmptySet_ShouldAddItem()
        {
            // Arrange
            var lockableSet = new LockableSet<int>();

            // Act
            lockableSet.Add(42);

            // Assert
            lockableSet.Should().ContainSingle().Which.Should().Be(42);
        }

        [Fact]
        public void Add_OnDuplicateItem_ShouldNotAddItem()
        {
            // Arrange
            var lockableSet = new LockableSet<int>();
            lockableSet.Add(42);

            // Act
            lockableSet.Add(42);

            // Assert
            lockableSet.Should().ContainSingle().Which.Should().Be(42);
        }

        [Fact]
        public void Clear_OnNonEmptySet_ShouldClearSet()
        {
            // Arrange
            var lockableSet = new LockableSet<int>();
            lockableSet.Add(42);

            // Act
            lockableSet.Clear();

            // Assert
            lockableSet.Should().BeEmpty();
        }

        [Theory]
        [InlineData(42, true)]
        [InlineData(99, false)]
        public void Contains_OnSetWithItem_ShouldReturnCorrectResult(int itemToCheck, bool expectedResult)
        {
            // Arrange
            var lockableSet = new LockableSet<int>();
            lockableSet.Add(42);

            // Act
            var result = lockableSet.Contains(itemToCheck);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void Remove_OnSetWithItem_ShouldRemoveItem()
        {
            // Arrange
            var lockableSet = new LockableSet<int>();
            lockableSet.Add(42);

            // Act
            var result = lockableSet.Remove(42);

            // Assert
            result.Should().BeTrue();
            lockableSet.Should().BeEmpty();
        }

        [Fact]
        public void Remove_OnNonExistingItem_ShouldNotModifySet()
        {
            // Arrange
            var lockableSet = new LockableSet<int>();
            lockableSet.Add(42);

            // Act
            var result = lockableSet.Remove(99);

            // Assert
            result.Should().BeFalse();
            lockableSet.Should().ContainSingle().Which.Should().Be(42);
        }

        [Fact]
        public void AddRange_OnEmptySet_ShouldAddRangeOfItems()
        {
            // Arrange
            var lockableSet = new LockableSet<int>();
            var itemsToAdd = new List<int> { 42, 99 };

            // Act
            lockableSet.AddRange(itemsToAdd);

            // Assert
            lockableSet.Should().Equal(itemsToAdd);
        }

        [Fact]
        public void GetEnumerator_OnSet_ShouldReturnEnumerator()
        {
            // Arrange
            var lockableSet = new LockableSet<int>();
            lockableSet.Add(42);
            lockableSet.Add(99);

            // Act
            var enumerator = lockableSet.GetEnumerator();

            // Assert
            enumerator.MoveNext().Should().BeTrue();
            enumerator.Current.Should().Be(42);
            enumerator.MoveNext().Should().BeTrue();
            enumerator.Current.Should().Be(99);
            enumerator.MoveNext().Should().BeFalse();
        }

        [Theory]
        [InlineData(0, 42)]
        [InlineData(1, 99)]
        public void Indexer_OnSet_ShouldReturnCorrectItem(int index, int expectedItem)
        {
            // Arrange
            var lockableSet = new LockableSet<int>();
            lockableSet.Add(42);
            lockableSet.Add(99);

            // Act
            var result = lockableSet[index];

            // Assert
            result.Should().Be(expectedItem);
        }
    }
}