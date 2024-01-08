using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void IsVoid_OnVoidMethod_ReturnsFalse()
        {
            //
            var method = _fastType.Method(nameof(MethodsClass.NoReturnNoArguments));

            //
            method.IsVoid.Should().BeTrue();
        }

        [Fact]
        public void IsVoid_OnReturnableMethod_ReturnsTrue()
        {
            //
            var method = _fastType.Method(nameof(MethodsClass.ReturnNoArguments));

            //
            method.IsVoid.Should().BeFalse();
        }


        [Fact]
        public void ReturnType_OnVoidMethod_ReturnsNull()
        {
            //
            var method = _fastType.Method(nameof(MethodsClass.NoReturnNoArguments));

            //
            method.ReturnType.Should().Be(typeof(void));
        }

        [Fact]
        public void ReturnType_OnReturnableMethod_ReturnsReturnType()
        {
            //
            var method = _fastType.Method(nameof(MethodsClass.ReturnNoArguments));

            //
            method.ReturnType.Should().Be(typeof(int));
        }
    }
}
