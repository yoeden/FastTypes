using FastTypes.Clone;

namespace FastTypes.Tests.Copy.New
{
    public abstract class BaseFastCopyTests
    {
        protected T InvokeDeepCopy<T>(T src, bool useGeneric = true)
        {
            return useGeneric ? FastCopy.DeepCopy(src) : (T)FastCopy.DeepCopy((object)src);
        }

        protected class ClassWithPrimitiveStub
        {
            //TODO: Replace all of this with Create method
            public ClassWithPrimitiveStub(int v) => Value = v;

            private ClassWithPrimitiveStub() { }

            public int Value { get; set; }
        }

        protected class ClassWithInternalClassStub
        {
            public static ClassWithInternalClassStub Create(ClassWithPrimitiveStub @internal)
            {
                return new ClassWithInternalClassStub
                {
                    Internal = @internal
                };
            }

            public ClassWithPrimitiveStub Internal { get; set; }
        }

        protected class ClassWithPureValueTypeStub
        {
            public ClassWithPureValueTypeStub(int v) => Value = new PureValueTypeStub(v);

            private ClassWithPureValueTypeStub() { }

            public PureValueTypeStub Value { get; set; }
        }

        protected class ClassWithComplexValueTypeStub
        {
            public ClassWithComplexValueTypeStub(int v) => ComplexValueType = new ComplexValueTypeStub(v);

            private ClassWithComplexValueTypeStub() { }

            public ComplexValueTypeStub ComplexValueType { get; set; }
        }

        protected struct PureValueTypeStub
        {
            public PureValueTypeStub(int v)
            {
                Value = v;
            }

            public int Value { get; set; }
        }

        protected struct ComplexValueTypeStub
        {
            public ComplexValueTypeStub(int v)
            {
                RefType = new ClassWithPrimitiveStub(v);
            }

            public ClassWithPrimitiveStub RefType { get; set; }
        }
    }
}