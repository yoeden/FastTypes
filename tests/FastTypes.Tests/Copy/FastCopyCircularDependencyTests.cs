using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FastTypes.Tests.Copy.New
{
    [Trait(Traits.Copy.Tag, Traits.Copy.CircularDependency)]
    public class FastCopyCircularDependencyTests : BaseFastCopyTests
    {
        [Trait(Traits.Copy.Tag, Traits.Copy.Arrays)]
        [Trait(Traits.Copy.Tag, Traits.Copy.RefTypes)]
        [Fact]
        public void DeepCopy_OnCircularDependency_ShouldClone()
        {
            //
            var source = SimpleCircularDependency.Create();

            //
            var copy = InvokeDeepCopy(source, true);

            //
            copy.Should().NotBeSameAs(source);
            copy.Nested.Should().NotBeSameAs(source.Nested);

            copy.Nested.Should().HaveCount(source.Nested.Length);
        }

        [Trait(Traits.Copy.Tag, Traits.Copy.RefTypes)]
        [Fact]
        public void DeepCopy_OnComplexCircularDependency_ShouldClone()
        {
            //
            var source = ComplexDeeperLevelDependency.Create();

            //
            var copy = InvokeDeepCopy(source, true);

            //
            copy.Should().NotBeSameAs(source);

            AssertCircularDependency();

            void AssertCircularDependency()
            {
                var copyCrt = copy.AdditionalData;
                var srcCrt = source.AdditionalData;
                while (copyCrt != null)
                {
                    copyCrt.Should().NotBeSameAs(srcCrt);
                    copyCrt.Text.Should().Be(srcCrt.Text);

                    srcCrt = srcCrt.Additional;
                    copyCrt = copyCrt.Additional;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class SimpleCircularDependency
        {
            public static SimpleCircularDependency Create()
            {
                return new SimpleCircularDependency()
                {
                    Age = 23,
                    Name = "Abc",
                    Nested = new[]
                    {
                        new SimpleCircularDependency()
                        {
                            Age = 24,
                            Name = "Xyz"
                        }
                    }
                };
            }

            public string Name { get; set; }
            public int Age { get; set; }
            public SimpleCircularDependency[] Nested { get; set; }
        }

        private class ComplexDeeperLevelDependency
        {
            public static ComplexDeeperLevelDependency Create()
            {
                return new ComplexDeeperLevelDependency()
                {
                    AdditionalData = new Data()
                    {
                        Text = "Level 1",
                        Additional = new Data()
                        {
                            Text = "Level 2",
                            Additional = new Data()
                            {
                                Text = "Level 3",
                            }
                        }
                    }
                };
            }

            public Data AdditionalData { get; set; }

            public class Data
            {
                public string Text { get; set; }
                public Data Additional { get; set; }
            }
        }
    }
}
