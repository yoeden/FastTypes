using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace FastTypes.Clone
{
    internal class FluentIL
    {
        const int alignment = -10;

#if DEBUG
        private readonly StringBuilder sb = new StringBuilder();
#endif

        private readonly ILGenerator _il;

        public FluentIL(ILGenerator il)
        {
            _il = il;
        }

        public FluentIL LoadArgument(int index)
        {
            _il.Emit(OpCodes.Ldarg, index);
            AppendEmit($"{"Ldarg",alignment} {index}");

            return this;
        }

        public FluentIL AddComment(string comment)
        {
            AppendEmit($"// {comment}");
            return this;
        }

        public FluentIL Nop()
        {
            _il.Emit(OpCodes.Nop);
            AppendEmit($"{"Nop",alignment}");

            return this;
        }


        private void AppendEmit(string s)
        {
#if DEBUG
            sb.AppendLine(s);
#endif
        }

        public string PrintIL()
        {
#if DEBUG
            return sb.ToString();
#endif
            return "-";
        }
    }
}
