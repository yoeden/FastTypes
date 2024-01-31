using System;
using FastTypes.Compiler;

namespace FastTypes.Clone.AnotherTake
{
    internal interface IDeepCopyEvaluator
    {
        /// <summary>
        /// This method expects the copyable value to be on top of the evaluation stack, and will set the new value on top instead.
        /// </summary>
        /// <param name="type"></param>
        FluentIL Evaluate(Type type);
    }
}