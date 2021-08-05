using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Application.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<T> where T : class, IAggregateRoot
    {
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(int id);

        Task<T> GetByGuidAsync(Guid guid);
        Task AddAsync(T entity);

        Task AddRangeAsync(ICollection<T> entities);
        void Remove(T entity);

        void RemoveRange(ICollection<T> entities);

        void Update(T entity);

        void UpdateRange(ICollection<T> entities);
    }
}