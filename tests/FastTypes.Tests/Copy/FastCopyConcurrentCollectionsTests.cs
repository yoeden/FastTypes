using System.Collections;
using System.Collections.Concurrent;
using FastTypes.Clone;
using FluentAssertions;

namespace FastTypes.Tests.Copy.New
{
    [Trait(Traits.Copy.Tag, Traits.Copy.Collections)]
    public class FastCopyConcurrentCollectionsTests : BaseFastCopyTests
    {
        [Trait(Traits.Copy.Tag, Traits.Copy.Primitives)]
        [Theory]
        [MemberData(nameof(Collections))]
        public void DeepCopy_OnPrimitiveCollection_ShouldClone(ICollection source)
        {
            //
            var copy = FastCopy.DeepCopy((object)source);

            //
            copy.Should().NotBeSameAs(source);
            copy.Should().BeEquivalentTo(source);
        }

        public static IEnumerable<object[]> Collections =>
            new List<object[]>
            {
                new object[] { new ConcurrentBag<int>(){1,2,3,4} },
                new object[] { new BlockingCollection<int>(){1,2,3,4} },
                new object[] { CreateIntStack() },
                new object[] { CreateIntIntDic() },
                new object[] { CreateIntIntDic() },
            };

        private static ConcurrentStack<int> CreateIntStack()
        {
            var stack = new ConcurrentStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            return stack;
        }

        private static ConcurrentQueue<int> CreateIntQueue()
        {
            var queue = new ConcurrentQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            return queue;
        }

        private static ConcurrentDictionary<int, int> CreateIntIntDic()
        {
            var dic = new ConcurrentDictionary<int, int>();
            dic.TryAdd(10, 100);
            dic.TryAdd(20, 200);
            dic.TryAdd(30, 300);
            return dic;
        }
    }
}