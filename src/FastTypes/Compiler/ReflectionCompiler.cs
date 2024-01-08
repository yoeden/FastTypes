namespace FastTypes.Compiler
{
    internal static class ReflectionCompiler
    {
        public static readonly IReflectionCompiler Compiler = new ILCompiler();
    }
}