using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using DeepCopy;
using FastTypes;
using FastTypes.Clone;
using ShereSoft.Extensions;

namespace FastTypesBenchmarks.Cloning
{
    public class DeepClone_Dictionary_Ref : BaseBenchmarks
    {
        private readonly Dictionary<int, Item> _referenceDic;

        public DeepClone_Dictionary_Ref()
        {
            var items = Enumerable.Range(0, 1000).Select(i => new Item() { Str = i.ToString(), Index = i });
            _referenceDic = new Dictionary<int, Item>(1000 * 10);
            foreach (Item item in items)
            {
                _referenceDic.Add(item.Index, item);
            }
        }

        [Benchmark(Baseline = true)]
        public Dictionary<int, Item> AddEachItemTests()
        {
            var dst = new Dictionary<int, Item>(_referenceDic.Count);

            foreach (var item in _referenceDic)
            {
                dst.Add(item.Key, new Item() { Str = item.Value.Str, Index = item.Value.Index });
            }

            return dst;
        }

        [Benchmark()]
        public Dictionary<int, Item> AddEachItemWithoutInitialCapacityTests()
        {
            var dst = new Dictionary<int, Item>();

            foreach (var item in _referenceDic)
            {
                dst.Add(item.Key, new Item() { Str = item.Value.Str, Index = item.Value.Index });
            }

            return dst;
        }

        [Benchmark()]
        public Dictionary<int, Item> FastTypesCloneRef()
        {
            return FastType.DeepCopy(_referenceDic);
        }

        [Benchmark]
        public Dictionary<int, Item> ShereSoftTests()
        {
            return ObjectExtensions.DeepClone(_referenceDic);
        }

        [Benchmark]
        public Dictionary<int, Item> ReubenBondTests()
        {
            return DeepCopier.Copy(_referenceDic);
        }

        public class Item
        {
            public string Str { get; set; }
            public int Index { get; set; }
        }
    }
}
