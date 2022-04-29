using HeyTaxi.VehicleService.Domain.Core.Models;

namespace HeyTaxi.VehicleService.Domain.Entities;

public class Driver : BaseEntity
{
    public string? DriverId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Avatar { get; set; }

    public virtual ICollection<Vehicle>? Vehicles { get; set; }
}