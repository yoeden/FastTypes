using FastTypes.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Scanner
{
    public class TypeQueryScannerGroupedResultsTests : BaseTypeQueryScanner
    {
        [Fact]
        public void ScanForGroupedTypes_OnSingleAssembly_AndOneGroup()
        {
            //TODO: Replace with TypeCreation API when its done
            var assembly = CreateStubAssembly();
            var expectedTypes = assembly.GetTypes().Where(t => t.IsClass);

            //
            var snapshot = FastType
                .Query()
                .FromAssembly(assembly)
                .Targeting(selector => selector.Classes())
                .Prepare();

            //
            var result = TypeQueryScanner.ScanForGroupedTypes(snapshot);

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
            var snapshot = FastType
                .Query()
                .FromAssembly(assembly)
                .AllClasses()
                .And()
                .AllInterfaces()
                .Prepare();

            //
            var result = TypeQueryScanner.ScanForGroupedTypes(snapshot);

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
            var snapshot = FastType
                .Query()
                .FromAssemblies(assemblies)
                .Targeting(selector => selector.Classes())
                .Prepare();

            //
            var result = TypeQueryScanner.ScanForGroupedTypes(snapshot);

            //1 Group
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
            var snapshot = FastType
                .Query()
                .FromAssemblies(assemblies)
                .AllClasses()
                .And()
                .AllInterfaces()
                .Prepare();

            //
            var result = TypeQueryScanner.ScanForGroupedTypes(snapshot);

            result.Should().HaveCount(2);
            result[0].Should().Contain(expectedClasses);
            result[1].Should().Contain(expectedInterfaces);
        }
    }
}