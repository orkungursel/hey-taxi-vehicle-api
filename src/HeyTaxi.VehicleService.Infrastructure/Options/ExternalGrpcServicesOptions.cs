using Microsoft.Extensions.Options;

namespace HeyTaxi.VehicleService.Infrastructure.Options;

public class ExternalGrpcServicesOptions
{
    public const string Section = "Services";
    public string UserDetailsServiceUri { get; init; } = null!;
}
