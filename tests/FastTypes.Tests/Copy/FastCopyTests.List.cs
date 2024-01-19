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
        [Trait(Traits.Copy.Tag, Traits.Copy.Generic)]
        [Fact]
        public void DeepCopyT_OnListWithPureValueTypes_ShouldClone()
        {
            //
            var root = new ObjectWithList<int>();
            root.List.Add(1);
            root.List.Add(5);
            root.List.Add(10);

            //
            var clone = InvokeGenericDeepCopy(root);

            //
            clone.Should().NotBeSameAs(root);
            clone.List.Should().Contain(root.List);
        }

        [Trait(Traits.Copy.Tag, Traits.Copy.Generic)]
        [Fact]
        public void DeepCopyT_OnListWithComplexObject_ShouldClone()
        {
            //
            var root = new ObjectWithList<ObjectWithListComplexItem>();
            root.List.Add(new ObjectWithListComplexItem(1));
            root.List.Add(new ObjectWithListComplexItem(5));
            root.List.Add(new ObjectWithListComplexItem(10));

            //
            var clone = InvokeGenericDeepCopy(root);

            //
            clone.Should().NotBeSameAs(root);
            clone.List.Should().Contain(root.List);
        }

        private class ObjectWithList<T>
        {
            public ObjectWithList()
            {
                List = new List<T>();
            }

            public List<T> List { get; set; }
        }

        private class ObjectWithListComplexItem
        {
            public ObjectWithListComplexItem(int number)
            {
                Number = number;
            }

            private ObjectWithListComplexItem() { }

            // ReSharper disable once MemberCanBePrivate.Local
            public int Number { get; set; }

            private bool Equals(ObjectWithListComplexItem other)
            {
                return Number == other.Number;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }

                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                if (obj.GetType() != GetType())
                {
                    return false;
                }

                return Equals((ObjectWithListComplexItem)obj);
            }

            public override int GetHashCode()
            {
                return Number;
            }
        }
    }
}
