﻿using System;
using System.Reflection;

namespace FastTypes.Features.Query
{
    public interface ITypeQueryBuilderAssembly
    {
        ITypeQueryBuilderTypes FromAssembly(Assembly assembly);

        ITypeQueryBuilderTypes FromAssemblies(params Assembly[] assemblies);

        ITypeQueryBuilderTypes FromAllAssemblies();

        ITypeQueryBuilderTypes AssemblyOfType<T>();

        ITypeQueryBuilderTypes AssemblyOfType(Type t);
    }
}