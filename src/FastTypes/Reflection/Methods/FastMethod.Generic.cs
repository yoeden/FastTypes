using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FastTypes.Reflection
{
    /// <summary>
    /// This class provides support for invoking methods a given Type.
    /// </summary>
    public sealed partial class FastMethod<TType> : FastMethod
    {
        private readonly Delegate _del;
        private readonly Delegate _delReturnsObject;

        private FastMethod(Delegate del, Delegate delReturnsObject, MethodInfo info)
        {
            _del = del;
            _delReturnsObject = delReturnsObject;

            Name = info.Name;
            IsStatic = info.IsStatic;
            ReturnType = info.ReturnType;
        }

        /// <inheritdoc />
        public override string Name { get; }

        /// <inheritdoc />
        public override bool IsStatic { get; }

        /// <inheritdoc />
        public override Type ReturnType { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowIfDelegateIsNot(Delegate @delegate, Type expected)
        {
            if (@delegate.GetType() != expected) ThrowHelper.UnexpectedMethodSignature(@delegate.GetType(), expected);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfMethodDoesntSupportObjectReturn()
        {
            if (_delReturnsObject == null) ThrowHelper.MethodExpectedVoid(Name, ReturnType);
        }
    }
}
