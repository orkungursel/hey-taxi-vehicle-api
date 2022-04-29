using HeyTaxi.VehicleService.WebApi.Options;

namespace HeyTaxi.VehicleService.WebApi.Configurations;

public static class ConfigureJwtOptionsExtension
{
    public static IServiceCollection ConfigureJwtOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Section));
        
        return services;
    }
}