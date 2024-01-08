using FastTypes.Reflection;
using FastTypes.Tests.Reflection.Method;
using FluentAssertions;

namespace FastTypes.Tests.Reflection.Activator
{
    [Trait(Traits.Reflection.Tag, Traits.Reflection.Activator)]
    public class ActivatorTests
    {
        private readonly IFastType _fastType;
        private readonly IFastType<StubActivatorClass> _fastTypeGeneric;

        public ActivatorTests()
        {
            _fastType = FastType.Of<StubActivatorClass>();
            _fastTypeGeneric = FastType.Of<StubActivatorClass>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void NewObject_OnPublicClassWithPublicCtor_ShouldCreate(int count)
        {
            //
            var args = new Args();
            var given = (object)null;
            var expected = (object)null;

            //
            switch (count)
            {
                case 0:
                    given = _fastType.Activator().NewObject();
                    expected = new StubActivatorClass();
                    break;
                case 1:
                    given = _fastType.Activator().NewObject(args.Arg1);
                    expected = new StubActivatorClass(args.Arg1);
                    break;
                case 2:
                    given = _fastType.Activator().NewObject(args.Arg1, args.Arg2);
                    expected = new StubActivatorClass(args.Arg1, args.Arg2);
                    break;
                case 3:
                    given = _fastType.Activator().NewObject(args.Arg1, args.Arg2, args.Arg3);
                    expected = new StubActivatorClass(args.Arg1, args.Arg2, args.Arg3);
                    break;
                case 4:
                    given = _fastType.Activator().NewObject(args.Arg1, args.Arg2, args.Arg3, args.Arg4);
                    expected = new StubActivatorClass(args.Arg1, args.Arg2, args.Arg3, args.Arg4);
                    break;
                case 5:
                    given = _fastType.Activator().NewObject(args.Arg1, args.Arg2, args.Arg3, args.Arg4, args.Arg5);
                    expected = new StubActivatorClass(args.Arg1, args.Arg2, args.Arg3, args.Arg4, args.Arg5);
                    break;
            }

            //
            expected.Should().Be(given);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void New_OnPublicClassWithPublicCtor_ShouldCreate(int count)
        {
            //
            var args = new Args();
            var given = (StubActivatorClass)null;
            var expected = (StubActivatorClass)null;

            //
            switch (count)
            {
                case 0:
                    given = _fastTypeGeneric.Activator().New();
                    expected = new StubActivatorClass();
                    break;
                case 1:
                    given = _fastTypeGeneric.Activator().New(args.Arg1);
                    expected = new StubActivatorClass(args.Arg1);
                    break;
                case 2:
                    given = _fastTypeGeneric.Activator().New(args.Arg1, args.Arg2);
                    expected = new StubActivatorClass(args.Arg1, args.Arg2);
                    break;
                case 3:
                    given = _fastTypeGeneric.Activator().New(args.Arg1, args.Arg2, args.Arg3);
                    expected = new StubActivatorClass(args.Arg1, args.Arg2, args.Arg3);
                    break;
                case 4:
                    given = _fastTypeGeneric.Activator().New(args.Arg1, args.Arg2, args.Arg3, args.Arg4);
                    expected = new StubActivatorClass(args.Arg1, args.Arg2, args.Arg3, args.Arg4);
                    break;
                case 5:
                    given = _fastTypeGeneric.Activator().New(args.Arg1, args.Arg2, args.Arg3, args.Arg4, args.Arg5);
                    expected = new StubActivatorClass(args.Arg1, args.Arg2, args.Arg3, args.Arg4, args.Arg5);
                    break;
            }

            //
            expected.Should().Be(given);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void NewObject_OnClassWithoutMatchingCtor_ShouldThrow(int count)
        {
            //
            var args = new Args();
            var action = static () => { };

            //
            switch (count)
            {
                case 0:
                    action = () => _fastType.Activator().NewObject();
                    break;
                case 1:
                    action = () => _fastType.Activator().NewObject(string.Empty);
                    break;
                case 2:
                    action = () => _fastType.Activator().NewObject(args.Arg1, string.Empty);
                    break;
                case 3:
                    action = () => _fastType.Activator().NewObject(args.Arg1, args.Arg2, string.Empty);
                    break;
                case 4:
                    action = () => _fastType.Activator().NewObject(args.Arg1, args.Arg2, args.Arg3, string.Empty);
                    break;
                case 5:
                    action = () => _fastType.Activator().NewObject(args.Arg1, args.Arg2, args.Arg3, args.Arg4, string.Empty);
                    break;
            }

            //
            action.Should().Throw<NoSuitableConstructorFound>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void New_OnClassWithoutMatchingCtor_ShouldThrow(int count)
        {
            //
            var args = new Args();
            var action = static () => { };

            //
            switch (count)
            {
                case 0:
                    action = () => _fastTypeGeneric.Activator().New();
                    break;
                case 1:
                    action = () => _fastTypeGeneric.Activator().New(string.Empty);
                    break;
                case 2:
                    action = () => _fastTypeGeneric.Activator().New(args.Arg1, string.Empty);
                    break;
                case 3:
                    action = () => _fastTypeGeneric.Activator().New(args.Arg1, args.Arg2, string.Empty);
                    break;
                case 4:
                    action = () => _fastTypeGeneric.Activator().New(args.Arg1, args.Arg2, args.Arg3, string.Empty);
                    break;
                case 5:
                    action = () => _fastTypeGeneric.Activator().New(args.Arg1, args.Arg2, args.Arg3, args.Arg4, string.Empty);
                    break;
            }

            //
            action.Should().Throw<NoSuitableConstructorFound>();
        }
    }
}
