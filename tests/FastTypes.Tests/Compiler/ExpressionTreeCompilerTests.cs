using FluentAssertions;
using System.Reflection;
using FastTypes.Compiler;

namespace FastTypes.Tests.Compiler
{
    [Trait(Traits.Compiler.Tag,Traits.Compiler.ExpressionTree)]
    public class ExpressionTreeCompilerTests
    {
        private readonly ExpressionTreeCompiler _compiler;

        public ExpressionTreeCompilerTests()
        {
            _compiler = new ExpressionTreeCompiler();
        }

        [Fact]
        public void Setter_OnProperty_ShouldReturnCompiledSetterDelegate()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty("Value");

            // Act
            Action<TestClass, int> setter = (Action<TestClass, int>)_compiler.Setter(property);

            // Assert
            var instance = new TestClass();
            setter.Should().NotBeNull();
            setter(instance, 42);
            instance.Value.Should().Be(42);
        }

        [Fact]
        public void SetterWithObjectParameter_OnProperty_ShouldReturnCompiledSetterDelegateWithObjectParameter()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty("Value");

            // Act
            Action<TestClass, object> setter = (Action<TestClass, object>)_compiler.SetterWithObjectParameter(property);

            // Assert
            var instance = new TestClass();
            setter.Should().NotBeNull();
            setter(instance, 42);
            instance.Value.Should().Be(42);
        }

        [Fact]
        public void Getter_OnProperty_ShouldReturnCompiledGetterDelegate()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty("Value");
            var instance = new TestClass { Value = 42 };

            // Act
            Func<TestClass, int> getter = (Func<TestClass, int>)_compiler.Getter(property);

            // Assert
            getter.Should().NotBeNull();
            getter(instance).Should().Be(42);
        }

        [Fact]
        public void GetterWithObjectReturn_OnProperty_ShouldReturnCompiledGetterDelegateWithObjectReturn()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty("Value");
            var instance = new TestClass { Value = 42 };

            // Act
            Func<TestClass, object> getter = (Func<TestClass, object>)_compiler.GetterWithObjectReturn(property);

            // Assert
            getter.Should().NotBeNull();
            getter(instance).Should().Be(42);
        }

        [Fact]
        public void Method_OnMethodInfo_ShouldReturnCompiledMethodDelegate()
        {
            // Arrange
            MethodInfo method = typeof(TestClass).GetMethod("Add");

            // Act
            Func<TestClass, int, int, int> methodDelegate = (Func<TestClass, int, int, int>)_compiler.Method(method);

            // Assert
            methodDelegate.Should().NotBeNull();
            var instance = new TestClass();
            methodDelegate(instance, 2, 3).Should().Be(5);
        }

        [Fact]
        public void MethodReturnObject_OnMethodInfo_ShouldReturnCompiledMethodDelegateWithObjectReturn()
        {
            // Arrange
            MethodInfo method = typeof(TestClass).GetMethod("Add");

            // Act
            Func<TestClass, int, int, object> methodDelegate =
                (Func<TestClass, int, int, object>)_compiler.MethodReturnObject(method);

            // Assert
            methodDelegate.Should().NotBeNull();
            var instance = new TestClass();
            methodDelegate(instance, 2, 3).Should().Be(5);
        }

        [Fact]
        public void Activator_OnConstructorInfo_ShouldReturnCompiledActivatorDelegate()
        {
            // Arrange
            ConstructorInfo constructor = typeof(TestClass).GetConstructor(new[] { typeof(int) });

            // Act
            Func<int, TestClass> activator = (Func<int, TestClass>)_compiler.Activator(constructor);

            // Assert
            activator.Should().NotBeNull();
            var instance = activator(42);
            instance.Should().NotBeNull();
            instance.Value.Should().Be(42);
        }

        private class TestClass
        {
            public int Value { get; set; }

            public TestClass()
            {
            }

            public TestClass(int value)
            {
                Value = value;
            }

            public int Add(int a, int b)
            {
                return a + b;
            }
        }
    }
}
