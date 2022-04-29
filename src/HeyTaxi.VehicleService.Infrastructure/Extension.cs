using HeyTaxi.VehicleApi;
using HeyTaxi.VehicleService.Application.Services;
using HeyTaxi.VehicleService.Domain.Core.Repository;
using HeyTaxi.VehicleService.Infrastructure.Clients;
using HeyTaxi.VehicleService.Infrastructure.Configurations;
using HeyTaxi.VehicleService.Infrastructure.Persistence.Context;
using HeyTaxi.VehicleService.Infrastructure.Repositories;
using HeyTaxi.VehicleService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeyTaxi.VehicleService.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddVehicleDbContext(configuration)
            .AddHealthChecks()
            .AddServices();

        services.ConfigureExternalGrpcServiceOptions(configuration);
        services.AddGrpcClients();

        // services.AddSeedWork(configuration);

        return services;
    }

   public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDriverService, DriverService>();

        return services;
    }
    
   public static IServiceCollection AddVehicleDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<VehicleDbContext>(o =>
        {
            o.UseNpgsql(connectionString);
            o.UseSnakeCaseNamingConvention();
        });

        return services;
    }

   public static IServiceCollection AddHealthChecks(this IServiceCollection services)
   {

       HealthCheckServiceCollectionExtensions.AddHealthChecks(services)
           .AddDbContextCheck<VehicleDbContext>(tags: new[] { "db", "all" })
           .AddCheck<HealthCheckDriverService>("DriverService", tags: new[] { "services", "grpc", "all" });

        return services;
    }

   public static IServiceCollection AddSeedWork(this IServiceCollection services,
        IConfiguration configuration)
    {
        var vehicleDbContext = services.BuildServiceProvider().GetRequiredService<VehicleDbContext>();
        var seeder = new SeedWork(vehicleDbContext);

        seeder.SeedAsync().GetAwaiter().GetResult();
        
        return services;
    }
    
}