using System;
using System.Collections.Generic;
using System.Reflection;

namespace FastTypes.Features.Query
{
    internal sealed partial class TypeQueryBuilder : ITypeQueryBuilderAssembly, ITypeQueryBuilderTypes, ITypeQueryBuilderModifiers
    {
        private readonly List<Assembly> _assemblies = new();
        private readonly List<ITypeQueryCriteria> _criterias = new();

        //

        public ITypeQueryBuilderTypes FromAssembly(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            return FromAssemblies(assembly);
        }

        public ITypeQueryBuilderTypes FromAllAssemblies()
        {
            return FromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        }

        public ITypeQueryBuilderTypes AssemblyOfType<T>()
        {
            return FromAssemblies(typeof(T).Assembly);
        }

        public ITypeQueryBuilderTypes AssemblyOfType(Type t)
        {
            return FromAssemblies(t.Assembly);
        }

        public ITypeQueryBuilderTypes FromAssemblies(params Assembly[] assemblies)
        {
            _assemblies.AddRange(assemblies);
            return this;
        }

        //

        public ITypeQueryBuilderModifiers Targeting(Action<ITypeSelector> types)
        {
            var selector = new TypeSelector();
            types(selector);

            _criterias.Add(selector.Create());

            return this;
        }

        //

        public ITypeQueryBuilderModifiers WithCriteria(ITypeQueryCriteria criteria)
        {
            _criterias.Add(criteria);
            return this;
        }

        public ITypeQueryBuilderModifiers NotPublic()
        {
            return WithCriteria(new AccessModifierCriteria());
        }

        public ITypeQueryBuilderModifiers WithPropertyOfType<T>()
        {
            return WithPropertyOfType(typeof(T));
        }

        public ITypeQueryBuilderModifiers WithPropertyOfType(Type t)
        {
            return WithCriteria(new PropertyOfTypeCriteria(t));
        }

        public ITypeQueryBuilderModifiers WithMethodOfType<T>()
        {
            return WithMethodOfType(typeof(T));
        }

        public ITypeQueryBuilderModifiers WithMethodOfType(Type t)
        {
            return WithCriteria(new MethodOfTypeCriteria(t));
        }

        public ITypeQueryBuilderModifiers WithAttribute<T>() where T : Attribute
        {
            return WithAttribute(typeof(T));
        }

        public ITypeQueryBuilderModifiers WithAttribute(Type t)
        {
            return WithCriteria(new AttributeCriteria(t));
        }

        public ITypeQueryBuilderModifiers AssignableTo<T>()
        {
            return WithCriteria(new AssignableToCriteria(typeof(T)));
        }

        public ITypeQueryBuilderModifiers AssignableTo(Type t)
        {
            return WithCriteria(new AssignableToCriteria(t));
        }

        public TypeQueryContext Prepare()
        {
            return new TypeQueryContext(_assemblies.AsReadOnly(), _criterias.AsReadOnly());
        }
    }
}