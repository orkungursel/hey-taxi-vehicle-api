using System.Linq.Expressions;
using HeyTaxi.VehicleService.Domain.Core.Models;

namespace HeyTaxi.VehicleService.Domain.Core.Repository;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllNoTrackingAsync(CancellationToken cancellationToken);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
    Task<IEnumerable<T>> FindAllNoTrackingAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task<T> UpdateAsync(T entity);
    void RemoveAsync(T entity);
    void RemoveRangeAsync(IEnumerable<T> entities);
}