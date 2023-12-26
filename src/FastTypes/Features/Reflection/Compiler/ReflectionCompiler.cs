using FastTypes.Features.Reflection.Compiler.ExpressionTree;
using FastTypes.Features.Reflection.Compiler.IL;

namespace FastTypes.Features.Reflection.Compiler
{
    internal static class ReflectionCompiler
    {
        public static readonly IReflectionCompiler Compiler = new ILCompiler();
    }
}