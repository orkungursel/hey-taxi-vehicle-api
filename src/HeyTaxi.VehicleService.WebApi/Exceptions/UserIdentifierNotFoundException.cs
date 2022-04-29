namespace HeyTaxi.VehicleService.WebApi.Exceptions;

public class UserIdentifierNotFoundException: Exception
{
    public UserIdentifierNotFoundException() : base("User Identifier not found")
    {
    }
}