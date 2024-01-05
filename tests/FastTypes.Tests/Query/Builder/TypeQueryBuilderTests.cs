using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FastTypes.Features.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Builder
{
    public class TypeQueryBuilderAssemblyTests
    {
        private static IReadOnlyList<Assembly> GetAssembliesFromBuilder(Func<ITypeQueryBuilderAssembly, ITypeQueryBuilderTypes> action)
        {
            var builder = new TypeQueryBuilder();
            return action(builder)
                .Targeting(selector => selector.Classes())
                .Prepare()
                .Assemblies;
        }

        [Fact]
        public void FromAssembly_OnNullAssembly_ShouldThrows()
        {
            //
            var result = () => GetAssembliesFromBuilder(
                builder => builder.FromAssembly(null)
                );

            //
            result.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void FromAssembly_OnValidAssembly_ShouldSetAssembly()
        {
            //
            var assembly = Assembly.GetExecutingAssembly();

            //
            var result = GetAssembliesFromBuilder(
                builder => builder.FromAssembly(assembly)
            );

            //
            result.Should().Equal(assembly);
        }

        [Fact]
        public void AssemblyOfTypeT_ShouldSetAssembly()
        {
            //
            var assembly = Assembly.GetExecutingAssembly();

            //
            var result = GetAssembliesFromBuilder(
                builder => builder.AssemblyOfType<TypeQueryBuilderAssemblyTests>()
            );

            //
            result.Should().Equal(assembly);
        }

        [Fact]
        public void AssemblyOfType_ShouldSetAssembly()
        {
            //
            var assembly = Assembly.GetExecutingAssembly();

            //
            var result = GetAssembliesFromBuilder(
                builder => builder.AssemblyOfType(typeof(TypeQueryBuilderAssemblyTests))
            );

            //
            result.Should().Equal(assembly);
        }

        //TODO: This test fails on random runs
        //[Fact]
        public void FromAllAssemblies_ShouldSetAllAssemblies()
        {
            //
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            //
            var result = GetAssembliesFromBuilder(
                builder => builder.FromAllAssemblies()
            );

            //
            result.Should().Equal(assemblies, "Last assembly : " + assemblies[^1].FullName);
        }
    }
}
