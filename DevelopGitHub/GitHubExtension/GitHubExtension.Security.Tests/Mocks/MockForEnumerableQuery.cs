using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GitHubExtension.Security.Tests.Mocks
{
    class MockForEnumerableQuery<T> : EnumerableQuery<T>, IDbAsyncQueryProvider
    {
        public MockForEnumerableQuery(IEnumerable<T> collection)
            : base(collection)
        {

        }

        async Task<object> IDbAsyncQueryProvider.ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return ((IQueryProvider)this).Execute(expression);
        }

        async Task<TResult> IDbAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return ((IQueryProvider)this).Execute<TResult>(expression);
        }

        //public IQueryable<TElement> CreateQuery<TElement>(System.Linq.Expressions.Expression expression)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable CreateQuery(System.Linq.Expressions.Expression expression)
        //{
        //    throw new NotImplementedException();
        //}

        //public TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
        //{
        //    throw new NotImplementedException();
        //}

        //public object Execute(System.Linq.Expressions.Expression expression)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
