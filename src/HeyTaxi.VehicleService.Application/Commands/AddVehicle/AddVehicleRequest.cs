namespace HeyTaxi.VehicleService.Application.Commands.AddVehicle;

public class AddVehicleRequest
{
    public string? Name { get; set; }

    public string? Plate { get; set; }

    public string? Type { get; set; }

    public string? Class { get; set; }

    public int Seats { get; set; }
}