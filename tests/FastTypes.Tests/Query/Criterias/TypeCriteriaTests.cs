using System.Reflection;
using FastTypes.Features.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Criterias
{
    public class TypeCriteriaTests
    {
        [Fact]
        public void New_OnNoCriteria_Throws()
        {
            // Arrange
            var criteria =  () => new TypeCriteria(isClass: false, isValueType: false, isInterface: false, isEnum: false);

            // Assert
            criteria.Should().Throw<ArgumentException>();
        }


        [Fact]
        public void IsMatching_OnClassType_WithIsClassCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new TypeCriteria(isClass: true, isValueType: false, isInterface: false, isEnum: false);
            var type = typeof(Class);

            // Act
            var result = criteria.IsMatching(type);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnEnumType_WithEnumCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new TypeCriteria(isClass: false, isValueType: false, isInterface: false, isEnum: true);
            var type = typeof(AssemblyFlags);

            // Act
            var result = criteria.IsMatching(type);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnValueType_WithIsValueTypeCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new TypeCriteria(isClass: false, isValueType: true, isInterface: false, isEnum: false);
            var type = typeof(int);

            // Act
            var result = criteria.IsMatching(type);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnEnumType_WithValueTypeCriteria_ReturnsFalse()
        {
            // Arrange
            var criteria = new TypeCriteria(isClass: false, isValueType: true, isInterface: false, isEnum: false);
            var type = typeof(AssemblyFlags);

            // Act
            var result = criteria.IsMatching(type);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsMatching_OnInterfaceType_WithIsInterfaceCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new TypeCriteria(isClass: false, isValueType: false, isInterface: true, isEnum: false);
            var type = typeof(IInterface);

            // Act
            var result = criteria.IsMatching(type);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnClassType_WithNoMatchingCriteria_ReturnsFalse()
        {
            // Arrange
            var criteria = () => new TypeCriteria(isClass: false, isValueType: false, isInterface: false, isEnum: false);

            //
            criteria.Should().Throw<ArgumentException>();
        }

        public interface IInterface { }
        public class Class { }
    }
}