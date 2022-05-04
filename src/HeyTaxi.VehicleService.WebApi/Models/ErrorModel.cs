namespace HeyTaxi.VehicleService.WebApi.Models;

class ErrorModel
{
    public ErrorModel()
    {
    }

    public ErrorModel(string message)
    {
        Message = message;
    }

    public string Message { get; set; } = null!;
}
