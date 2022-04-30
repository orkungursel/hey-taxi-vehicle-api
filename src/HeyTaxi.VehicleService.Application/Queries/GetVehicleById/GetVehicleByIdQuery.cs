using HeyTaxi.VehicleService.Application.Interfaces;
using HeyTaxi.VehicleService.Application.Models.DTOs;

namespace HeyTaxi.VehicleService.Application.Queries.GetVehicleById;

public class GetVehicleByIdQuery : IQuery<VehicleWithDriverDTO?> {
    public GetVehicleByIdQuery(string vehicleId) {
        VehicleId = vehicleId;
    }

    public string VehicleId { get; }
}
