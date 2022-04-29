using HeyTaxi.VehicleService.Domain.Core.Models;
using HeyTaxi.VehicleService.Domain.Enums;

namespace HeyTaxi.VehicleService.Domain.Entities;

public class Vehicle : AuditableEntity
{
    public long DriverId { get; set; }

    public virtual Driver? Driver { get; set; }

    public string? Name { get; set; }

    public string? Plate { get; set; }

    public VehicleType Type { get; set; }

    public VehicleClass Class { get; set; }

    public int Seats { get; set; }
}