using System;
using System.Reflection.Emit;
using System.Text;

namespace FastTypes.Compiler
{
    internal class PrintableFluentIL : FluentIL
    {
        private const int InstructionStringAlignment = 14;
        private const int AddressStringAlignment = 8;
        private readonly StringBuilder _sb = new();

        private int _offset;
        private int _identLevel = 0;

        public PrintableFluentIL(ILGenerator il) : base(il)
        {
        }

        protected override void Emitted<TArg>(OpCode op, TArg arg)
        {
            if (arg == null)
            {
                AppendEmit(op);
            }
            else if (arg is LocalBuilder local)
            {
                if (local.LocalType.IsGenericType)
                    AppendEmit(op, $"{local.LocalType.Name}<{local.LocalType.GetGenericArguments()[0].Name}> ({local.LocalIndex})");
                else
                    AppendEmit(op, $"{local.LocalType.Name} ({local.LocalIndex})");
            }
            else
            {
                AppendEmit(op, arg.ToString());
            }
        }

        protected override void LabelMarked(ref Label label, string name)
        {
            AppendLine(string.Empty);
            AppendLine($"{name}: ");
        }

        protected override void BeginBlock<T>(BlockType type, T arg)
        {
            switch (type)
            {
                case BlockType.Scope:
                    AppendLine("{");
                    break;
                case BlockType.Try:
                    AppendLine(".try {");
                    break;
                case BlockType.Catch:
                    AppendLine($".catch ({(arg as Type).Name}) {{");
                    break;
                case BlockType.Finally:
                    AppendLine(".finally {");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            _identLevel++;
        }

        protected override void EndBlock(BlockType type)
        {
            _identLevel--;
            AppendLine("}");
        }

        private void AppendEmit(OpCode op, string args = null)
        {
            AppendLine($"IL_{_offset,-AddressStringAlignment:X}{op.Name,-InstructionStringAlignment} {args}");
            _offset = Offset;
        }

        private void AppendLine(string s)
        {
            _sb.AppendLine($"{new string(' ', 2 * _identLevel)}{s}");
        }

        public string GetIL()
        {
            return _sb.ToString();
        }
    }
}