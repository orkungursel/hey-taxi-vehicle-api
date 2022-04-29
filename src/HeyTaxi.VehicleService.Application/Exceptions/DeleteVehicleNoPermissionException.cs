namespace HeyTaxi.VehicleService.Application.Exceptions;

public class DeleteVehicleNoPermissionException: Exception
{
    public DeleteVehicleNoPermissionException(long driverId,
        long vehicleId) : base($"User with id {driverId} does not have permission to delete vehicle with id {vehicleId}")
    {
    }
}