using FastTypes.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Scanner
{
    public class TypeQueryScannerSingleResultsTests : BaseTypeQueryScanner
    {
        [Fact]
        public void ScanForTypes_OnSingleAssembly_AndOneGroup()
        {
            //TODO: Replace with TypeCreation API when its done
            var assembly = CreateStubAssembly();
            var expectedTypes = assembly.GetTypes().Where(t => t.IsClass);

            //
            var snapshot = FastType
                .Query()
                .FromAssembly(assembly)
                .Target(selector => selector.Classes())
                .Snapshot();

            //
            var result = TypeQueryScanner.ScanForTypes(snapshot);

            result.Should().Contain(expectedTypes);
        }

        [Fact]
        public void ScanForTypes_OnSingleAssembly_AndTwoGroup()
        {
            //TODO: Replace with TypeCreation API when its done
            var assembly = CreateStubAssembly();
            var expectedClasses = assembly.GetTypes().Where(t => t.IsClass);
            var expectedInterfaces = assembly.GetTypes().Where(t => t.IsInterface);

            //
            var snapshot = FastType
                .Query()
                .FromAssembly(assembly)
                .TargetClasses()
                .And()
                .TargetInterfaces()
                .Snapshot();

            //
            var result = TypeQueryScanner.ScanForTypes(snapshot);

            //
            result.Should().Contain(expectedClasses.Concat(expectedInterfaces));
        }

        [Fact]
        public void ScanForTypes_OnMultipleAssemblies_AndOneGroup()
        {
            //TODO: Replace with TypeCreation API when its done
            var assemblies = new[] { CreateStubAssembly("A1"), CreateStubAssembly("A2") };
            var expectedTypes = assemblies.SelectMany(a => a.GetTypes()).Where(t => t.IsClass);

            //
            var snapshot = FastType
                .Query()
                .FromAssemblies(assemblies)
                .Target(selector => selector.Classes())
                .Snapshot();

            //
            var result = TypeQueryScanner.ScanForTypes(snapshot);

            //1 Group
            result.Should().Contain(expectedTypes);
        }

        [Fact]
        public void ScanForTypes_OnMultipleAssemblies_AndTwoGroup()
        {
            //TODO: Replace with TypeCreation API when its done
            var assemblies = new[] { CreateStubAssembly("A1"), CreateStubAssembly("A2") };
            var expectedClasses = assemblies.SelectMany(a => a.GetTypes()).Where(t => t.IsClass);
            var expectedInterfaces = assemblies.SelectMany(a => a.GetTypes()).Where(t => t.IsInterface);

            //
            var snapshot = FastType
                .Query()
                .FromAssemblies(assemblies)
                .TargetClasses()
                .And()
                .TargetInterfaces()
                .Snapshot();

            //
            var result = TypeQueryScanner.ScanForTypes(snapshot);

            //
            result.Should().Contain(expectedClasses.Concat(expectedInterfaces));
        }
    }
}