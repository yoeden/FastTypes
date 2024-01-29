using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using FastTypes.Compiler;

namespace FastTypes.Clone.AnotherTake
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