namespace HeyTaxi.VehicleService.Application.Models.DTOs;

public class VehicleWithDriverDTO : VehicleDTO
{
    public DriverDTO? Driver { get; set; }
}
