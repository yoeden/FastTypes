using FastTypes.Features.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Builder
{
    public class TypeQueryBuilderTypesTests
    {
        private static TypeCriteria GetTypeSelectorCriteriaFromBuilder(Action<ITypeSelector> action)
        {
            var builder = new TypeQueryBuilder();
            return (TypeCriteria)builder.FromAllAssemblies()
                .Targeting(action)
                .Prepare()
                .Criterias
                .FirstOrDefault(criteria => criteria is TypeCriteria);
        }

        [Fact]
        public void Targeting_OnClasses_HaveClasses()
        {
            //
            var types = GetTypeSelectorCriteriaFromBuilder(s => s.Classes());

            //
            types.IsClass.Should().BeTrue();
            types.IsValueType.Should().BeFalse();
            types.IsInterface.Should().BeFalse();
            types.IsEnum.Should().BeFalse();
        }

        [Fact]
        public void Targeting_OnValueTypes_HaveValueTypes()
        {
            //
            var types = GetTypeSelectorCriteriaFromBuilder(s => s.ValueTypes());

            //
            types.IsValueType.Should().BeTrue();
            types.IsClass.Should().BeFalse();
            types.IsInterface.Should().BeFalse();
            types.IsEnum.Should().BeFalse();
        }

        [Fact]
        public void Targeting_OnEnums_HaveEnums()
        {
            //
            var types = GetTypeSelectorCriteriaFromBuilder(s => s.Enums());

            //
            types.IsEnum.Should().BeTrue();
            types.IsValueType.Should().BeFalse();
            types.IsClass.Should().BeFalse();
            types.IsInterface.Should().BeFalse();
        }

        [Fact]
        public void Targeting_OnInterface_HaveInterface()
        {
            //
            var types = GetTypeSelectorCriteriaFromBuilder(s => s.Interfaces());

            //
            types.IsInterface.Should().BeTrue();
            types.IsValueType.Should().BeFalse();
            types.IsClass.Should().BeFalse();
            types.IsEnum.Should().BeFalse();
        }

        [Fact]
        public void Targeting_OnInterfaceAndClasses_HaveInterfaceAndClasses()
        {
            //
            var types = GetTypeSelectorCriteriaFromBuilder(s => s.Interfaces().Classes());

            //
            types.IsInterface.Should().BeTrue();
            types.IsClass.Should().BeTrue();
            types.IsValueType.Should().BeFalse();
            types.IsEnum.Should().BeFalse();
        }
    }
}