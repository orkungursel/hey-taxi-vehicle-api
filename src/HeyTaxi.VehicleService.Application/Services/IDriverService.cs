using HeyTaxi.VehicleService.Application.Models.DTOs;
using HeyTaxi.VehicleService.Domain.Entities;

namespace HeyTaxi.VehicleService.Application.Services;

public interface IDriverService
{
    Task<Driver> GetOrCreateDriverAsync(string driverId, CancellationToken cancellationToken);
    Task<object?> GetDriverFromGrpcAsync(string driverId, CancellationToken cancellationToken);
}