using HeyTaxi.VehicleService.Application;
using HeyTaxi.VehicleService.Infrastructure;
using HeyTaxi.VehicleService.WebApi.Configurations;
using HeyTaxi.VehicleService.WebApi.Endpoints;
using HeyTaxi.VehicleService.WebApi.Middlewares;
using HeyTaxi.VehicleService.WebApi.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureJsonOptions();
builder.Services.ConfigureJwtOptions(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.AddSwagger();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddGrpc();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080, o => o.Protocols = HttpProtocols.Http1AndHttp2);
    options.ListenAnyIP(50052, o => o.Protocols = HttpProtocols.Http2);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseAppSwagger();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => AppDomain.CurrentDomain.FriendlyName);
app.MapHealthChecks("/health");
app.MapVehicleEndpoints();

app.MapGrpcService<VehicleGrpcService>()
    .RequireHost("*:50052"); ;

app.Run();
