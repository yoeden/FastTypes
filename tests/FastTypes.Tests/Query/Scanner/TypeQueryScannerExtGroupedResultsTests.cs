using FastTypes.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Scanner
{
    public class TypeQueryScannerExtGroupedResultsTests : BaseTypeQueryScanner
    {
        [Fact]
        public void ScanForGroupedTypes_OnSingleAssembly_AndOneGroup()
        {
            //TODO: Replace with TypeCreation API when its done
            var assembly = CreateStubAssembly();
            var expectedTypes = assembly.GetTypes().Where(t => t.IsClass);

            //
            var result = FastType
                .Query()
                .FromAssembly(assembly)
                .Target(selector => selector.Classes())
                .FindGroupedTypes();

            //
            result.Should().HaveCount(1);
            result[0].Should().Contain(expectedTypes);
        }

        [Fact]
        public void ScanForGroupedTypes_OnSingleAssembly_AndTwoGroup()
        {
            //TODO: Replace with TypeCreation API when its done
            var assembly = CreateStubAssembly();
            var expectedClasses = assembly.GetTypes().Where(t => t.IsClass);
            var expectedInterfaces = assembly.GetTypes().Where(t => t.IsInterface);

            //
            var result = FastType
                .Query()
                .FromAssembly(assembly)
                .TargetClasses()
                .And()
                .TargetInterfaces()
                .FindGroupedTypes();

            //
            result.Should().HaveCount(2);
            result[0].Should().Contain(expectedClasses);
            result[1].Should().Contain(expectedInterfaces);
        }

        [Fact]
        public void ScanForGroupedTypes_OnMultipleAssemblies_AndOneGroup()
        {
            //TODO: Replace with TypeCreation API when its done
            var assemblies = new[] { CreateStubAssembly("A1"), CreateStubAssembly("A2") };
            var expectedTypes = assemblies.SelectMany(a => a.GetTypes()).Where(t => t.IsClass);

            //
            var result = FastType
                .Query()
                .FromAssemblies(assemblies)
                .Target(selector => selector.Classes())
                .FindGroupedTypes();

            //
            result.Should().HaveCount(1);
            result[0].Should().Contain(expectedTypes);
        }

        [Fact]
        public void ScanForGroupedTypes_OnMultipleAssemblies_AndTwoGroup()
        {
            //TODO: Replace with TypeCreation API when its done
            var assemblies = new[] { CreateStubAssembly("A1"), CreateStubAssembly("A2") };
            var expectedClasses = assemblies.SelectMany(a => a.GetTypes()).Where(t => t.IsClass);
            var expectedInterfaces = assemblies.SelectMany(a => a.GetTypes()).Where(t => t.IsInterface);

            //
            var result = FastType
                .Query()
                .FromAssemblies(assemblies)
                .TargetClasses()
                .And()
                .TargetInterfaces()
                .FindGroupedTypes();

            //
            result.Should().HaveCount(2);
            result[0].Should().Contain(expectedClasses);
            result[1].Should().Contain(expectedInterfaces);
        }
    }
}