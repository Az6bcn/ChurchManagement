using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class, IAggregateRoot
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepositoryAsync(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<T>> GetAllAsync()
    {
        var response = await _dbContext.Set<T>().ToListAsync();

        return response;
    }

    public async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        var response = await _dbContext.Set<T>()
                                       .Where(predicate)
                                       .ToListAsync();

        return response;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var response = await _dbContext.Set<T>()
                                       .FindAsync(id);

        return response;
    }

    public async Task<T> GetByGuidAsync(Guid guid)
    {
        var response = await _dbContext.Set<T>()
                                       .FindAsync(guid);

        return response;
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public async Task AddRangeAsync(ICollection<T> entities)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities);
    }

    public void Remove(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public void RemoveRange(ICollection<T> entities)
    {
        _dbContext.Set<T>().RemoveRange(entities);
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public void UpdateRange(ICollection<T> entities)
    {
        _dbContext.Set<T>().UpdateRange(entities);
    }
}