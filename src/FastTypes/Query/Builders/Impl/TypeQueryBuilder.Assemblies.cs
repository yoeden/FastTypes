using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FastTypes.Query
{
    internal partial class TypeQueryBuilder
    {
        public ITypeQueryBuilderTarget FromAssembly(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            return FromAssemblies(assembly);
        }

        public ITypeQueryBuilderTarget FromAllAssemblies()
        {
            return FromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        }

        public ITypeQueryBuilderTarget AssemblyContaining<T>()
        {
            return FromAssemblies(typeof(T).Assembly);
        }

        public ITypeQueryBuilderTarget AssemblyContaining(Type t)
        {
            return FromAssemblies(t.Assembly);
        }

        public ITypeQueryBuilderTarget FromAssemblies(params Assembly[] assemblies)
        {
            _assemblies.AddRange(assemblies);
            return this;
        }

        public ITypeQueryBuilderTarget FromEntryAssembly()
        {
            return FromAssembly(Assembly.GetEntryAssembly());
        }

        public ITypeQueryBuilderTarget FromExecutingAssembly()
        {
            return FromAssembly(Assembly.GetExecutingAssembly());
        }

        public ITypeQueryBuilderTarget FromCallingAssembly()
        {
            return FromAssembly(Assembly.GetCallingAssembly());
        }
    }
}
