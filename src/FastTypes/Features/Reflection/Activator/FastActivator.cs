namespace FastTypes.Features.Reflection.Activator
{
    public abstract class FastActivator
    {
        /// <summary>
        /// Invokes the public empty constructor.
        /// </summary>
        /// <returns>New instance of current Type as object</returns>
        public abstract object NewObject();

        /// <summary>
        /// Invokes a public matching constructor.
        /// </summary>
        /// <returns>New instance of current Type as object</returns>
        public abstract object NewObject<T>(T arg1);

        /// <summary>
        /// Invokes a public matching constructor.
        /// </summary>
        /// <returns>New instance of current Type as object</returns>
        public abstract object NewObject<T1, T2>(T1 arg1, T2 arg2);

        /// <summary>
        /// Invokes a public matching constructor.
        /// </summary>
        /// <returns>New instance of current Type as object</returns>
        public abstract object NewObject<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);

        /// <summary>
        /// Invokes a public matching constructor.
        /// </summary>
        /// <returns>New instance of current Type as object</returns>
        public abstract object NewObject<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);


        /// <summary>
        /// Invokes a public matching constructor.
        /// </summary>
        /// <returns>New instance of current Type as object</returns>
        public abstract object NewObject<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}