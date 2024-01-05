using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastTypes.Query;
using FluentAssertions;
using NSubstitute;

namespace FastTypes.Tests.Query.Criterias
{
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
        }
    }
}
