using System;
using System.Reflection;

namespace FastTypes.Clone
{
    internal sealed class CopyTargetCtor : CopyTargetMember
    {
        private readonly ParameterInfo[] _parameters;

        public CopyTargetCtor(ConstructorInfo constructor) : base(constructor.DeclaringType)
        {
            Constructor = constructor;
            _parameters = constructor.GetParameters();
        }

        public ConstructorInfo Constructor { get; }
        public override bool CanRead => true;
        public override bool CanWrite => true;
        public bool IsDefault => _parameters.Length == 0;

        public bool IsMatching(Type t1)
        {
            if (_parameters.Length != 1) return false;
            if (_parameters[0].ParameterType != t1) return false;

            return true;
        }

        public bool IsMatching<T1>() => IsMatching(typeof(T1));

        public bool IsMatching<T1, T2>()
        {
            if (_parameters.Length != 1) return false;
            if (_parameters[0].ParameterType != typeof(T1)) return false;
            if (_parameters[1].ParameterType != typeof(T2)) return false;

            return true;
        }
    }
}