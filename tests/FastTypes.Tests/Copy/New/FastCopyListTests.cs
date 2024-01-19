using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FastTypes.Tests.Copy.New
{
    public class FastCopyListTests : BaseFastCopyTests
    {
        [Fact]
        public void DeepCopy_OnPureList_ShouldClone()
        {
            //
            var src = new PureList().Init();

            //
            var copy = InvokeDeepCopy(src, true);

            //
            copy.Should().NotBeSameAs(src);
            copy.List.Should().BeEquivalentTo(src.List);
        }

        //

        private class PureList
        {
            public PureList Init()
            {
                List = new List<int>() { 1, 2, 3 };
                return this;
            }

            public List<int> List { get; set; }
        }
    }
}
