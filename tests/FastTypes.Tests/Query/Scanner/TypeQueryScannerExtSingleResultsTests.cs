using FastTypes.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Scanner
{
    public class TypeQueryScannerExtSingleResultsTests : BaseTypeQueryScanner
    {
        [Fact]
        public void ScanForTypes_OnSingleAssembly_AndOneGroup()
        {
            //TODO: Replace with TypeCreation API when its done
            var assembly = CreateStubAssembly();
            var expectedTypes = assembly.GetTypes().Where(t => t.IsClass);

            //
            var result = FastType
                .Query()
                .FromAssembly(assembly)
                .Target(selector => selector.Classes())
                .FindTypes();

            //
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
            var result = FastType
                .Query()
                .FromAssembly(assembly)
                .TargetClasses()
                .And()
                .TargetInterfaces()
                .FindTypes();

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
            var result = FastType
                .Query()
                .FromAssemblies(assemblies)
                .Target(selector => selector.Classes())
                .FindTypes();

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
            var result = FastType
                .Query()
                .FromAssemblies(assemblies)
                .TargetClasses()
                .And()
                .TargetInterfaces()
                .FindTypes();

            //
            result.Should().Contain(expectedClasses.Concat(expectedInterfaces));
        }
    }
}