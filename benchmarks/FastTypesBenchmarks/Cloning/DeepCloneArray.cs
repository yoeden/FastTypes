using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FastTypes;
using FastTypes.Clone;

namespace FastTypesBenchmarks.Cloning
{
    public class DeepClone_Array_ValueType : FastTypesBaseBenchmarks
    {
        private readonly int[] ValueTypeArray = Enumerable.Repeat(0, 100_000).ToArray();

        [Benchmark(Baseline = true)]
        public int[] ArrayCopy()
        {
            var dst = new int[ValueTypeArray.Length];
            Array.Copy(ValueTypeArray, dst, dst.Length);
            return dst;
        }

        [Benchmark()]
        public int[] FastTypesClone()
        {
            return FastType.DeepCopy(ValueTypeArray);
        }
    }

    public class DeepClone_Array_Ref : FastTypesBaseBenchmarks
    {
        private readonly Item[] ReferenceArray = Enumerable.Repeat(0, 1000).Select(i => new Item() { Str = i.ToString() }).ToArray();

        [Benchmark(Baseline = true)]
        public Item[] ManualRefTests()
        {
            var dst = new Item[ReferenceArray.Length];
            for (var i = 0; i < dst.Length; i++)
            {
                dst[i] = new Item() { Str = ReferenceArray[i].Str };
            }
            return dst;
        }

        [Benchmark()]
        public Item[] FastTypesCloneRef()
        {
            return FastType.DeepCopy(ReferenceArray);
        }

        public class Item
        {
            public string Str { get; set; }
        }
    }
}
