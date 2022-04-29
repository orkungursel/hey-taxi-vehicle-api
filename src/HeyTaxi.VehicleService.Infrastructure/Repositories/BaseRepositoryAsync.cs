using HeyTaxi.VehicleService.Domain.Core.Models;
using HeyTaxi.VehicleService.Domain.Core.Repository;
using HeyTaxi.VehicleService.Domain.Core.Specifications;
using HeyTaxi.VehicleService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HeyTaxi.VehicleService.Infrastructure.Repositories;

public class BaseRepositoryAsync<T> : IBaseRepositoryAsync<T> where T : BaseEntity
{
    private readonly VehicleDbContext _dbContext;

    public BaseRepositoryAsync(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IList<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }

    public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        return await ApplySpecification(spec).CountAsync(cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        return entity;
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
    }
}