using System;
using System.Collections.Generic;
using System.Text;

namespace FastTypes.Query
{
    internal partial class TypeQueryBuilder
    {
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
    }
}
