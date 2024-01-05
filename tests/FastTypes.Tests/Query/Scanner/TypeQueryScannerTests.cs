using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;
using System.Runtime.Loader;
using System.Xml.Linq;
using FastTypes.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Scanner
{
    public class TypeQueryScannerTests
    {
        [Fact]
        public void Scan_SingleAssembly()
        {
            //
            Assembly assembly = new CustomAssemblyBuilder()
                .DefineType("CustomType")
                .BuildType(out _)
                .DefineType("CustomInterface", TypeAttributes.Interface | TypeAttributes.Public | TypeAttributes.Abstract)
                .BuildType(out _)
                .BuildAssembly();
            var expectedTypes = assembly.GetTypes().Where(t => t.IsClass);

            //
            var result = FastType
                .Query()
                .FromAssembly(assembly)
                .Targeting(selector => selector.Classes())
                .Scan();

            //
            result.Assemblies.Should().Equal(assembly);
            result.Types.Should().Equal(expectedTypes);
        }

        [Fact]
        public void Scan_MultipleAssembly()
        {
            //
            var expectedTypes = new List<Type>();

            Assembly assembly1 = new CustomAssemblyBuilder()
                .DefineType("CustomType1")
                .BuildType(out _)
                .DefineType("CustomInterface", TypeAttributes.Interface | TypeAttributes.Public | TypeAttributes.Abstract)
                .BuildType(out _)
                .BuildAssembly();
            expectedTypes.AddRange(assembly1.GetTypes().Where(t => t.IsClass));

            Assembly assembly2 = new CustomAssemblyBuilder()
                .DefineType("CustomType2")
                .BuildType(out _)
                .BuildAssembly();
            expectedTypes.AddRange(assembly2.GetTypes().Where(t => t.IsClass));


            //
            var result = FastType
                .Query()
                .FromAssemblies(assembly1, assembly2)
                .Targeting(selector => selector.Classes())
                .Scan();

            //
            result.Assemblies.Should().Equal(assembly1, assembly2);

            //Order doesn't matter
            result.Types.Should().Contain(expectedTypes);
        }

        [Fact]
        public void FindTypes_SingleAssembly()
        {
            //
            Assembly assembly = new CustomAssemblyBuilder()
                .DefineType("CustomType")
                .BuildType(out _)
                .DefineType("CustomInterface", TypeAttributes.Interface | TypeAttributes.Public | TypeAttributes.Abstract)
                .BuildType(out _)
                .BuildAssembly();
            var expectedTypes = assembly.GetTypes().Where(t => t.IsClass);

            //
            var result = FastType
                .Query()
                .FromAssembly(assembly)
                .Targeting(selector => selector.Classes())
                .FindTypes();

            //
            result.Should().Equal(expectedTypes);
        }

        [Fact]
        public void FindTypes_MultipleAssembly()
        {
            //
            var expectedTypes = new List<Type>();

            Assembly assembly1 = new CustomAssemblyBuilder()
                .DefineType("CustomType1")
                .BuildType(out _)
                .DefineType("CustomInterface", TypeAttributes.Interface | TypeAttributes.Public | TypeAttributes.Abstract)
                .BuildType(out _)
                .BuildAssembly();
            expectedTypes.AddRange(assembly1.GetTypes().Where(t => t.IsClass));

            Assembly assembly2 = new CustomAssemblyBuilder()
                .DefineType("CustomType2")
                .BuildType(out _)
                .BuildAssembly();
            expectedTypes.AddRange(assembly2.GetTypes().Where(t => t.IsClass));


            //
            var result = FastType
                .Query()
                .FromAssemblies(assembly1, assembly2)
                .Targeting(selector => selector.Classes())
                .FindTypes();

            //
            result.Should().Contain(expectedTypes);
        }
    }

    public sealed class CustomAssemblyBuilder
    {
        public AssemblyBuilder Builder { get; }
        public ModuleBuilder ModuleBuilder { get; }

        public CustomAssemblyBuilder()
        {
            // Create a dynamic assembly
            Builder = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName("DynamicAssembly"),
                //The dynamic assembly will be automatically unloaded and its memory reclaimed, when it's no longer accessible.
                //Important so it will unload and wont break any other tests
                AssemblyBuilderAccess.RunAndCollect
                );

            // Create a dynamic module
            ModuleBuilder = Builder.DefineDynamicModule("DynamicModule");
        }

        public CustomTypeBuilder DefineType(string name, TypeAttributes attributes = TypeAttributes.Public)
        {
            return new CustomTypeBuilder(this, name, attributes);
        }

        public Assembly BuildAssembly()
        {
            return Builder;
        }
    }

    public sealed class CustomTypeBuilder
    {
        private readonly CustomAssemblyBuilder _parent;
        private readonly TypeBuilder _typeBuilder;

        public CustomTypeBuilder(CustomAssemblyBuilder parent, string name, TypeAttributes attributes = TypeAttributes.Public)
        {
            _parent = parent;
            _typeBuilder = _parent.ModuleBuilder.DefineType(name, attributes);
        }

        public CustomAssemblyBuilder BuildType(out Type t)
        {
            // Create the type
            t = _typeBuilder.CreateType();
            return _parent;
        }
    }
}
