using System.Collections;
using FastTypes.Reflection;
using FluentAssertions;
using NSubstitute;

namespace FastTypes.Tests.Reflection.Method
{
    public sealed class Args
    {
        public Args()
        {
            Arg1 = ExpectedValues.RandomInt();
            Arg2 = ExpectedValues.RandomInt();
            Arg3 = ExpectedValues.RandomInt();
            Arg4 = ExpectedValues.RandomInt();
            Arg5 = ExpectedValues.RandomInt();
        }

        public int Arg1 { get; }
        public int Arg2 { get; }
        public int Arg3 { get; }
        public int Arg4 { get; }
        public int Arg5 { get; }
    }

    [Trait(Traits.Reflection.Tag, Traits.Reflection.Method)]
    public class MethodTests_Void_OnObject
    {
        //DONT CHANGE, it should be invoked on object overloads
        protected readonly IFastType _fastType;
        protected readonly MethodsClass _instance;

        public MethodTests_Void_OnObject()
        {
            _fastType = FastType.Of<MethodsClass>();
            _instance = Substitute.For<MethodsClass>();
        }

        [Theory]
        [InlineData(0, nameof(MethodsClass.NoReturnNoArguments))]
        [InlineData(1, nameof(MethodsClass.NoReturn1Arg))]
        [InlineData(2, nameof(MethodsClass.NoReturn2Arg))]
        [InlineData(3, nameof(MethodsClass.NoReturn3Arg))]
        [InlineData(4, nameof(MethodsClass.NoReturn4Arg))]
        [InlineData(5, nameof(MethodsClass.NoReturn5Arg))]
        public void Invoke_OnValidArguments_ShouldInvoke(int argsCount, string name)
        {
            //
            var args = new Args();
            var expectedArgs = Array.Empty<object>();
            var method = _fastType.Method(name);

            //
            switch (argsCount)
            {
                case 0:
                    method.Invoke(_instance);
                    expectedArgs = new object[] { };
                    break;
                case 1:
                    method.Invoke(_instance, args.Arg1);
                    expectedArgs = new object[] { args.Arg1 };
                    break;
                case 2:
                    method.Invoke(_instance, args.Arg1, args.Arg2);
                    expectedArgs = new object[] { args.Arg1, args.Arg2 };
                    break;
                case 3:
                    method.Invoke(_instance, args.Arg1, args.Arg2, args.Arg3);
                    expectedArgs = new object[] { args.Arg1, args.Arg2, args.Arg3 };
                    break;
                case 4:
                    method.Invoke(_instance, args.Arg1, args.Arg2, args.Arg3, args.Arg4);
                    expectedArgs = new object[] { args.Arg1, args.Arg2, args.Arg3, args.Arg4 };
                    break;
                case 5:
                    method.Invoke(_instance, args.Arg1, args.Arg2, args.Arg3, args.Arg4, args.Arg5);
                    expectedArgs = new object[] { args.Arg1, args.Arg2, args.Arg3, args.Arg4, args.Arg5 };
                    break;
            }

            //
            var call = _instance.ReceivedCalls().FirstOrDefault();
            call.Should().NotBeNull($"No call was made to method '{name}'");

            call.GetMethodInfo().Name.Should().Be(name);
            call.GetArguments().Should().BeEquivalentTo(expectedArgs);
        }

        [Theory]
        [InlineData(0, nameof(MethodsClass.NoReturnNoArguments))]
        [InlineData(1, nameof(MethodsClass.NoReturn1Arg))]
        [InlineData(2, nameof(MethodsClass.NoReturn2Arg))]
        [InlineData(3, nameof(MethodsClass.NoReturn3Arg))]
        [InlineData(4, nameof(MethodsClass.NoReturn4Arg))]
        [InlineData(5, nameof(MethodsClass.NoReturn5Arg))]
        public void Invoke_OnInstanceIsNull_ShouldThrow(int argsCount, string name)
        {
            //
            var args = new Args();
            var action = static () => { };
            var method = _fastType.Method(name);

            //
            switch (argsCount)
            {
                case 0:
                    action = () => method.Invoke(null);
                    break;
                case 1:
                    action = () => method.Invoke(null, args.Arg1);
                    break;
                case 2:
                    action = () => method.Invoke(null, args.Arg1, args.Arg2);
                    break;
                case 3:
                    action = () => method.Invoke(null, args.Arg1, args.Arg2, args.Arg3);
                    break;
                case 4:
                    action = () => method.Invoke(null, args.Arg1, args.Arg2, args.Arg3, args.Arg4);
                    break;
                case 5:
                    action = () => method.Invoke(null, args.Arg1, args.Arg2, args.Arg3, args.Arg4, args.Arg5);
                    break;
            }

            //
            action.Should().Throw<InstanceNullException>();
        }

        [Theory]
        [InlineData(0, nameof(MethodsClass.ReturnNoArguments))]
        [InlineData(1, nameof(MethodsClass.Return1Arg))]
        [InlineData(2, nameof(MethodsClass.Return2Arg))]
        [InlineData(3, nameof(MethodsClass.Return3Arg))]
        [InlineData(4, nameof(MethodsClass.Return4Arg))]
        [InlineData(5, nameof(MethodsClass.Return5Arg))]
        public void Invoke_OnMethodWithResult_ShouldInvokeAndIgnoreResult(int argsCount, string name)
        {
            //
            var args = new Args();
            var action = static () => { };
            var expectedArgs = Array.Empty<object>();
            var method = _fastType.Method(name);

            //
            switch (argsCount)
            {
                case 0:
                    method.Invoke(_instance);
                    break;
                case 1:
                    method.Invoke(_instance, args.Arg1);
                    expectedArgs = new object[] { args.Arg1 };
                    break;
                case 2:
                    method.Invoke(_instance, args.Arg1, args.Arg2);
                    expectedArgs = new object[] { args.Arg1, args.Arg2, };
                    break;
                case 3:
                    method.Invoke(_instance, args.Arg1, args.Arg2, args.Arg3);
                    expectedArgs = new object[] { args.Arg1, args.Arg2, args.Arg3, };
                    break;
                case 4:
                    method.Invoke(_instance, args.Arg1, args.Arg2, args.Arg3, args.Arg4);
                    expectedArgs = new object[] { args.Arg1, args.Arg2, args.Arg3, args.Arg4, };
                    break;
                case 5:
                    method.Invoke(_instance, args.Arg1, args.Arg2, args.Arg3, args.Arg4, args.Arg5);
                    expectedArgs = new object[] { args.Arg1, args.Arg2, args.Arg3, args.Arg4, args.Arg5 };
                    break;
            }

            //
            var call = _instance.ReceivedCalls().FirstOrDefault();
            call.Should().NotBeNull($"No call was made to method '{name}'");

            call.GetMethodInfo().Name.Should().Be(name);
            call.GetArguments().Should().BeEquivalentTo(expectedArgs);
        }

        [Theory]
        //0 Args is valid
        [InlineData(1, nameof(MethodsClass.Return1Arg))]
        [InlineData(2, nameof(MethodsClass.Return2Arg))]
        [InlineData(3, nameof(MethodsClass.Return3Arg))]
        [InlineData(4, nameof(MethodsClass.Return4Arg))]
        [InlineData(5, nameof(MethodsClass.Return5Arg))]
        public void Invoke_OnInvalidSignature_ShouldThrow(int argsCount, string name)
        {
            //
            var args = new Args();
            var action = static () => { };
            var method = _fastType.Method(name);

            //
            switch (argsCount)
            {
                case 1:
                    action = () => method.Invoke(_instance, string.Empty);
                    break;
                case 2:
                    action = () => method.Invoke(_instance, args.Arg1, string.Empty);
                    break;
                case 3:
                    action = () => method.Invoke(_instance, args.Arg1, args.Arg2, string.Empty);
                    break;
                case 4:
                    action = () => method.Invoke(_instance, args.Arg1, args.Arg2, args.Arg3, string.Empty);
                    break;
                case 5:
                    action = () => method.Invoke(_instance, args.Arg1, args.Arg2, args.Arg3, args.Arg4, string.Empty);
                    break;
            }

            //
            action.Should().Throw<UnexpectedMethodSignatureException>();
        }
    }
}