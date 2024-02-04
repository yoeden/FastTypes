using FastTypes.Reflection;
using FluentAssertions;
using NSubstitute;

namespace FastTypes.Tests.Reflection.Method
{
    [Trait(Traits.Reflection.Tag, Traits.Reflection.Method)]
    public class MethodTests_ReturnsT_OnObject
    {
        //DONT CHANGE, it should be invoked on object overloads
        protected readonly IFastType _fastType;
        protected readonly MethodsClass _instance;

        public MethodTests_ReturnsT_OnObject()
        {
            _fastType = FastType.Of<MethodsClass>();
            _instance = Substitute.For<MethodsClass>();

            _instance.ReturnNoArg().Returns(ExpectedValues.Int);
            _instance.Return1Arg(Arg.Any<int>()).Returns(ExpectedValues.Int);
            _instance.Return2Arg(Arg.Any<int>(), Arg.Any<int>()).Returns(ExpectedValues.Int);
            _instance.Return3Arg(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()).Returns(ExpectedValues.Int);
            _instance.Return4Arg(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()).Returns(ExpectedValues.Int);
            _instance.Return5Arg(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()).Returns(ExpectedValues.Int);
        }

        [Theory]
        [InlineData(0, nameof(MethodsClass.ReturnNoArg))]
        [InlineData(1, nameof(MethodsClass.Return1Arg))]
        [InlineData(2, nameof(MethodsClass.Return2Arg))]
        [InlineData(3, nameof(MethodsClass.Return3Arg))]
        [InlineData(4, nameof(MethodsClass.Return4Arg))]
        [InlineData(5, nameof(MethodsClass.Return5Arg))]
        public void Invoke_OnValidParameters_ShouldReturn(int argsCount, string name)
        {
            //
            var args = new Args();
            var expectedArgs = Array.Empty<object>();
            var givenResult = (object)null;
            var method = _fastType.Method(name);

            //
            switch (argsCount)
            {
                case 0:
                    givenResult = method.Invoke(_instance);
                    expectedArgs = new object[] { };
                    break;
                case 1:
                    givenResult = method.Invoke<int>(_instance, args.Arg1);
                    expectedArgs = new object[] { args.Arg1 };
                    break;
                case 2:
                    givenResult = method.Invoke<int, int>(_instance, args.Arg1, args.Arg2);
                    expectedArgs = new object[] { args.Arg1, args.Arg2 };
                    break;
                case 3:
                    givenResult = method.Invoke<int, int, int>(_instance, args.Arg1, args.Arg2, args.Arg3);
                    expectedArgs = new object[] { args.Arg1, args.Arg2, args.Arg3 };
                    break;
                case 4:
                    givenResult = method.Invoke<int, int, int, int>(_instance, args.Arg1, args.Arg2, args.Arg3, args.Arg4);
                    expectedArgs = new object[] { args.Arg1, args.Arg2, args.Arg3, args.Arg4 };
                    break;
                case 5:
                    givenResult = method.Invoke<int, int, int, int, int>(_instance, args.Arg1, args.Arg2, args.Arg3, args.Arg4, args.Arg5);
                    expectedArgs = new object[] { args.Arg1, args.Arg2, args.Arg3, args.Arg4, args.Arg5 };
                    break;
            }

            //
            var call = _instance.ReceivedCalls().FirstOrDefault();
            call.Should().NotBeNull($"No call was made to method '{name}'");

            //
            call.GetMethodInfo().Name.Should().Be(name);
            call.GetArguments().Should().BeEquivalentTo(expectedArgs);

            //
            givenResult.Should().Be(ExpectedValues.Int);
        }

        [Theory]
        [InlineData(0, nameof(MethodsClass.ReturnNoArg))]
        [InlineData(1, nameof(MethodsClass.Return1Arg))]
        [InlineData(2, nameof(MethodsClass.Return2Arg))]
        [InlineData(3, nameof(MethodsClass.Return3Arg))]
        [InlineData(4, nameof(MethodsClass.Return4Arg))]
        [InlineData(5, nameof(MethodsClass.Return5Arg))]
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
                    action = () => method.Invoke<int>(null, args.Arg1);
                    break;
                case 2:
                    action = () => method.Invoke<int, int>(null, args.Arg1, args.Arg2);
                    break;
                case 3:
                    action = () => method.Invoke<int, int, int>(null, args.Arg1, args.Arg2, args.Arg3);
                    break;
                case 4:
                    action = () => method.Invoke<int, int, int, int>(null, args.Arg1, args.Arg2, args.Arg3, args.Arg4);
                    break;
                case 5:
                    action = () => method.Invoke<int, int, int, int, int>(null, args.Arg1, args.Arg2, args.Arg3, args.Arg4, args.Arg5);
                    break;
            }

            //
            action.Should().Throw<InstanceNullException>();
        }

        [Theory]
        [InlineData(0, nameof(MethodsClass.ReturnNoArg))]
        [InlineData(1, nameof(MethodsClass.Return1Arg))]
        [InlineData(2, nameof(MethodsClass.Return2Arg))]
        [InlineData(3, nameof(MethodsClass.Return3Arg))]
        [InlineData(4, nameof(MethodsClass.Return4Arg))]
        [InlineData(5, nameof(MethodsClass.Return5Arg))]
        public void Invoke_OnInvalidParameters_ShouldThrow(int argsCount, string name)
        {
            //
            var args = new Args();
            var action = static () => { };
            var method = _fastType.Method<int>(name);

            //
            switch (argsCount)
            {
                case 0:
                    action = () => method.Invoke(_instance, string.Empty);
                    break;
                case 1:
                    action = () => method.Invoke(_instance, string.Empty);
                    break;
                case 2:
                    action = () => method.Invoke(_instance, string.Empty, string.Empty);
                    break;
                case 3:
                    action = () => method.Invoke(_instance, string.Empty, string.Empty, string.Empty);
                    break;
                case 4:
                    action = () => method.Invoke(_instance, string.Empty, string.Empty, string.Empty, string.Empty);
                    break;
                case 5:
                    action = () => method.Invoke(_instance, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    break;
            }

            //
            action.Should().Throw<UnexpectedMethodParametersException>();
        }
    }
}