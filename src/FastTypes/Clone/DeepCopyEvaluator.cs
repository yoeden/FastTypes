using System;
using FastTypes.Clone.Metadata;
using FastTypes.Clone.Units;
using FastTypes.Compiler;

namespace FastTypes.Clone.AnotherTake
{
    internal sealed class DeepCopyEvaluator : IDeepCopyEvaluator
    {
        private readonly FluentIL _il;
        private readonly CopyTarget _root;
        private readonly SeenTypes _seenTypes;

        public DeepCopyEvaluator(FluentIL il, CopyTarget root, SeenTypes seenTypes)
        {
            _il = il;
            _root = root;
            _seenTypes = seenTypes;
        }

        public FluentIL Evaluate(Type type)
        {
            if (type.IsPureType())
            {
                // We dont mark pure type as seen
                // Nothing to do since setting the given value will copy it by the runtime
                return _il;
            }
            else if (_seenTypes.HasBeenSeen(type))
            {
                var m = typeof(FastCopy<>).MakeGenericType(type).GetMethod(nameof(FastCopy<CopyTarget>.DeepCopy));
                _il.Call(m);

                return _il;
            }
            else
            {

                return _il.CheckNull(
                    type,
                    _ => _il.LoadNull(),
                    _ => InternalEvaluate(type),
                    debugIgnore: false
                );
            }
        }

        private void InternalEvaluate(Type type)
        {
            var defaultAction = new CopyObjectByFields(this);
            var actions = new DeepCopyAction[]
            {
                //Order matters
                new ProducerConsumerCollectionsCopyAction(this),
                new ArrayCopyAction(this),
                new GenericCollectionInterfaceCopyAction(this),
            };

            var handled = false;
            for (var i = 0; i < actions.Length; i++)
            {
                var action = actions[i];
                if (action.Copy(_il, type))
                {
                    handled = true;
                    break;
                }
            }

            if (!handled)
            {
                _seenTypes.MarkAsSeen(type);
                defaultAction.Copy(_il, type);
            }
        }
    }
}