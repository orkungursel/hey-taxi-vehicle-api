using HeyTaxi.VehicleService.Application.Interfaces;
using HeyTaxi.VehicleService.Application.Services;
using HeyTaxi.VehicleService.Domain.Entities;

namespace HeyTaxi.VehicleService.Application.Queries.Foo;

public class FooQueryHandler : IQueryHandler<FooQuery, object>
{
    private readonly IDriverService _driverService;

    public FooQueryHandler(IDriverService driverService)
    {
        _driverService = driverService;
    }

    public async Task<object> Handle(FooQuery request, CancellationToken cancellationToken = default)
    {
        var driver = await _driverService.GetDriverFromGrpcAsync(request.DriverId, cancellationToken);
        return driver!;
    }
}