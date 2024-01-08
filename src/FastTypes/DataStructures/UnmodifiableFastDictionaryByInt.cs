using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FastTypes.DataStructures
{
    internal sealed class UnmodifiableFastDictionaryByInt<TValue>
    {
        private static readonly UnmodifiableFastDictionaryByInt<TValue> Empty =
            new(ArraySegment<TValue>.Empty, static s => -1);

        public static UnmodifiableFastDictionaryByInt<TValue> Create(IReadOnlyList<KeyValuePair<int, TValue>> values)
        {
            if (values.Count == 0) return Empty;

            DynamicMethod method = new("", typeof(int), new[] { typeof(int) });

            var valuesArray = new TValue[values.Count];
            for (int i = 0; i < valuesArray.Length; i++)
            {
                valuesArray[i] = values[i].Value;
            }

            ILGenerator ilGenerator = method.GetILGenerator();
            var jumpTable = new Label[values.Count + 1];
            for (var i = 0; i < jumpTable.Length; i++)
            {
                jumpTable[i] = ilGenerator.DefineLabel();
            }

            for (int i = 0; i < values.Count; i++)
            {
                ilGenerator.MarkLabel(jumpTable[i]);
                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldc_I4, values[i].Key);
                ilGenerator.Emit(OpCodes.Bne_Un, jumpTable[i + 1]);
                ilGenerator.Emit(OpCodes.Ldc_I4, i);
                ilGenerator.Emit(OpCodes.Ret);
            }

            ilGenerator.MarkLabel(jumpTable[^1]);
            ilGenerator.Emit(OpCodes.Ldc_I4_M1);
            ilGenerator.Emit(OpCodes.Ret);

            Func<int, int> indexResolver = (Func<int, int>)method.CreateDelegate(typeof(Func<int, int>));
            return new UnmodifiableFastDictionaryByInt<TValue>(valuesArray, indexResolver);
        }

        private readonly IReadOnlyList<TValue> _values;
        private readonly Func<int, int> _resolver;

        public UnmodifiableFastDictionaryByInt(
            IReadOnlyList<TValue> values,
            Func<int, int> resolver)
        {
            _values = values;
            _resolver = resolver;
        }

        public TValue this[int id]
        {
            get
            {
                var index = _resolver(id);
                if (index == -1) throw new KeyNotFoundException("TODO");
                return _values[index];
            }
        }

        public bool ContainsKey(int key) => _resolver(key) != -1;
    }
}