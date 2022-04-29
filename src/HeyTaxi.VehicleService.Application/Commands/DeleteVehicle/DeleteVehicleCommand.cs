using HeyTaxi.VehicleService.Application.Interfaces;
using MediatR;

namespace HeyTaxi.VehicleService.Application.Commands.DeleteVehicle;

public class DeleteVehicleCommand : ICommand<Unit>
{
    public DeleteVehicleCommand(string vehicleId, string driverId)
    {
        VehicleId = vehicleId;
        DriverId = driverId;
    }

    public string VehicleId { get; }
    public string DriverId { get; }
}