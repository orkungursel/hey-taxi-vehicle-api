using HeyTaxi.VehicleService.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeyTaxi.VehicleService.Infrastructure.Configurations;

public static class ConfigureExternalGrpcServicesOptionsExtension
{
    public static IServiceCollection ConfigureExternalGrpcServiceOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ExternalGrpcServicesOptions>(options => configuration
            .GetSection(ExternalGrpcServicesOptions.Section)
            .Bind(options));
        
        return services;
    }
}