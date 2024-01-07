using System.Reflection;
using System.Reflection.Emit;

namespace FastTypes.Tests.Query.Scanner
{
    public sealed class CustomAssemblyBuilder
    {
        public AssemblyBuilder Builder { get; }
        public ModuleBuilder ModuleBuilder { get; }

        public CustomAssemblyBuilder(string name = null)
        {
            // Create a dynamic assembly
            Builder = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName(name ?? "DynamicAssembly"),
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
}