using System;
using System.Collections.Generic;
using System.Text;

namespace FastTypes.Query
{
    internal partial class TypeQueryBuilder
    {
        public ITypeQueryBuilderCriterias Target(Action<ITypeSelector> types)
        {
            var selector = new TypeSelector();
            types(selector);

            _currentCriteriasScope.Add(selector.Create());

            return this;
        }
    }
}
