using HeyTaxi.VehicleService.Domain.Core.Models;
using HeyTaxi.VehicleService.Domain.Core.Specifications;

namespace HeyTaxi.VehicleService.Domain.Core.Repository;

public interface IBaseRepositoryAsync<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<IList<T>> ListAllAsync();
    Task<IList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken);
    Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    void Update(T entity);
    void Delete(T entity);
    Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken);
}