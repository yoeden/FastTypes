using System.Reflection;
using System.Reflection.Emit;

namespace FastTypes.Tests.Query.Scanner
{
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