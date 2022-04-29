using HeyTaxi.VehicleService.Application.Models.DTOs;
using HeyTaxi.VehicleService.Application.Services;
using HeyTaxi.VehicleService.Domain.Core.Repository;
using HeyTaxi.VehicleService.Domain.Entities;
using HeyTaxi.VehicleService.Domain.Specifications;
using HeyTaxi.VehicleService.Infrastructure.Clients;
using HeyTaxi.VehicleService.Infrastructure.Exceptions;

namespace HeyTaxi.VehicleService.Infrastructure.Services;

public class DriverService : IDriverService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserServiceClient _userServiceClient;

    public DriverService(UserServiceClient userServiceClient, IUnitOfWork unitOfWork)
    {
        _userServiceClient = userServiceClient;
        _unitOfWork = unitOfWork;
    }

    public async Task<Driver> GetOrCreateDriverAsync(string driverId, CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.Repository<Driver>()
            .FirstOrDefaultAsync(DriverSpecifications.GetDriverByIdSpec(driverId),
            cancellationToken);

        if (driver != null)
        {
            return driver;
        }
        
        var userInfo = await _userServiceClient.GetUserInfoAsync(driverId, cancellationToken);
        if (userInfo == null)
        {
            throw new DriverNotFoundException(driverId);
        }

        var newDriver = new Driver()
        {
            DriverId = userInfo.Id,
            Name = userInfo.Name,
            Avatar = userInfo.Avatar,
            Email = userInfo.Email,
        };

        return await _unitOfWork.Repository<Driver>().AddAsync(newDriver, cancellationToken);
    }

    public async Task<object?> GetDriverFromGrpcAsync(string driverId, CancellationToken cancellationToken)
    {
        var userInfo = await _userServiceClient.GetUserInfoAsync(driverId, cancellationToken);
        if (userInfo == null)
        {
            throw new DriverNotFoundException(driverId);
        }

        return userInfo;
    }
}