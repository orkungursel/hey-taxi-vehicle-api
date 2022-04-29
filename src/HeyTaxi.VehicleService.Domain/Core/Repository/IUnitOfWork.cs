using HeyTaxi.VehicleService.Domain.Core.Models;

namespace HeyTaxi.VehicleService.Domain.Core.Repository;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
    Task RollBackChangesAsync();
    IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity;
}