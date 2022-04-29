namespace HeyTaxi.VehicleService.Infrastructure.Exceptions;

internal class DriverNotFoundException : Exception
{
    public DriverNotFoundException(string driverId) : base($"Driver with id {driverId} not found")
    {
    }
}