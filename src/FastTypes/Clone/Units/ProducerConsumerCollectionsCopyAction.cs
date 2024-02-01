using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastTypes.Compiler;

namespace FastTypes.Clone
{
    internal sealed class ProducerConsumerCollectionsCopyAction : DeepCopyAction
    {
        public ProducerConsumerCollectionsCopyAction(IDeepCopyEvaluator evaluator) : base(evaluator)
        {
        }

        public override bool Copy(FluentIL il, Type type)
        {
            if (
                !type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IProducerConsumerCollection<>)) &&
                !(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(BlockingCollection<>)) &&
                !(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ConcurrentDictionary<,>))
            )
                return false;

            var target = CopyTarget.FromType(type);
            var collectionInterface = type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>));
            var elementType = collectionInterface.GetGenericArguments()[0];

            var ctor = GetBestFittingConstructor(target, elementType);
            if (ctor == null) ThrowHelper.ProducerConsumerConstructor(type);

            //Source is on top of stack
            il.New(ctor);

            return true;
        }

        private static ConstructorInfo GetBestFittingConstructor(CopyTarget target, Type elementType)
        {
            if (target.Type.IsGenericType && target.Type.GetGenericTypeDefinition() == typeof(BlockingCollection<>))
            {
                var concurrentInterfaceType = typeof(IProducerConsumerCollection<>).MakeGenericType(elementType);
                return target.Constructors.FirstOrDefault(c => c.IsMatching(concurrentInterfaceType))?.Constructor;
            }
            else
            {
                var enumerableType = typeof(IEnumerable<>).MakeGenericType(elementType);
                return target.Constructors.FirstOrDefault(c => c.IsMatching(enumerableType))?.Constructor;
            }
        }
    }
}