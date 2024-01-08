using FluentAssertions;
using FastTypes.DataStructures;

namespace FastTypes.Tests.DataStructures
{
    [Trait(Traits.DataStructures.Tag, Traits.DataStructures.LockableList)]
    public class LockableListTests
    {
        [Fact]
        public void Add_OnEmptyList_ShouldAddItem()
        {
            // Arrange
            var lockableList = new LockableList<int>();

            // Act
            lockableList.Add(42);

            // Assert
            lockableList.Should().ContainSingle().Which.Should().Be(42);
        }

        [Fact]
        public void Clear_OnNonEmptyList_ShouldClearList()
        {
            // Arrange
            var lockableList = new LockableList<int>();
            lockableList.Add(42);

            // Act
            lockableList.Clear();

            // Assert
            lockableList.Should().BeEmpty();
        }

        [Theory]
        [InlineData(42, true)]
        [InlineData(99, false)]
        public void Contains_OnListWithItem_ShouldReturnCorrectResult(int itemToCheck, bool expectedResult)
        {
            // Arrange
            var lockableList = new LockableList<int>();
            lockableList.Add(42);

            // Act
            var result = lockableList.Contains(itemToCheck);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void CopyTo_OnList_ShouldCopyToDestinationArray()
        {
            // Arrange
            var lockableList = new LockableList<int>();
            lockableList.Add(42);
            lockableList.Add(99);

            var array = new int[2];

            // Act
            lockableList.CopyTo(array, 0);

            // Assert
            array.Should().Equal(42, 99);
        }

        [Theory]
        [InlineData(42, true)]
        [InlineData(99, false)]
        public void Remove_OnListWithItem_ShouldRemoveItem(int itemToRemove, bool expectedResult)
        {
            // Arrange
            var lockableList = new LockableList<int>();
            lockableList.Add(42);

            // Act
            var result = lockableList.Remove(itemToRemove);

            // Assert
            result.Should().Be(expectedResult);
            if (expectedResult) lockableList.Should().BeEmpty();
        }

        [Fact]
        public void AddRange_OnEmptyList_ShouldAddRangeOfItems()
        {
            // Arrange
            var lockableList = new LockableList<int>();
            var itemsToAdd = new List<int> { 42, 99 };

            // Act
            lockableList.AddRange(itemsToAdd);

            // Assert
            lockableList.Should().Equal(itemsToAdd);
        }

        [Fact]
        public void GetEnumerator_OnList_ShouldReturnEnumerator()
        {
            // Arrange
            var lockableList = new LockableList<int>();
            lockableList.Add(42);
            lockableList.Add(99);

            // Act
            var enumerator = lockableList.GetEnumerator();

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
        public void Indexer_OnList_ShouldReturnCorrectItem(int index, int expectedItem)
        {
            // Arrange
            var lockableList = new LockableList<int>();
            lockableList.Add(42);
            lockableList.Add(99);

            // Act
            var result = lockableList[index];

            // Assert
            result.Should().Be(expectedItem);
        }
    }
}
