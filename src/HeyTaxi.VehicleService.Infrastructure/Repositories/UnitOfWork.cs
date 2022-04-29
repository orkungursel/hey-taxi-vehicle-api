using HeyTaxi.VehicleService.Domain.Core.Models;
using HeyTaxi.VehicleService.Domain.Core.Repository;
using HeyTaxi.VehicleService.Infrastructure.Persistence.Context;

namespace HeyTaxi.VehicleService.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly VehicleDbContext _dbContext;
    private readonly IDictionary<Type, dynamic> _repositories;

    public UnitOfWork(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Dictionary<Type, dynamic>();
    }

    public IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity
    {
        var entityType = typeof(T);
        if (_repositories.ContainsKey(entityType))
        {
            return _repositories[entityType];
        }

        var repositoryType = typeof(BaseRepositoryAsync<>);
        var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

        _repositories.Add(entityType, repository ?? throw new InvalidOperationException());
        return (IBaseRepositoryAsync<T>)repository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RollBackChangesAsync()
    {
        await _dbContext.Database.RollbackTransactionAsync();
    }
}