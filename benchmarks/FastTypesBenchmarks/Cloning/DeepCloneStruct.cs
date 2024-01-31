using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using DeepCopy;
using FastTypes;
using FastTypes.Clone;
using Microsoft.CodeAnalysis.Diagnostics;
using ShereSoft.Extensions;

namespace FastTypesBenchmarks.Cloning
{
    [ReturnValueValidator(true)]
    public class DeepClone_Generic_PureValueType : BaseBenchmarks
    {
        private readonly Guid Guid = Guid.NewGuid();

        [Benchmark(Baseline = true)]
        public Guid Manual()
        {
            Span<byte> dst = stackalloc byte[16];
            Guid.TryWriteBytes(dst);
            return new Guid(dst);
        }

        [Benchmark]
        public Guid FastTypesTest()
        {
            return FastType.DeepCopy(Guid);
        }

        [Benchmark]
        public Guid ShereSoft_DeepCloningTests()
        {
            return ObjectExtensions.DeepClone(Guid);
        }

        [Benchmark]
        public Guid ReubenBond_DeepCopyTests()
        {
            return DeepCopier.Copy(Guid);
        }
    }

    public class DeepClone_Generic_ComplexValueType : BaseBenchmarks
    {
        private readonly ValueTuple<Attributes> Complex = new(new Attributes()
        {
            Name = "Name",
            Age = 24,
            Body = "Body",
            Created = DateTime.Now,
            Gender = "Gender",
            Title = "Title",
            Updated = DateTime.Today
        });

        [Benchmark(Baseline = true)]
        public ValueTuple<Attributes> Manual()
        {
            return new ValueTuple<Attributes>(Complex.Item1.DeepCopy());
        }

        [Benchmark]
        public ValueTuple<Attributes> FastTypesTest()
        {
            return FastType.DeepCopy(Complex);
        }

        [Benchmark]
        public ValueTuple<Attributes> ShereSoft_DeepCloningTests()
        {
            return ObjectExtensions.DeepClone(Complex);
        }

        [Benchmark]
        public ValueTuple<Attributes> ReubenBond_DeepCopyTests()
        {
            return DeepCopier.Copy(Complex);
        }
    }
}
