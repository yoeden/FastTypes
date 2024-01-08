using FastTypes.Query;
using FluentAssertions;

namespace FastTypes.Tests.Query.Criterias
{
    [Trait(Traits.Query.Tag,Traits.Query.Criterias)]
    public class TypeQueryCriteriaInterfaceTests
    {
        [Fact]
        public void Priority_OnDefault()
        {
            //
            ITypeQueryCriteria criteria = new SecretTypeQueryCriteria();

            //
            criteria.Priority.Should().Be(QueryCriteriaPriority.Mid);
        }

        private class SecretTypeQueryCriteria : ITypeQueryCriteria
        {
            public bool IsMatching(Type t)
            {
                return false;
            }

            public int Priority => QueryCriteriaPriority.Mid;
        }
    }
}
