using HeyTaxi.VehicleService.Application.Interfaces;
using HeyTaxi.VehicleService.Application.Models.DTOs;

namespace HeyTaxi.VehicleService.Application.Queries.GetDriverVehicles;

public class GetDriverVehiclesQuery : IQuery<IEnumerable<VehicleDTO>>
{
    public GetDriverVehiclesQuery(string driverId)
    {
        DriverId = driverId;
    }

    public string DriverId { get; }
}