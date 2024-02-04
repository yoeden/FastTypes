using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace FastTypes.Compiler
{
    /// <summary>
    /// Enum representing different block types.
    /// </summary>
    public enum BlockType
    {
        /// <summary>
        /// Represents a scope block.
        /// </summary>
        Scope,
        /// <summary>
        /// Represents a try block.
        /// </summary>
        Try,
        /// <summary>
        /// Represents a catch block.
        /// </summary>
        Catch,
        /// <summary>
        /// Represents a finally block.
        /// </summary>
        Finally,
    }

    internal class FluentIL
    {
        private readonly ILGenerator _il;

        private readonly Dictionary<Label, string> _labels;
        private readonly Dictionary<int, string> _locals;

        public FluentIL(ILGenerator il)
        {
            _il = il;
            _labels = new Dictionary<Label, string>();
            _locals = new Dictionary<int, string>();
        }

        protected int Offset => _il.ILOffset;

        protected virtual void Emitted<TArg>(OpCode op, TArg arg = default) { }
        protected void Emitted(OpCode op) => Emitted<object>(op, null);

        protected virtual void LabelMarked(ref Label label, string name) { }

        protected virtual void BeginBlock<T>(BlockType type, T arg = default) { }
        protected void BeginBlock(BlockType type) => BeginBlock<object>(type, null);
        protected virtual void EndBlock(BlockType type) { }

        //

        public FluentIL Delegate(Action<FluentIL> action)
        {
            action(this);
            return this;
        }

        //

        public LocalBuilder DeclareLocal(Type t, string name = "")
        {
            var local = _il.DeclareLocal(t);
            _locals.Add(local.LocalIndex, $"{local.LocalIndex}_{name}");
            return local;
        }

        public string ResolveLocalName(LocalBuilder local)
        {
            return _locals[local.LocalIndex];
        }

        //

        public Label DefineLabel(string name = "")
        {
            var label = _il.DefineLabel();
            _labels.Add(label, $"{_labels.Count}_{name}");
            return label;
        }

        public FluentIL MarkLabel(ref Label label)
        {
            _il.MarkLabel(label);
            LabelMarked(ref label, _labels[label]);

            return this;
        }

        //

        public FluentIL Nop(string comment = "")
        {
            //TODO: Check out if nop affecting perfomances, it doesnt matter since we can "fake" emit it.
            _il.Emit(OpCodes.Nop);
            Emitted(OpCodes.Nop, $"// {comment}");

            return this;
        }

        //
        // Methods
        //

        public FluentIL LoadArgument(int index)
        {
            switch (index)
            {
                case 0:
                    _il.Emit(OpCodes.Ldarg_0);
                    break;
                case 1:
                    _il.Emit(OpCodes.Ldarg_1);
                    break;
                case 2:
                    _il.Emit(OpCodes.Ldarg_2);
                    break;
                case 3:
                    _il.Emit(OpCodes.Ldarg_3);
                    break;
                default:
                    //TODO: Check if it is faster the use ldarg_s than ldarg
                    _il.Emit(OpCodes.Ldarg, index);
                    break;
            }

            Emitted(OpCodes.Ldarg, index);

            return this;
        }

        public FluentIL Return()
        {
            _il.Emit(OpCodes.Ret);
            Emitted(OpCodes.Ret);

            return this;
        }

        public FluentIL Call(MethodInfo method)
        {
            if (method.IsVirtual) return CallVirtual(method);

            _il.Emit(OpCodes.Call, method);
            Emitted(OpCodes.Call, $"{method.DeclaringType}::{method}");

            return this;
        }

        public FluentIL CallVirtual(MethodInfo method)
        {
            if (!method.IsVirtual) return Call(method);

            _il.Emit(OpCodes.Callvirt, method);
            Emitted(OpCodes.Callvirt, method.Name);

            return this;
        }

        //
        // Compares
        //

        public FluentIL CompareEquals()
        {
            _il.Emit(OpCodes.Ceq);
            Emitted(OpCodes.Ceq);

            return this;
        }

        //
        // Branches
        //

        public FluentIL BranchIfFalse(Label label)
        {
            _il.Emit(OpCodes.Brfalse, label);
            Emitted(OpCodes.Brfalse, _labels[label]);

            return this;
        }

        public FluentIL BranchIfTrue(Label label)
        {
            _il.Emit(OpCodes.Brtrue, label);
            Emitted(OpCodes.Brtrue, _labels[label]);

            return this;
        }

        public FluentIL BranchLowerThan(Label label)
        {
            _il.Emit(OpCodes.Blt, label);
            Emitted(OpCodes.Blt, _labels[label]);

            return this;
        }

        public FluentIL Branch(Label label)
        {
            _il.Emit(OpCodes.Br, label);
            Emitted(OpCodes.Br, _labels[label]);

            return this;
        }

        //
        // Array
        //

        public FluentIL LoadArrayLength()
        {
            _il.Emit(OpCodes.Ldlen);
            Emitted(OpCodes.Ldlen);

            return this;
        }

        public FluentIL NewArray(Type elementType)
        {
            _il.Emit(OpCodes.Newarr, elementType);
            Emitted(OpCodes.Newarr, elementType);

            return this;
        }

        public FluentIL LoadElement(Type elementType)
        {
            //TODO: Optimize with for example OpCodes.Ldelem_R4
            _il.Emit(OpCodes.Ldelem, elementType);
            Emitted(OpCodes.Ldelem);

            return this;
        }

        public FluentIL LoadElementReference(Type elementType)
        {
            _il.Emit(OpCodes.Ldelem_Ref);
            Emitted(OpCodes.Ldelem_Ref, elementType);

            return this;
        }

        public FluentIL StoreElement(Type elementType)
        {
            //TODO: optimize with for example OpCodes.Stelem_R4
            _il.Emit(OpCodes.Stelem, elementType);
            Emitted(OpCodes.Stelem);

            return this;
        }

        public FluentIL StoreElementReference()
        {
            _il.Emit(OpCodes.Stelem_Ref);
            Emitted(OpCodes.Stelem_Ref);

            return this;
        }

        //
        // Evaluation stack
        //

        public FluentIL Pop()
        {
            _il.Emit(OpCodes.Pop);
            Emitted(OpCodes.Pop);

            return this;
        }

        public FluentIL Duplicate()
        {
            _il.Emit(OpCodes.Dup);
            Emitted(OpCodes.Dup);

            return this;
        }


        //
        // Load Values
        //

        public FluentIL LoadNull()
        {
            _il.Emit(OpCodes.Ldnull);
            Emitted(OpCodes.Ldnull);

            return this;
        }

        public FluentIL LoadInt(int value)
        {
            //TODO: Optimize for ldc_i4_0
            _il.Emit(OpCodes.Ldc_I4_S, value);
            Emitted(OpCodes.Ldc_I4_S, value);

            return this;
        }

        public FluentIL LoadString(string str)
        {
            _il.Emit(OpCodes.Ldstr, str);
            Emitted(OpCodes.Ldstr, str);

            return this;
        }

        //
        // Convert
        //

        public FluentIL ConvertToInt()
        {
            _il.Emit(OpCodes.Conv_I4);
            Emitted(OpCodes.Conv_I4);
            return this;
        }

        public FluentIL ConvertToLong()
        {
            _il.Emit(OpCodes.Conv_I8);
            Emitted(OpCodes.Conv_I8);

            return this;
        }

        //
        // Locals
        //

        public FluentIL StoreLocal(LocalBuilder local)
        {
            _il.Emit(OpCodes.Stloc, local);
            Emitted(OpCodes.Stloc, local);

            return this;
        }

        public FluentIL LoadLocalAddress(LocalBuilder local)
        {
            _il.Emit(OpCodes.Ldloca, local);
            Emitted(OpCodes.Ldloca, local);

            return this;
        }

        public FluentIL LoadLocal(LocalBuilder local)
        {
            //TODO:
            //if (local.LocalIndex == 0) OpCodes.Ldloc_0
            _il.Emit(OpCodes.Ldloc, local);
            Emitted(OpCodes.Ldloc, local);

            return this;
        }

        //
        // Memory
        //

        public FluentIL InitObject(Type type)
        {
            _il.Emit(OpCodes.Initobj, type);
            Emitted(OpCodes.Initobj, type);

            return this;
        }

        public FluentIL New(ConstructorInfo info)
        {
            _il.Emit(OpCodes.Newobj, info);
            Emitted(OpCodes.Newobj, info);

            return this;
        }

        public FluentIL Box(Type t)
        {
            if (!t.IsValueType) throw new NotImplementedException("Cant box a non value type.");

            _il.Emit(OpCodes.Box, t);
            Emitted(OpCodes.Box, t);
            return this;
        }

        public FluentIL UnBoxAny(Type t)
        {
            if (!t.IsValueType) throw new NotImplementedException("Cant box a non value type.");

            _il.Emit(OpCodes.Unbox_Any, t);
            Emitted(OpCodes.Unbox_Any, t);
            return this;
        }

        //
        // Fields
        //

        public FluentIL LoadField(FieldInfo field)
        {
            _il.Emit(OpCodes.Ldfld, field);
            Emitted(OpCodes.Ldfld, field);

            return this;
        }

        public FluentIL StoreField(FieldInfo field)
        {
            _il.Emit(OpCodes.Stfld, field);
            Emitted(OpCodes.Stfld, field);

            return this;
        }

        //
        // Math
        //

        public FluentIL Add()
        {
            _il.Emit(OpCodes.Add);
            Emitted(OpCodes.Add);

            return this;
        }


        public FluentIL Mul()
        {
            _il.Emit(OpCodes.Mul);
            Emitted(OpCodes.Mul);

            return this;
        }

        //
        // Try
        //

        private FluentIL EndFinally()
        {
            _il.Emit(OpCodes.Endfinally);
            Emitted(OpCodes.Endfinally);

            return this;
        }

        public FluentIL Try(Action<FluentIL> block, Action<FluentIL, Type> @catch = null, Type exceptionType = null, Action<FluentIL> @finally = null)
        {
            using (var x = new FluentILTryCatchFinally(_il, this, block))
            {
                if (@catch != null)
                {
                    if (exceptionType == null) throw new ArgumentException("");
                    x.Catch(@catch, exceptionType);
                }
                if (@finally != null) x.Finally(@finally);
            }

            return this;
        }

        public sealed class FluentILTryCatchFinally : IDisposable
        {
            private readonly ILGenerator _il;
            private readonly FluentIL _fluentIL;

            public FluentILTryCatchFinally(ILGenerator il, FluentIL fluentIL, Action<FluentIL> tryBlock)
            {
                _il = il;
                _fluentIL = fluentIL;

                _il.BeginExceptionBlock();
                _fluentIL.BeginBlock(BlockType.Try);
                tryBlock(fluentIL);
                _fluentIL.EndBlock(BlockType.Try);
            }

            public FluentILTryCatchFinally Catch(Action<FluentIL, Type> block, Type exceptionType)
            {
                _il.BeginCatchBlock(exceptionType);
                _fluentIL.BeginBlock(BlockType.Catch, exceptionType);
                block(_fluentIL, exceptionType);
                _fluentIL.EndBlock(BlockType.Catch);
                return this;
            }

            public FluentILTryCatchFinally Finally(Action<FluentIL> block)
            {
                _il.BeginFinallyBlock();
                _fluentIL.BeginBlock(BlockType.Finally);
                block(_fluentIL);
                _fluentIL.EndFinally();
                _fluentIL.EndBlock(BlockType.Finally);
                return this;
            }

            public void Dispose()
            {
                _il.EndExceptionBlock();
            }
        }

        //
        // Utils
        //

        public FluentIL DebugPrintTopOfStack(Type t)
        {
            Duplicate();
            if (t.IsValueType)
            {
                Box(t);
            }
            else
            {

            }
            return Call(typeof(Debug).GetMethod("WriteLine", new Type[] { typeof(object) }));
        }

        public FluentIL DebugPrint(string output)
        {
            return
                LoadString(output)
                .Call(typeof(Debug).GetMethod("WriteLine", new Type[] { typeof(string) }));
        }
    }
}
