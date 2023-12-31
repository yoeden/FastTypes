using System.Data;
using FastTypes.Features.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Criterias
{
    public class AssignableToCriteriaTests
    {
        //
        //
        //

        [Fact]
        public void New_OnNullType_Throws()
        {
            //
            var action = () => new AssignableToCriteria(null);

            //
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void New_OnValid_Creates()
        {
            //
            var criteria = new AssignableToCriteria(typeof(int));

            //
            criteria.Should().NotBeNull();
            criteria.Priority.Should().Be(QueryCriteriaPriority.Mid);
        }

        //
        // Interfaces
        //

        [Fact]
        public void IsMatching_OnDerivedClass_WhenBaseIsInterface_True()
        {
            //
            var criteria = new AssignableToCriteria(typeof(IService));

            //
            criteria.IsMatching(typeof(ServiceImpl)).Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnDerivedInterface_WhenBaseIsInterface_True()
        {
            //
            var criteria = new AssignableToCriteria(typeof(IService));

            //
            criteria.IsMatching(typeof(IAnotherService)).Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnDerivedClass_WhenBaseIsGenericInterface_True()
        {
            //
            var criteria = new AssignableToCriteria(typeof(IGenericService<>));

            //
            criteria.IsMatching(typeof(ServiceGenericImpl)).Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnDerivedGenericInterface_WhenBaseIsGenericInterface_True()
        {
            //
            var criteria = new AssignableToCriteria(typeof(IGenericService<>));

            //
            criteria.IsMatching(typeof(IAnotherGenericService<>)).Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnNonRelatedClassWhenBaseIsInterface_False()
        {
            //
            var criteria = new AssignableToCriteria(typeof(IService));

            //
            criteria.IsMatching(typeof(AbstractBaseClass)).Should().BeFalse();
        }

        [Fact]
        public void IsMatching_OnNonRelatedClassWhenBaseIsGenericInterface_False()
        {
            //
            var criteria = new AssignableToCriteria(typeof(IGenericService<>));

            //
            criteria.IsMatching(typeof(AbstractBaseClass)).Should().BeFalse();
        }

        [Fact]
        public void IsMatching_OnNonRelatedInterfaceWhenBaseIsGenericInterface_False()
        {
            //
            var criteria = new AssignableToCriteria(typeof(IGenericService<>));

            //
            criteria.IsMatching(typeof(IService)).Should().BeFalse();
        }

        //
        // Classes
        //

        [Fact]
        public void IsMatching_OnDerivedClass_WhenBaseIsGenericClass_True()
        {
            //
            var criteria = new AssignableToCriteria(typeof(AbstractGenericBaseClass<>));

            //
            criteria.IsMatching(typeof(ServiceGenericImpl)).Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnDerivedClass_WhenBaseOfBaseClass_True()
        {
            //
            var criteria = new AssignableToCriteria(typeof(AbstractBaseClass));

            //
            criteria.IsMatching(typeof(ServiceDecoratorImpl)).Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnDerivedClass_WhenBaseOfGenericBaseClass_True()
        {
            //
            var criteria = new AssignableToCriteria(typeof(AbstractGenericBaseClass<>));

            //
            criteria.IsMatching(typeof(ServiceGenericDecoratorImpl)).Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnGenericClass_WhenBaseIsGenericClass_True()
        {
            //
            var criteria = new AssignableToCriteria(typeof(AbstractGenericBaseClass<>));

            //
            criteria.IsMatching(typeof(ServiceWithGenericImpl<>)).Should().BeTrue();
        }

        [Fact]
        public void IsMatching_OnDerivedClass_WhenBaseIsAbstractClass_True()
        {
            //
            var criteria = new AssignableToCriteria(typeof(AbstractBaseClass));

            //
            criteria.IsMatching(typeof(ServiceImpl)).Should().BeTrue();
        }

        public interface IService { }
        public interface IAnotherService : IService { }
        public interface IGenericService<T> { }
        public interface IAnotherGenericService<T> : IGenericService<T> { }
        public abstract class AbstractBaseClass { }
        public abstract class AbstractGenericBaseClass<T> { }
        public class ServiceImpl : AbstractBaseClass, IService { }
        public sealed class ServiceDecoratorImpl : ServiceImpl { }
        public sealed class ServiceGenericDecoratorImpl : ServiceGenericImpl { }
        public class ServiceGenericImpl : AbstractGenericBaseClass<int>, IGenericService<int> { }
        public class ServiceWithGenericImpl<T> : AbstractGenericBaseClass<T>, IGenericService<T> { }

    }
}