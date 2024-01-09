using FastTypes.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Criterias
{
    [Trait(Traits.Query.Tag,Traits.Query.Criterias)]
    public class MethodOfTypeCriteriaTests
    {
        //
        // Basic Input & Structure
        //

        [Fact]
        public void New_OnNullType_Throw()
        {
            var action = () => new MethodOfTypeCriteria(null);

            action.Should().Throw<ArgumentException>();
        }


        [Fact]
        public void New_OnValid_Creates()
        {
            //
            var criteria = new MethodOfTypeCriteria(typeof(int));

            //
            criteria.Should().NotBeNull();
            criteria.Priority.Should().Be(QueryCriteriaPriority.Low);
        }

        //
        // Classes
        //

        [Fact]
        public void IsMatching_OnClassWithTargetMethodType_True()
        {
            //
            var criteria = new MethodOfTypeCriteria(typeof(int));

            //
            criteria
                .IsMatching(typeof(MyClass))
                .Should()
                .BeTrue();
        }

        [Fact]
        public void IsMatching_OnClassWithoutTargetMethodType_False()
        {
            //
            var criteria = new MethodOfTypeCriteria(typeof(double));

            //
            criteria
                .IsMatching(typeof(MyClass))
                .Should()
                .BeFalse();
        }

        [Fact]
        public void IsMatching_IgnoreBaseObjectMethods_False()
        {
            //
            var criteria = new MethodOfTypeCriteria(typeof(string));

            //
            criteria
                .IsMatching(typeof(MyClass))
                .Should()
                .BeFalse();
        }

        //
        // Value Types
        //

        [Fact]
        public void IsMatching_OnValueTypeWithTargetMethodType_True()
        {
            //
            var criteria = new MethodOfTypeCriteria(typeof(int));

            //
            criteria
                .IsMatching(typeof(MyStruct))
                .Should()
                .BeTrue();
        }

        [Fact]
        public void IsMatching_OnValueTypeWithoutTargetMethodType_False()
        {
            //
            var criteria = new MethodOfTypeCriteria(typeof(double));

            //
            criteria
                .IsMatching(typeof(MyStruct))
                .Should()
                .BeFalse();
        }

        [Fact]
        public void IsMatching_IgnoreBaseValueTypeMethods_False()
        {
            //
            var criteria = new MethodOfTypeCriteria(typeof(string));

            //
            criteria
                .IsMatching(typeof(MyClass))
                .Should()
                .BeFalse();
        }

        public struct MyStruct
        {
            public int Method() => 1;
        }

        public class MyClass
        {
            public int Method() => 1;
        }
    }
}
