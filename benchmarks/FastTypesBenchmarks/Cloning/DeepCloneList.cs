using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastTypes;
using FastTypes.Clone;

namespace FastTypesBenchmarks.Cloning
{
    public class DeepClone_List_ValueType : BaseBenchmarks
    {
        private readonly List<int> ValueTypeArray = Enumerable.Range(0, 100_000).ToList();

        [Benchmark(Baseline = true)]
        public List<int> ArrayCopy()
        {
            return new List<int>(ValueTypeArray);
        }

        [Benchmark()]
        public List<int> FastTypesClone()
        {
            return FastType.DeepCopy(ValueTypeArray);
        }
    }

    public class DeepClone_List_Ref : BaseBenchmarks
    {
        private readonly List<Item> ReferenceArray;

        public DeepClone_List_Ref()
        {
            ReferenceArray = new List<Item>(1000 * 10);
            ReferenceArray.AddRange(Enumerable.Range(0, 1000).Select(i => new Item() { Str = i.ToString() }));
        }

        [Benchmark(Baseline = true)]
        public List<Item> AddEachItemTests()
        {
            var dst = new List<Item>(ReferenceArray.Count);
            for (var i = 0; i < ReferenceArray.Count; i++)
            {
                dst.Add(new Item() { Str = ReferenceArray[i].Str });
            }

            return dst;
        }

        [Benchmark()]
        public List<Item> AddEachItemWithoutInitialCapacityTests()
        {
            var dst = new List<Item>();
            for (var i = 0; i < ReferenceArray.Count; i++)
            {
                dst.Add(new Item() { Str = ReferenceArray[i].Str });
            }

            return dst;
        }

        [Benchmark()]
        public List<Item> FastTypesCloneRef()
        {
            return FastType.DeepCopy(ReferenceArray);
        }

        public class Item
        {
            public string Str { get; set; }
        }
    }

   
}
