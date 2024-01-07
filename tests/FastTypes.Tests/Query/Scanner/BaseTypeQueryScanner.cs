using System.Reflection;

namespace FastTypes.Tests.Query.Scanner
{
    public abstract class BaseTypeQueryScanner
    {
        protected static Assembly CreateStubAssembly(string name = null)
        {
            Assembly assembly = new CustomAssemblyBuilder(name)
                .DefineType("CustomType")
                .BuildType(out _)
                .DefineType("CustomInterface", TypeAttributes.Interface | TypeAttributes.Public | TypeAttributes.Abstract)
                .BuildType(out _)
                .BuildAssembly();
            return assembly;
        }
    }
}