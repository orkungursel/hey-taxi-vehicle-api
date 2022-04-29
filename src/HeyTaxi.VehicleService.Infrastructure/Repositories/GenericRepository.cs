using System.Linq.Expressions;
using HeyTaxi.VehicleService.Domain.Core.Models;
using HeyTaxi.VehicleService.Domain.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace HeyTaxi.VehicleService.Infrastructure.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _set;

    protected GenericRepository(DbContext dbContext)
    {
        _set = dbContext.Set<T>();
    }

    public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _set.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _set.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await _set.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        return await _set.Where(expression).ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<T>> FindAllNoTrackingAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        return await _set.Where(expression).AsNoTracking().ToListAsync(cancellationToken);
    }
    
    public async Task<T> AddAsync(T entity)
    {
        var entityEntry = await _set.AddAsync(entity);
        return entityEntry.Entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _set.AddRangeAsync(entities);
    }

    public Task<T> UpdateAsync(T entity)
    {
        var entityEntry = _set.Update(entity);

        return Task.FromResult(entityEntry.Entity);
    }

    public void RemoveAsync(T entity)
    {
        _set.Remove(entity);
    }

    public void RemoveRangeAsync(IEnumerable<T> entities)
    {
        _set.RemoveRange(entities);
    }
}