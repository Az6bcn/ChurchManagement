using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        T GetById(int id);

        T GetByGuid(Guid guid);
        void Add(T entity);
        
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        
        void RemoveRange(IEnumerable<T> entities);
        
        void Update(T entity);
        
        void UpdateRange(IEnumerable<T> entities);
    }
}
