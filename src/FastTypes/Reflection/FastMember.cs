using System.Runtime.CompilerServices;

namespace FastTypes.Reflection
{
    public abstract class FastMember
    {
        public abstract string Name { get; }
        public abstract bool IsStatic { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //Use TType to avoid boxing
        protected void ThrowIfInstanceIsNull<TType>(TType instance)
        {
            if (!IsStatic && instance == null) ThrowHelper.InstanceCantBeNullOfNonStaticMembers(Name);
        }
    }
}