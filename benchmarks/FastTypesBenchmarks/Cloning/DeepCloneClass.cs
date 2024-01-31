using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AnyClone;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using DeepCopy;
using FastTypes;
using FastTypes.Clone;
using Force.DeepCloner;
using ShereSoft.Extensions;

namespace FastTypesBenchmarks.Cloning
{
    //Bench ValueTypes on object
    //Bench class on object
    //Bench Fasttype for both 

    public class DeppClone_Object_ComplexType : BaseBenchmarks
    {
        [Benchmark(Baseline = true)]
        public CloneTarget ManualTest()
        {
            return CloneInstance.DeepCopy(CloneInstance.Instance);
        }

        [Benchmark]
        public object FastTypesTests()
        {
            return FastType.DeepCopy((object)CloneInstance.Instance);
        }

        //[Benchmark]
        //public object Force_DeepClonerTests()
        //{
        //    return DeepClonerExtensions.DeepClone((object)CloneInstance.Instance);
        //}

        //[Benchmark]
        //public object JsonSerializerTests()
        //{
        //    return JsonSerializer.Deserialize(JsonSerializer.Serialize(CloneInstance.Instance), typeof(CloneTarget));
        //}

        //[Benchmark]
        //public object ObjectClonerTests()
        //{
        //    return ObjectCloner.ObjectCloner.DeepClone((object)CloneInstance.Instance);
        //}

        //[Benchmark]
        //public object AnyCloneTests()
        //{
        //    return CloneExtensions.Clone((object)CloneInstance.Instance);
        //}

        //[Benchmark]
        //public object ShereSoft_DeepCloningTests()
        //{
        //    return ObjectExtensions.DeepClone((object)CloneInstance.Instance);
        //}

        //[Benchmark]
        //public object ReubenBond_DeepCopyTests()
        //{
        //    return DeepCopier.Copy((object)CloneInstance.Instance);
        //}
    }

    public class DeepClone_Generic_ComplexType : BaseBenchmarks
    {
        [Benchmark(Baseline = true)]
        public CloneTarget ManualTest()
        {
            return CloneInstance.DeepCopy(CloneInstance.Instance);
        }

        [Benchmark]
        public CloneTarget FastTypesTests()
        {
            return FastType.DeepCopy(CloneInstance.Instance);
        }

        [Benchmark]
        public CloneTarget Force_DeepClonerTests()
        {
            return DeepClonerExtensions.DeepClone(CloneInstance.Instance);
        }

        [Benchmark]
        public CloneTarget JsonSerializerTests()
        {
            return JsonSerializer.Deserialize<CloneTarget>(JsonSerializer.Serialize(CloneInstance.Instance));
        }

        [Benchmark]
        public CloneTarget ObjectClonerTests()
        {
            return ObjectCloner.ObjectCloner.DeepClone(CloneInstance.Instance);
        }

        [Benchmark]
        public CloneTarget AnyCloneTests()
        {
            return CloneExtensions.Clone(CloneInstance.Instance);
        }

        [Benchmark]
        public CloneTarget ShereSoft_DeepCloningTests()
        {
            return ObjectExtensions.DeepClone(CloneInstance.Instance);
        }

        [Benchmark]
        public CloneTarget ReubenBond_DeepCopyTests()
        {
            return DeepCopier.Copy(CloneInstance.Instance);
        }
    }

    public class DeepCloneBackwardCompatibility_Generic_ComplexType : FastTypesBaseBenchmarks
    {
        [Benchmark(Baseline = true)]
        public CloneTarget ManualTest()
        {
            return CloneInstance.DeepCopy(CloneInstance.Instance);
        }

        [Benchmark]
        public CloneTarget FastTypesTests()
        {
            return FastType.DeepCopy(CloneInstance.Instance);
        }
    }

    public class DeepCloneBackwardCompatibility_Object_ComplexType : FastTypesBaseBenchmarks
    {
        [Benchmark(Baseline = true)]
        public CloneTarget ManualTest()
        {
            return CloneInstance.DeepCopy(CloneInstance.Instance);
        }

        [Benchmark]
        public CloneTarget FastTypesTests()
        {
            return (CloneTarget)FastType.DeepCopy((object)CloneInstance.Instance);
        }
    }
}

