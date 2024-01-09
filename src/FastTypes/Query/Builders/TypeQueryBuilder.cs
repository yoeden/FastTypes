using System;
using System.Collections.Generic;
using System.Reflection;

namespace FastTypes.Query
{
    internal sealed partial class TypeQueryBuilder : ITypeQueryBuilderAssembly, ITypeQueryBuilderTarget, ITypeQueryBuilderCriterias
    {
        private readonly List<Assembly> _assemblies = new();
        private readonly List<TypeQueryGroup> _groups = new();

        private readonly List<ITypeQueryCriteria> _currentCriteriasScope = new();
        private readonly Dictionary<Type, object> _tags = new();
        private bool _isNotPublicSet;

        //

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

        //

        public ITypeQueryBuilderCriterias Target(Action<ITypeSelector> types)
        {
            var selector = new TypeSelector();
            types(selector);

            _currentCriteriasScope.Add(selector.Create());

            return this;
        }

        //

        public ITypeQueryBuilderCriterias WithCriteria(ITypeQueryCriteria criteria)
        {
            _currentCriteriasScope.Add(criteria);
            return this;
        }

        public ITypeQueryBuilderCriterias Tag<T>(T tag)
        {
            //TODO: throw tag already exists
            _tags.Add(tag.GetType(), tag);
            return this;
        }

        public ITypeQueryBuilderCriterias NotPublic()
        {
            //TODO: Default search is public types, unit tests this
            _isNotPublicSet = true;
            return WithCriteria(new AccessModifierCriteria());
        }

        public ITypeQueryBuilderCriterias WithPropertyOfType<T>()
        {
            return WithPropertyOfType(typeof(T));
        }

        public ITypeQueryBuilderCriterias WithPropertyOfType(Type t)
        {
            return WithCriteria(new PropertyOfTypeCriteria(t));
        }

        public ITypeQueryBuilderCriterias WithMethodOfType<T>()
        {
            return WithMethodOfType(typeof(T));
        }

        public ITypeQueryBuilderCriterias WithMethodOfType(Type t)
        {
            return WithCriteria(new MethodOfTypeCriteria(t));
        }

        public ITypeQueryBuilderCriterias WithAttribute<T>() where T : Attribute
        {
            return WithAttribute(typeof(T));
        }

        public ITypeQueryBuilderCriterias WithAttribute(Type t)
        {
            return WithCriteria(new AttributeCriteria(t));
        }

        public ITypeQueryBuilderCriterias AssignableTo<T>()
        {
            return WithCriteria(new AssignableToCriteria(typeof(T)));
        }

        public ITypeQueryBuilderCriterias AssignableTo(Type t)
        {
            return WithCriteria(new AssignableToCriteria(t));
        }

        public TypeQuerySnapshot Snapshot()
        {
            SealCurrentScope();

            return new TypeQuerySnapshot(_assemblies.AsReadOnly(), _groups.AsReadOnly());
        }

        public ITypeQueryBuilderTarget And()
        {
            SealCurrentScope();

            _currentCriteriasScope.Clear();
            _tags.Clear();

            return this;
        }

        private void SealCurrentScope()
        {
            if (!_isNotPublicSet) _currentCriteriasScope.Add(new AccessModifierCriteria(isPublic: true));

            _isNotPublicSet = false;

            var criterias = new List<ITypeQueryCriteria>(_currentCriteriasScope.Count);
            criterias.AddRange(_currentCriteriasScope);
            criterias.TrimExcess();

            var tags = new Dictionary<Type, object>(_tags);
            tags.TrimExcess();

            _groups.Add(new TypeQueryGroup(new QueryCriterias(criterias), QueryTags.FromDictionary(tags)));
        }
    }
}