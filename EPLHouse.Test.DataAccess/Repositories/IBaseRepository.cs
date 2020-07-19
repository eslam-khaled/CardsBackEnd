using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EPLHouse.Test.DataAccess.Repositories
{
    public interface IBaseRepository<T> where T:class
    {
        T AddNew(T obj);
        IEnumerable<T> GetAll();
        void FindById(int id);
        void Delete(T id);
        T GetById(int id);
        void Update(T obj);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);
    }
}
