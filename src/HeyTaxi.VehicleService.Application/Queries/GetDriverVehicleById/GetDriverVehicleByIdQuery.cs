using HeyTaxi.VehicleService.Application.Interfaces;
using HeyTaxi.VehicleService.Application.Models.DTOs;

namespace HeyTaxi.VehicleService.Application.Queries.GetDriverVehicleById;

public class GetDriverVehicleByIdQuery: IQuery<VehicleDTO?>
{
    public GetDriverVehicleByIdQuery(string driverId, string vehicleId)
    {
        DriverId = driverId;
        VehicleId = vehicleId;
    }

    public string DriverId { get; }
    public string VehicleId { get; }
}