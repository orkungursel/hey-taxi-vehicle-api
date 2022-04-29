namespace HeyTaxi.VehicleService.Application.Exceptions;

public class DriverNotFoundException: Exception
{
    public DriverNotFoundException(string id) : base($"Driver with id {id} not found.")
    {
    }
}