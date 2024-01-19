using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTypes.Tests.Copy
{
    public partial class FastCopyTests
    {
        [Fact]
        public void DeepCopyT_OnObjectWithNestedObject_ShouldClone()
        {
            //
            var root = new NestedRoot()
            {
                Text = "This is text",
                Nested = new NestedChild()
                {
                    Text = "This is nested text",
                    Number = 23,
                }
            };

            //
            var clone = InvokeGenericDeepCopy(root);

            //
            clone.Should().NotBeSameAs(root);
            clone.Text.Should().Be(root.Text);

            clone.Nested.Should().NotBeSameAs(root.Nested);
            clone.Nested.Number.Should().Be(root.Nested.Number);
            clone.Nested.Text.Should().Be(root.Nested.Text);
        }

        [Fact]
        public void DeepCopy_OnObjectWithNestedObject_ShouldClone()
        {
            //
            var root = new NestedRoot()
            {
                Text = "This is text",
                Nested = new NestedChild()
                {
                    Text = "This is nested text",
                    Number = 23,
                }
            };

            //
            var clone = InvokeGenericDeepCopy(root);

            //
            clone.Should().NotBeSameAs(root);
            clone.Text.Should().Be(root.Text);

            clone.Nested.Should().NotBeSameAs(root.Nested);
            clone.Nested.Number.Should().Be(root.Nested.Number);
            clone.Nested.Text.Should().Be(root.Nested.Text);
        }

        private class NestedRoot
        {
            public string Text { get; set; }
            public NestedChild Nested { get; set; }
        }

        private class NestedChild
        {
            public string Text { get; set; }
            public int Number { get; set; }
        }
    }
}
