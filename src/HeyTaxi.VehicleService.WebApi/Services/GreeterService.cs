using Grpc.Core;
using HeyTaxi.VehicleService.WebApi.Proto;

namespace HeyTaxi.VehicleService.WebApi.Services;

public class VehicleGrpcService : Proto.VehicleService.VehicleServiceBase
{
    private readonly ILogger<VehicleGrpcService> _logger;
    public VehicleGrpcService(ILogger<VehicleGrpcService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
