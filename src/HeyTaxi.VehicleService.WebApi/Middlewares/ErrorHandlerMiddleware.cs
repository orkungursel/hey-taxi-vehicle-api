using System.Net;
using System.Text.Json;
using FluentValidation;
using HeyTaxi.VehicleService.WebApi.Models;

namespace HeyTaxi.VehicleService.WebApi.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly bool _isDevelopment;
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
        _isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = error switch
            {
                KeyNotFoundException _ => (int)HttpStatusCode.NotFound,
                ArgumentException _ => (int)HttpStatusCode.BadRequest,
                Grpc.Core.RpcException _ => (int)HttpStatusCode.ServiceUnavailable,
                ValidationException _ => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            if (_isDevelopment)
            {
                var result = JsonSerializer.Serialize(new ErrorModel(error.Message));

                await response.WriteAsync(result);
            }
            else
            {
                var result = JsonSerializer.Serialize(new ErrorModel(error.Message));
                await response.WriteAsync(result);
            }
        }
    }
}
