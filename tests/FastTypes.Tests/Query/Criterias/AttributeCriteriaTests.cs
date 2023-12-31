using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FastTypes.Features.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Criterias
{
    public class AttributeCriteriaTests
    {
        [Fact]
        public void New_OnNullAttributeType_Throws()
        {
            //
            var criteria = () => new AttributeCriteria(null);

            criteria.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void New_OnNonAttributeType_Throws()
        {
            //
            var criteria = () => new AttributeCriteria(typeof(object));

            criteria.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void New_OnAttributeType_Create()
        {
            //
            var criteria = new AttributeCriteria(typeof(SerializableAttribute));

            criteria.Should().NotBeNull();
            criteria.Priority.Should().Be(QueryCriteriaPriority.Low);
        }

        //
        //
        //

        [Fact]
        public void IsMatching_OnClassWithMatchingAttribute_True()
        {
            //
            var criteria = new AttributeCriteria(typeof(SerializableAttribute));

            //
            criteria
                .IsMatching(typeof(ClassWithAttribute))
                .Should()
                .BeTrue();
        }

        [Fact]
        public void IsMatching_OnClassWithoutMatchingAttribute_False()
        {
            //
            var criteria = new AttributeCriteria(typeof(SerializableAttribute));

            //
            criteria
                .IsMatching(typeof(ClassWithoutAttribute))
                .Should()
                .BeFalse();
        }


        [System.Serializable]
        public class ClassWithAttribute { }

        public class ClassWithoutAttribute { }
    }
}
