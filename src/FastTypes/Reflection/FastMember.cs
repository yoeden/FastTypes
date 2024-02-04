using System.Runtime.CompilerServices;

namespace FastTypes.Reflection
{
    /// <summary>
    /// Base class representing every reflectable member (Method, Property and Field)
    /// </summary>
    public abstract class FastMember
    {
        /// <summary>
        /// The member name
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Flag to indicate if the member is static
        /// </summary>
        public abstract bool IsStatic { get; }

        /// <summary>
        /// Throws if the given instance is null (valid only for non value types).
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="instance"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ThrowIfInstanceIsNull<TType>(TType instance)
        {
            //TODO: ref TType to avoid cloning value types
            //Use TType to avoid boxing
            if (!IsStatic && instance == null) ThrowHelper.InstanceCantBeNullOfNonStaticMembers(Name);
        }
    }
}