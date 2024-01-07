using System.Reflection;
using FastTypes.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Builder
{
    public class TypeQueryBuilderModifiersTests
    {
        private static T GetCriteriaFromBuilder<T>(Func<ITypeQueryBuilderModifiers, ITypeQueryBuilderModifiers> func)
            where T : ITypeQueryCriteria
        {
            var builder = new TypeQueryBuilder();
            return (T)func(
                    builder
                        .FromAllAssemblies()
                        .Targeting(s => s.Classes().Interfaces().ValueTypes())
                )
                .Prepare()
                .Groups[0]
                .Criterias
                .FirstOrDefault(criteria => criteria is T);
        }

        [Fact]
        public void NotPublic_ShouldHaveCriteria()
        {
            //
            var criteria = GetCriteriaFromBuilder<AccessModifierCriteria>(
                modifiers => modifiers.NotPublic()
            );

            //
            criteria.Should().NotBeNull();
        }

        [Fact]
        public void AssignableTo_ShouldHaveCriteria()
        {
            //
            var criteria = GetCriteriaFromBuilder<AssignableToCriteria>(
                modifiers => modifiers.AssignableTo(typeof(object))
            );

            //
            criteria.Should().NotBeNull();
        }

        [Fact]
        public void AssignableToT_ShouldHaveCriteria()
        {
            //
            var criteria = GetCriteriaFromBuilder<AssignableToCriteria>(
                modifiers => modifiers.AssignableTo<object>()
            );

            //
            criteria.Should().NotBeNull();
        }

        [Fact]
        public void WithAttribute_ShouldHaveCriteria()
        {
            //
            var criteria = GetCriteriaFromBuilder<AttributeCriteria>(
                modifiers => modifiers.WithAttribute(typeof(DefaultMemberAttribute))
            );

            //
            criteria.Should().NotBeNull();
        }

        [Fact]
        public void WithAttributeT_ShouldHaveCriteria()
        {
            //
            var criteria = GetCriteriaFromBuilder<AttributeCriteria>(
                modifiers => modifiers.WithAttribute<DefaultMemberAttribute>()
            );

            //
            criteria.Should().NotBeNull();
        }

        [Fact]
        public void WithMethodOfType_ShouldHaveCriteria()
        {
            //
            var criteria = GetCriteriaFromBuilder<MethodOfTypeCriteria>(
                modifiers => modifiers.WithMethodOfType(typeof(DefaultMemberAttribute))
            );

            //
            criteria.Should().NotBeNull();
        }

        [Fact]
        public void WithMethodOfTypeT_ShouldHaveCriteria()
        {
            //
            var criteria = GetCriteriaFromBuilder<MethodOfTypeCriteria>(
                modifiers => modifiers.WithMethodOfType<DefaultMemberAttribute>()
            );

            //
            criteria.Should().NotBeNull();
        }

        [Fact]
        public void WithPropertyOfType_ShouldHaveCriteria()
        {
            //
            var criteria = GetCriteriaFromBuilder<PropertyOfTypeCriteria>(
                modifiers => modifiers.WithPropertyOfType(typeof(DefaultMemberAttribute))
            );

            //
            criteria.Should().NotBeNull();
        }

        [Fact]
        public void WithPropertyOfTypeT_ShouldHaveCriteria()
        {
            //
            var criteria = GetCriteriaFromBuilder<PropertyOfTypeCriteria>(
                modifiers => modifiers.WithPropertyOfType<DefaultMemberAttribute>()
            );

            //
            criteria.Should().NotBeNull();
        }
    }
}