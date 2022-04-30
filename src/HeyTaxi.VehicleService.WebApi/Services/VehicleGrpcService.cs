using Grpc.Core;
using HeyTaxi.VehicleService.Application.Commands.AddVehicle;
using HeyTaxi.VehicleService.Application.Queries.GetVehicleById;
using HeyTaxi.VehicleService.WebApi.Proto;
using MediatR;

namespace HeyTaxi.VehicleService.WebApi.Services;

public class VehicleGrpcService : Proto.VehicleService.VehicleServiceBase
{
    private readonly ILogger<VehicleGrpcService> _logger;
    private readonly IMediator _mediator;

    public VehicleGrpcService(ILogger<VehicleGrpcService> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public override async Task<GetVehicleResponse> GetVehicle(GetVehicleRequest request, ServerCallContext context)
    {
        var vehicleId = request.Id;

        if (string.IsNullOrEmpty(vehicleId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Vehicle id is required" ));
        };

        try
        {
            var vehicle = await _mediator.Send(new GetVehicleByIdQuery(vehicleId), context.CancellationToken);

            if (vehicle == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Vehicle not found"));
            }

            return new GetVehicleResponse
            {
                Id = vehicle?.Id,
                Name = vehicle?.Name,
                Plate = vehicle?.Plate,
                Class = vehicle?.Class,
                Type = vehicle?.Type,
                Seats = vehicle?.Seats ?? 0,
                Driver = new DriverDetailsResponse() {
                    Id = vehicle?.Driver?.Id,
                    Name = vehicle?.Driver?.Name,
                    Email = vehicle?.Driver?.Email,
                    Avatar = vehicle?.Driver?.Avatar
                }
            };
        }
        catch (HashidsNet.NoResultException)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Vehicle id is wrong"));
        }
        catch
        {

            throw new RpcException(new Status(StatusCode.Unknown, "An error occurred"));
        }
    }
}
