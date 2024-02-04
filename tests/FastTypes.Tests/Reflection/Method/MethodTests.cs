using FastTypes.Reflection;
using FluentAssertions;

namespace FastTypes.Tests.Reflection.Method
{
    [Trait(Traits.Reflection.Tag, Traits.Reflection.Method)]
    public class MethodTests
    {
        protected readonly IFastType<MethodsClass> _fastType;

        public MethodTests()
        {
            _fastType = FastType.Of<MethodsClass>();
        }

        [Fact]
        public void IsVoid_OnVoidMethod_ReturnsTrue()
        {
            //
            var method = _fastType.Method(nameof(MethodsClass.NoReturnNoArg));

            //
            method.IsVoid.Should().BeTrue();
        }

        [Fact]
        public void IsVoid_OnReturnableMethod_ReturnsFalse()
        {
            //
            var method = _fastType.Method(nameof(MethodsClass.ReturnNoArg));

            //
            method.IsVoid.Should().BeFalse();
        }


        [Fact]
        public void ReturnType_OnVoidMethod_ReturnsNull()
        {
            //
            var method = _fastType.Method(nameof(MethodsClass.NoReturnNoArg));

            //
            method.ReturnType.Should().Be(typeof(void));
        }

        [Fact]
        public void ReturnType_OnReturnableMethod_ReturnsReturnType()
        {
            //
            var method = _fastType.Method(nameof(MethodsClass.ReturnNoArg));

            //
            method.ReturnType.Should().Be(typeof(int));
        }

        [Fact]
        public void Name_MethodName()
        {
            //
            var method = _fastType.Method(nameof(MethodsClass.ReturnNoArg));

            //
            method.Name.Should().Be(nameof(MethodsClass.ReturnNoArg));
        }

        [Fact]
        public void IsStatic_InstanceMethod_False()
        {
            //
            var method = _fastType.Method(nameof(MethodsClass.ReturnNoArg));

            //
            method.IsStatic.Should().BeFalse();
        }

        [Fact]
        public void IsStatic_InstanceMethod_True()
        {
            //
            var method = _fastType.Method(nameof(MethodsClass.StaticNoReturnNoArg));

            //
            method.IsStatic.Should().BeTrue();
        }

        [Fact]
        public void Parameters_2Parameters()
        {
            //
            var method = _fastType.Method(nameof(MethodsClass.NoReturn2Arg));

            //
            method.Parameters.Should().BeEquivalentTo(typeof(MethodsClass).GetMethod(nameof(MethodsClass.NoReturn2Arg)).GetParameters());
        }

        [Fact]
        public void Method_OnNonExistingVoidMethod_Throw()
        {
            var action = () => _fastType.Method("DoesntExists");

            action.Should().Throw<MethodDoesntExistsException>();
        }

        [Fact]
        public void Method_OnNonExistingReturnableMethod_Throw()
        {
            var action = () => _fastType.Method<int>("DoesntExists");

            action.Should().Throw<MethodDoesntExistsException>();
        }

        [Fact]
        public void Method_OnUnexpectedReturnType_Throw()
        {
            var action = () => _fastType.Method<string>(nameof(MethodsClass.Return1Arg));

            action.Should().Throw<UnexpectedMethodReturnTypeException>();
        }
    }
}
