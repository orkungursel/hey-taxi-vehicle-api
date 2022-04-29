using HeyTaxi.VehicleApi;
using HeyTaxi.VehicleService.Infrastructure.Clients;
using HeyTaxi.VehicleService.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HeyTaxi.VehicleService.Infrastructure.Configurations;

public static class ConfigureGrpcClients
{
    public static IServiceCollection AddGrpcClients(this IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var options = scope.ServiceProvider.GetRequiredService<IOptions<ExternalGrpcServicesOptions>>();

        var uri = options.Value.UserDetailsServiceUri;

        if (string.IsNullOrEmpty(uri))
        {
            throw new Exception("UserDetailsServiceUri is not configured");
        }

        services.AddGrpcClient<UserService.UserServiceClient>((o) =>
        {
            o.Address = new Uri(uri);
        });
        services.AddSingleton<UserServiceClient>();

        return services;
    }
}
