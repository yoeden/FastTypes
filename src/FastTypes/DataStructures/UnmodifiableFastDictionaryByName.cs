using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FastTypes.DataStructures
{
    internal sealed class UnmodifiableFastDictionaryByName<TValue>
    {
        private static readonly UnmodifiableFastDictionaryByName<TValue> Empty =
            new(ArraySegment<TValue>.Empty, static s => -1);

        public static UnmodifiableFastDictionaryByName<TValue> Create(IReadOnlyList<KeyValuePair<string, TValue>> values)
        {
            //TODO: Add this to the compile interface
            if (values.Count == 0) return Empty;

            DynamicMethod method = new("", typeof(int), new[] { typeof(string) });

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
                ilGenerator.Emit(OpCodes.Ldstr, values[i].Key);
                ilGenerator.Emit(OpCodes.Call, typeof(string).GetMethod("op_Equality", new[] { typeof(string), typeof(string) }));
                ilGenerator.Emit(OpCodes.Brfalse, jumpTable[i + 1]);
                ilGenerator.Emit(OpCodes.Ldc_I4, i);
                ilGenerator.Emit(OpCodes.Ret);
            }

            ilGenerator.MarkLabel(jumpTable[^1]);
            ilGenerator.Emit(OpCodes.Ldc_I4_M1);
            ilGenerator.Emit(OpCodes.Ret);

            Func<string, int> indexResolver = (Func<string, int>)method.CreateDelegate(typeof(Func<string, int>));
            return new UnmodifiableFastDictionaryByName<TValue>(valuesArray, indexResolver);
        }

        private readonly IReadOnlyList<TValue> _values;
        private readonly Func<string, int> _resolver;

        public UnmodifiableFastDictionaryByName(
            IReadOnlyList<TValue> values,
            Func<string, int> resolver)
        {
            _values = values;
            _resolver = resolver;
        }


        public IReadOnlyList<TValue> Values => _values;

        public TValue this[string id]
        {
            get
            {
                var index = _resolver(id);
                if (index == -1) throw new KeyNotFoundException("TODO");
                return _values[index];
            }
        }

        public bool ContainsKey(string key) => _resolver(key) != -1;
    }

}