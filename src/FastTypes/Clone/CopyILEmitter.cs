using System;
using System.Diagnostics;
using System.Reflection.Emit;
using FastTypes.Compiler;

namespace FastTypes.Clone
{
    internal static class CopyILEmitter
    {
        public static void EmitCopy(
            ILGenerator il,
            Type t,
            bool isValueAlreadyLoaded = false)
        {
            var target = CopyTarget.FromType(t);
            var seenTypes = new SeenTypes();
            var fluentIL = new PrintableFluentIL(il);

            if (!isValueAlreadyLoaded) fluentIL.LoadArgument(0);
            IDeepCopyEvaluator evaluator = new DeepCopyEvaluator(fluentIL, target, seenTypes);
            evaluator.Evaluate(target.Type);
            fluentIL.Return();

            Debug.WriteLine(fluentIL.GetIL());
        }
    }
}