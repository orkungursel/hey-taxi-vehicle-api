namespace HeyTaxi.VehicleService.Application.Models.DTOs;

public class VehicleDTO
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Plate { get; set; }

    public string? Type { get; set; }

    public string? Class { get; set; }

    public int Seats { get; set; }
}