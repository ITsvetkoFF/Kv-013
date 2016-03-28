using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GitHubExtension.Security.Tests.Mocks
{
    class MockForDbSet<T> : MockForEnumerableQuery<T>, IDbSet<T> where T : class
    {
        private IQueryable<T> data;

        public MockForDbSet(IEnumerable<T> collection) : base(collection)
        {
            data = collection.AsQueryable();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        //IEnumerator GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        public Type ElementType
        {
            get { return data.ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return data.Expression; }
        }

        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public T Attach(T entity)
        {
            throw new NotImplementedException();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> Local
        {
            get { throw new NotImplementedException(); }
        }

        public T Remove(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
