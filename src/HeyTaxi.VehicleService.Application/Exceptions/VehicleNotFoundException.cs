namespace HeyTaxi.VehicleService.Application.Exceptions;

public class VehicleNotFoundException: Exception
{
    public VehicleNotFoundException(long id) : base($"Vehicle with id {id} not found.")
    {
    }
}