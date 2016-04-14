using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace GitHubExtension.Security.Tests.Mocks
{
    class MockForDbSet<T> : MockForEnumerableQuery<T>, IDbSet<T> where T : class
    {
        private readonly IQueryable<T> data;

        public MockForDbSet(IEnumerable<T> collection) : base(collection)
        {
            data = collection.AsQueryable();
        }

        public Type ElementType
        {
            get { return data.ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return data.Expression; }
        }

        public ObservableCollection<T> Local
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return data.GetEnumerator();
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

        public T Remove(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
