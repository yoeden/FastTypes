namespace FastTypes.Tests.Reflection.Method
{
    public class MethodsClass
    {
        public virtual void NoReturnNoArguments() { }
        public virtual void NoReturn1Arg(int a) { }
        public virtual void NoReturn2Arg(int a1, int a2) { }
        public virtual void NoReturn3Arg(int a1, int a2, int a3) { }
        public virtual void NoReturn4Arg(int a1, int a2, int a3, int a4) { }
        public virtual void NoReturn5Arg(int a1, int a2, int a3, int a4, int a5) { }

        public virtual int ReturnNoArguments() => ExpectedValues.Int;
        public virtual int Return1Arg(int a1) => a1;
        public virtual int Return2Arg(int a1, int a2) => a1;
        public virtual int Return3Arg(int a1, int a2, int a3) => a1;
        public virtual int Return4Arg(int a1, int a2, int a3, int a4) => a1;
        public virtual int Return5Arg(int a1, int a2, int a3, int a4, int a5) => a1;
    }
}