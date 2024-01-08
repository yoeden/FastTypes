using FastTypes.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Criterias
{
    [Trait(Traits.Query.Tag,Traits.Query.Criterias)]
    public class AccessModifiersCriteriaTests
    {
        [Fact]
        public void IsMatching_OnPublicClassesWhenCriteriaIsPublic_True()
        {
            //
            var criteria = new AccessModifierCriteria(isPublic: true);

            //
            criteria.IsMatching(typeof(AccessModifierCriteriaPublicClass)).Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnInternalClassesWhenCriteriaIsPublic_False()
        {
            //
            var criteria = new AccessModifierCriteria(isPublic: true);

            //
            criteria.IsMatching(typeof(AccessModifierCriteriaInternalClass)).Should().BeFalse();
        }

        [Fact]
        public void IsMatching_OnPublicClassesWhenCriteriaIsNotPublic_False()
        {
            //
            var criteria = new AccessModifierCriteria(isPublic: false);

            //
            criteria.IsMatching(typeof(AccessModifierCriteriaPublicClass)).Should().BeFalse();
        }

        [Fact]
        public void IsMatching_OnInternalClassesWhenCriteriaIsNotPublic_True()
        {
            //
            var criteria = new AccessModifierCriteria(isPublic: false);

            //
            criteria.IsMatching(typeof(AccessModifierCriteriaInternalClass)).Should().BeTrue();
        }

        //
        //
        //

        [Fact]
        public void IsMatching_OnNestedPublicClasses_WhenCriteriaIsPublic_True()
        {
            //
            var criteria = new AccessModifierCriteria(isPublic: true);

            //
            criteria.IsMatching(typeof(NestedPublicClass)).Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnNestedInternalClasses_WhenCriteriaIsPublic_False()
        {
            //
            var criteria = new AccessModifierCriteria(isPublic: true);

            //
            criteria.IsMatching(typeof(NestedInternalClass)).Should().BeFalse();
        }

        [Fact]
        public void IsMatching_OnNestedPublicClasses_WhenCriteriaIsNotPublic_False()
        {
            //
            var criteria = new AccessModifierCriteria(isPublic: false);

            //
            criteria.IsMatching(typeof(NestedPublicClass)).Should().BeFalse();
        }

        [Fact]
        public void IsMatching_OnNestedInternalClasses_WhenCriteriaIsNotPublic_True()
        {
            //
            var criteria = new AccessModifierCriteria(isPublic: false);

            //
            criteria.IsMatching(typeof(NestedInternalClass)).Should().BeTrue();
        }

        public class NestedPublicClass { }
        internal class NestedInternalClass { }
    }

    public class AccessModifierCriteriaPublicClass { }
    internal class AccessModifierCriteriaInternalClass { }
}