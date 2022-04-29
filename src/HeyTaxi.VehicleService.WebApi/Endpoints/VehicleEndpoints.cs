using HeyTaxi.VehicleService.Application.Commands.AddVehicle;
using HeyTaxi.VehicleService.Application.Commands.DeleteVehicle;
using HeyTaxi.VehicleService.Application.Queries.Foo;
using HeyTaxi.VehicleService.Application.Queries.GetDriverVehicleById;
using HeyTaxi.VehicleService.Application.Queries.GetDriverVehicles;
using HeyTaxi.VehicleService.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HeyTaxi.VehicleService.WebApi.Endpoints;

public static class VehicleEndpoints
{
    public static void MapVehicleEndpoints(this WebApplication app)
    {
        app.MapGet("api/v1/vehicle/driver", GetDriver)
            .RequireAuthorization()
            .WithName("GetDriver");

        app.MapGet("api/v1/vehicle", Index)
            .RequireAuthorization()
            .WithName("GetVehicles");
        
        app.MapGet("api/v1/vehicle/{id}", Find)
            .RequireAuthorization()
            .WithName("GetVehicleById");
        
        app.MapPost("api/v1/vehicle", Create)
            .RequireAuthorization()
            .WithValidator<AddVehicleRequest>()
            .WithName("AddVehicle");
        
        app.MapDelete("api/v1/vehicle/{id}", Delete)
            .RequireAuthorization()
            .WithName("DeleteVehicle");
    }

    private static async Task<IResult> GetDriver(
        HttpContext context,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var userId = context.GetUserId();
        var result = await mediator.Send(
            new FooQuery(userId), cancellationToken);
        return Results.Ok(result);
    }
    
    private static async Task<IResult> Index(
        HttpContext context,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var userId = context.GetUserId();
        var result = await mediator.Send(
            new GetDriverVehiclesQuery(userId), cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> Find(string id, 
        HttpContext context,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var userId = context.GetUserId();
        var result = await mediator.Send(
            new GetDriverVehicleByIdQuery(userId, id), cancellationToken);

        return result != null ? Results.Ok(result) : Results.NotFound();
    }

    private static async Task<IResult> Create(AddVehicleRequest data, 
        HttpContext context,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var userId = context.GetUserId();
        var vehicle = await mediator.Send(new AddVehicleCommand(userId, data), cancellationToken);
        return Results.CreatedAtRoute("GetVehicleById", new { id = vehicle.Id }, vehicle);
    }

    private static async Task<IResult> Delete(string id, 
        HttpContext context,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var userId = context.GetUserId();
        await mediator.Send(new DeleteVehicleCommand(id, userId), cancellationToken);
        return Results.Ok();
    }
}