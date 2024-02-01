using System;
using FastTypes.Compiler;

namespace FastTypes.Clone
{
    internal abstract class DeepCopyAction
    {
        private readonly IDeepCopyEvaluator _evaluator;

        protected DeepCopyAction(IDeepCopyEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        protected FluentIL Evaluate(Type type) => _evaluator.Evaluate(type);

        /// <summary>
        /// This method expects the copyable value to be on top of the evaluation stack, and will set the new value on top instead.
        /// </summary>
        /// <param name="il"></param>
        /// <param name="type"></param>
        /// <returns>true if the value was copied;false otherwise</returns>
        public abstract bool Copy(FluentIL il, Type type);
    }
}