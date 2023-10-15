using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RAZE.Entities;

namespace RAZE.Repositories
{
    public interface IDatabaseRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        void Update(T entity);

        IEnumerable<T> Read();

        T ReadOne(Func<T, bool> predicate, params Expression<Func<T, object>>[] navigationProperties);

        IEnumerable<T> Read(Func<T, bool> predicate, params Expression<Func<T, object>>[] navigationProperties);

        int Count();
    }
}