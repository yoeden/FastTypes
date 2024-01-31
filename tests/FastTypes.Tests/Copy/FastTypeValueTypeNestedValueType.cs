using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastTypes.Tests.Copy.New;

namespace FastTypes.Tests.Copy
{
    [Trait(Traits.Copy.Tag, Traits.Copy.PureValueType)]
    public class FastTypeValueTypeNestedValueType : BaseFastCopyTests
    {
        [Trait(Traits.Copy.Tag, Traits.Copy.Generic)]
        [Fact]
        public void DeepCopyT_OnValueTypeWithNestedValueType_ShouldClone()
        {
            //
            var root = new NestedValueRoot()
            {
                Text = "This is text",
                Nested = new NestedValueChild()
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

        [Trait(Traits.Copy.Tag, Traits.Copy.Object)]
        [Fact]
        public void DeepCopy_OnValueTypeWithNestedValueType_ShouldClone()
        {
            //
            var root = new NestedValueRoot()
            {
                Text = "This is text",
                Nested = new NestedValueChild()
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

        private struct NestedValueRoot
        {
            public string Text { get; set; }
            public NestedValueChild Nested { get; set; }
        }

        private struct NestedValueChild
        {
            public string Text { get; set; }
            public int Number { get; set; }
        }
    }
}
