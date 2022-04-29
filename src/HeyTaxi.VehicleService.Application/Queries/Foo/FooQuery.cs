using HeyTaxi.VehicleService.Application.Interfaces;
using HeyTaxi.VehicleService.Domain.Entities;
using MediatR;

namespace HeyTaxi.VehicleService.Application.Queries.Foo;

public class FooQuery : IQuery<object>
{
    public FooQuery(string driverId)
    {
        DriverId = driverId;
    }

    public string DriverId { get; }
}