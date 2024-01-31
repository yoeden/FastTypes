using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FastTypes.Tests.Copy.New
{
    [Trait(Traits.Copy.Tag, Traits.Copy.Collections)]
    public class FastCopyListTests : BaseFastCopyTests
    {
        [Trait(Traits.Copy.Tag, Traits.Copy.Primitives)]
        [Fact]
        public void DeepCopy_OnPureList_ShouldClone()
        {
            //
            var src = new List<int>() { 1, 2, 3 };

            //
            var copy = InvokeDeepCopy(src, true);

            //
            copy.Should().NotBeSameAs(src);
            copy.Should().BeEquivalentTo(src);
        }

        [Trait(Traits.Copy.Tag, Traits.Copy.RefTypes)]
        [Fact]
        public void DeepCopy_OnList_ShouldClone()
        {
            //
            var src = new ListOfTuples().Init();

            //
            var copy = InvokeDeepCopy(src, true);

            //
            copy.Should().NotBeSameAs(src);
            copy.List.Should().HaveCount(src.List.Count);
            for (var i = 0; i < copy.List.Count; i++)
            {
                copy.List[i].Should().NotBeSameAs(src.List[i]);
                copy.List[i].Item1.Should().Be(src.List[i].Item1);
            }
        }

        //

        private class ListOfTuples
        {
            public ListOfTuples Init()
            {
                List = new List<Item>() { new("A"), new("B"), new("C") };
                return this;
            }

            public List<Item> List { get; set; }

            public class Item
            {
                private Item()
                {

                }

                public Item(string item1)
                {
                    Item1 = item1;
                }

                public string Item1 { get; set; }
            }
        }
    }
}
