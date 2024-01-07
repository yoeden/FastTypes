using System;
using System.Collections.Generic;
using System.Reflection;

namespace FastTypes.Query
{
    internal sealed partial class TypeQueryBuilder : ITypeQueryBuilderAssembly, ITypeQueryBuilderTargets, ITypeQueryBuilderModifiers
    {
        private readonly List<Assembly> _assemblies = new();
        private readonly List<TypeQueryGroup> _groups = new();

        private readonly List<ITypeQueryCriteria> _currentCriteriasScope = new();
        private readonly Dictionary<Type, object> _tags = new();

        //

        public ITypeQueryBuilderTargets FromAssembly(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            return FromAssemblies(assembly);
        }

        public ITypeQueryBuilderTargets FromAllAssemblies()
        {
            return FromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        }

        public ITypeQueryBuilderTargets AssemblyOfType<T>()
        {
            return FromAssemblies(typeof(T).Assembly);
        }

        public ITypeQueryBuilderTargets AssemblyOfType(Type t)
        {
            return FromAssemblies(t.Assembly);
        }

        public ITypeQueryBuilderTargets FromAssemblies(params Assembly[] assemblies)
        {
            _assemblies.AddRange(assemblies);
            return this;
        }

        //

        public ITypeQueryBuilderModifiers Targeting(Action<ITypeSelector> types)
        {
            var selector = new TypeSelector();
            types(selector);

            _currentCriteriasScope.Add(selector.Create());

            return this;
        }

        //

        public ITypeQueryBuilderModifiers WithCriteria(ITypeQueryCriteria criteria)
        {
            _currentCriteriasScope.Add(criteria);
            return this;
        }

        public ITypeQueryBuilderModifiers Tag<T>(T tag)
        {
            //TODO: throw tag already exists
            _tags.Add(tag.GetType(), tag);
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

        public TypeQuerySnapshot Prepare()
        {
            SealCurrentScope();

            return new TypeQuerySnapshot(_assemblies.AsReadOnly(), _groups.AsReadOnly());
        }

        public ITypeQueryBuilderTargets And()
        {
            SealCurrentScope();

            _currentCriteriasScope.Clear();
            _tags.Clear();

            return this;
        }

        private void SealCurrentScope()
        {
            var criterias = new List<ITypeQueryCriteria>(_currentCriteriasScope.Count);
            criterias.AddRange(_currentCriteriasScope);
            criterias.TrimExcess();

            var tags = new Dictionary<Type, object>(_tags);
            tags.TrimExcess();

            _groups.Add(new TypeQueryGroup(new QueryCriterias(criterias), QueryTags.FromDictionary(tags)));
        }
    }
}