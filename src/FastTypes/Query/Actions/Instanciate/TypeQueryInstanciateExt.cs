using System;
using System.Collections.Generic;
using FastTypes.Reflection;

namespace FastTypes.Query
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeQueryInstanciateExt
    {
        /// <summary>
        /// Instanciates objects based on the types returned by the query.
        /// </summary>
        /// <param name="query">The type query builder preparation.</param>
        /// <param name="customActivator">Custom activator function to create instances.</param>
        /// <returns>A read-only list of instanciated objects.</returns>
        public static IReadOnlyList<object> Instanciate(this ITypeQueryBuilderPreparation query, Func<FastActivator, object> customActivator = null)
        {
            var types = query.FindTypes();
            var instances = new List<object>(types.Count);
            for (var i = 0; i < types.Count; i++)
            {
                var fastType = FastType.Of(types[i]);
                try
                {
                    var activator = fastType.Activator();
                    var instance = customActivator != null ? customActivator(activator) : activator.NewObject();
                    instances.Add(instance);
                }
                catch (Exception ex)
                {
                    ThrowHelper.FailedToInstanciateType(types[i], ex);
                }
            }

            return instances;
        }

        /// <summary>
        /// Instanciates objects of type T based on the types returned by the query.
        /// </summary>
        /// <typeparam name="T">The type of objects to be instanciated.</typeparam>
        /// <param name="query">The type query builder preparation.</param>
        /// <param name="customActivator">Custom activator function to create instances.</param>
        /// <returns>A read-only list of instanciated objects of type T.</returns>
        public static IReadOnlyList<T> InstanciateAs<T>(this ITypeQueryBuilderPreparation query, Func<FastActivator, object> customActivator = null)
        {
            var types = query.FindTypes();
            var instances = new List<T>(types.Count);
            for (var i = 0; i < types.Count; i++)
            {
                var fastType = FastType.Of(types[i]);
                try
                {
                    var activator = fastType.Activator();

                    //TODO: In case of value types, this is boxed and causing memory allocation.
                    //Add to the activator base class a NewAs<T>() method to allow activation and type specifiying (will be beneficial for downcasting to interface as well)
                    var instance = customActivator != null ? customActivator(activator) : activator.NewObject();

                    instances.Add((T)instance);
                }
                catch (Exception ex)
                {
                    ThrowHelper.FailedToInstanciateType(types[i], ex);
                }
            }

            return instances;
        }
    }
}
