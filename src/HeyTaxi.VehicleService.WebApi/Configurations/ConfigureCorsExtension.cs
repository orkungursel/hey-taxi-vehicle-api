namespace HeyTaxi.VehicleService.WebApi.Configurations;

using Microsoft.AspNetCore.Cors.Infrastructure;

public static class ConfigureCorsExtension
{
    private static readonly string CorsPolicyName = "CorsPolicy";

    public static IServiceCollection ConfigureCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(CorsOptions);

        return services;
    }

    public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicyName);

        return app;
    }

    private static void CorsOptions(CorsOptions options)
    {
        options.AddPolicy(CorsPolicyName,
            builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .Build()
            );
    }
}
