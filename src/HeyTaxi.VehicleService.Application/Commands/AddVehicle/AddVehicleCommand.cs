using HeyTaxi.VehicleService.Application.Interfaces;
using HeyTaxi.VehicleService.Application.Models.DTOs;

namespace HeyTaxi.VehicleService.Application.Commands.AddVehicle;

public class AddVehicleCommand : ICommand<VehicleDTO>
{
    public AddVehicleCommand(string driverId, AddVehicleRequest data) : base()
    {
        Data = data;
        DriverId = driverId;
    }
    
    public AddVehicleRequest Data;
    public string DriverId;
}