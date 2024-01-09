using System.Reflection;
using FastTypes.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Instantiator
{
    [Trait(Traits.Query.Tag,Traits.Query.Instanciator)]
    public class QueryInstantiatorTests
    {
        [Fact]
        public void Instanciate_OnDefaultCtorClasses_ShouldInstanciate()
        {
            //
            var result = FastType
                .Query()
                .FromAssembly(Assembly.GetExecutingAssembly())
                .TargetClasses()
                .AssignableTo<InstanceClass>()
                .Instanciate();

            //
            result.Should().HaveCount(1);
            result.Should().AllBeOfType<InstanceClass>();
        }

        [Fact]
        public void Instanciate_OnCustomInstanciatorForClasses_ShouldInstanciate()
        {
            //
            var result = FastType
                .Query()
                .FromAssembly(Assembly.GetExecutingAssembly())
                .TargetClasses()
                .AssignableTo<InstanceClassIntParameter>()
                .Instanciate(activator => activator.NewObject(23));

            //
            result.Should().HaveCount(1);
            result.Should().AllBeOfType<InstanceClassIntParameter>();
        }

        [Fact]
        public void Instanciate_OnNonInstnaciatableType_ShouldThrow()
        {
            //
            var result = () => FastType
                .Query()
                .FromAssembly(Assembly.GetExecutingAssembly())
                .TargetInterfaces()
                .AssignableTo<InterfaceStub>()
                .Instanciate(activator => activator.NewObject(23));

            //
            result.Should().Throw<FailedToInstanciateTypeException>();
        }

        [Fact]
        public void InstanciateAs_OnDefaultCtorClasses_ShouldInstanciate()
        {
            //
            var result = FastType
                .Query()
                .FromAssembly(Assembly.GetExecutingAssembly())
                .TargetClasses()
                .AssignableTo<InstanceClass>()
                .InstanciateAs<InstanceClass>();

            //
            result.Should().HaveCount(1);
            result.Should().AllBeOfType<InstanceClass>();
        }

        [Fact]
        public void InstanciateAs_OnCustomInstanciatorForClasses_ShouldInstanciate()
        {
            //
            var result = FastType
                .Query()
                .FromAssembly(Assembly.GetExecutingAssembly())
                .TargetClasses()
                .AssignableTo<InstanceClassIntParameter>()
                .InstanciateAs<InstanceClassIntParameter>(activator => activator.NewObject(23));

            //
            result.Should().HaveCount(1);
            result.Should().AllBeOfType<InstanceClassIntParameter>();
        }

        [Fact]
        public void InstanciateAs_OnNonInstnaciatableType_ShouldThrow()
        {
            //
            var result = () => FastType
                .Query()
                .FromAssembly(Assembly.GetExecutingAssembly())
                .TargetInterfaces()
                .AssignableTo<InterfaceStub>()
                .InstanciateAs<InterfaceStub>(activator => activator.NewObject(23));

            //
            result.Should().Throw<FailedToInstanciateTypeException>();
        }

        public class InstanceClass { }

        public interface InterfaceStub { }

        public class InstanceClassIntParameter
        {
            public InstanceClassIntParameter(int prop)
            {
                Prop = prop;
            }

            public int Prop { get; set; }
        }
    }


}
