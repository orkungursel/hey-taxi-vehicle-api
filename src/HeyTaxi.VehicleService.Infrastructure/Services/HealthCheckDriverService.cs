using Grpc.Core;
using HeyTaxi.VehicleApi;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using static HeyTaxi.VehicleApi.UserService;

namespace HeyTaxi.VehicleService.Infrastructure.Services;

public class HealthCheckDriverService : IHealthCheck
{
    private readonly UserServiceClient _userServiceClient;
    
    public HealthCheckDriverService(UserServiceClient userServiceClient)
    {
        _userServiceClient = userServiceClient;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        context.Registration.Timeout = TimeSpan.FromSeconds(2);
        context.Registration.FailureStatus = HealthStatus.Unhealthy;
        
        try
        {
            GetUserInfoRequest request = new()
            {
                UserIds = { "" }
            };
            
            await _userServiceClient.GetUserInfoAsync(request,
                Metadata.Empty,
                DateTime.Now.Add(context.Registration.Timeout).ToUniversalTime(),
                cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return HealthCheckResult.Unhealthy();
        }

        return HealthCheckResult.Healthy();
    }
}