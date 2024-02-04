using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FastTypes.Reflection;
using FluentAssertions;
using NSubstitute;

namespace FastTypes.Tests.Reflection.Method.New
{
    public class FastMethodInvokeReturnsTOnObject
    {
        [Fact]
        public void Invoke_VoidMethodWithNoArguments_Invoke()
        {
            //
            var instance = Substitute.For<MethodsClass>();
            var method = FastType.Of<MethodsClass>().Method(nameof(instance.NoReturnNoArg));

            //
            method.Invoke(instance);

            //
            var call = instance.ReceivedCalls().FirstOrDefault();

            call.Should().NotBeNull($"No call was made to method '{nameof(instance.NoReturnNoArg)}'");
            call.GetMethodInfo().Name.Should().Be(nameof(instance.NoReturnNoArg));
            call.GetArguments().Should().BeEquivalentTo(Array.Empty<object>());
        }

        [Fact]
        public void Invoke_VoidMethodWithNoArgumentsAndNullInstance_Throws()
        {
            //
            var instance = Substitute.For<MethodsClass>();
            var method = FastType.Of<MethodsClass>().Method(nameof(instance.NoReturnNoArg));

            //
            var invoke = () => method.Invoke(null);

            //
            invoke.Should().Throw<InstanceNullException>();
        }

        [Fact]
        public void Invoke_VoidMethodWithOneArguments_Invoke()
        {
            //
            var instance = Substitute.For<MethodsClass>();
            var method = FastType.Of<MethodsClass>().Method(nameof(instance.NoReturn1Arg));
            var args = new object[] { 23 };

            //
            method.Invoke(instance, 23);

            //
            AssertCall(instance, nameof(instance.NoReturn1Arg), 23);
        }

        [Fact]
        public void Invoke_VoidMethodWithOneArgumentsAndNullInstance_Throws()
        {
            //
            var instance = Substitute.For<MethodsClass>();
            var method = FastType.Of<MethodsClass>().Method(nameof(instance.NoReturnNoArg));

            //
            var invoke = () => method.Invoke(null, 23);

            //
            invoke.Should().Throw<InstanceNullException>();
        }

        //TODO: Reuse this when re-writing unit tests
        protected void AssertCall<T>(T instance, string methodName, params object[] args) where T : class
        {
            //
            var call = instance.ReceivedCalls().FirstOrDefault();

            //
            call.Should().NotBeNull($"No call was made to method '{methodName}'");
            call.GetMethodInfo().Name.Should().Be(methodName);
            call.GetArguments().Should().BeEquivalentTo(args);
        }
    }
}
