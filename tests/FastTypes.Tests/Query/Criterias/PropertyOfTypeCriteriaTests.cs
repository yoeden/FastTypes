using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastTypes.Features.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Criterias
{
    public class PropertyOfTypeCriteriaTests
    {
        //
        // Basic Input & Structure
        //

        [Fact]
        public void New_OnNullType_Throw()
        {
            var action = () => new PropertyOfTypeCriteria(null);

            action.Should().Throw<ArgumentException>();
        }


        [Fact]
        public void New_OnValid_Creates()
        {
            //
            var criteria = new PropertyOfTypeCriteria(typeof(int));

            //
            criteria.Should().NotBeNull();
            criteria.Priority.Should().Be(QueryCriteriaPriority.Low);
        }

        //
        // Classes
        //

        [Fact]
        public void IsMatching_OnClassWithTargetPropertyType_True()
        {
            //
            var criteria = new PropertyOfTypeCriteria(typeof(int));

            //
            criteria
                .IsMatching(typeof(MyClass))
                .Should()
                .BeTrue();
        }

        [Fact]
        public void IsMatching_OnClassWithoutTargetPropertyType_False()
        {
            //
            var criteria = new PropertyOfTypeCriteria(typeof(string));

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
        public void IsMatching_OnValueTypeWithTargetPropertyType_True()
        {
            //
            var criteria = new PropertyOfTypeCriteria(typeof(int));

            //
            criteria
                .IsMatching(typeof(MyStruct))
                .Should()
                .BeTrue();
        }

        [Fact]
        public void IsMatching_OnValueTypeWithoutTargetPropertyType_False()
        {
            //
            var criteria = new PropertyOfTypeCriteria(typeof(string));

            //
            criteria
                .IsMatching(typeof(MyStruct))
                .Should()
                .BeFalse();
        }

        public struct MyStruct
        {
            public int Prop { get; }
        }

        public class MyClass
        {
            public int Prop { get; }
        }
    }
}
