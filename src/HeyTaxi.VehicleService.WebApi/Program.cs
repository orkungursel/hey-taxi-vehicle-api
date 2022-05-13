using HeyTaxi.VehicleService.Application;
using HeyTaxi.VehicleService.Infrastructure;
using HeyTaxi.VehicleService.WebApi.Configurations;
using HeyTaxi.VehicleService.WebApi.Endpoints;
using HeyTaxi.VehicleService.WebApi.Middlewares;
using HeyTaxi.VehicleService.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel();

builder.Services.ConfigureJsonOptions();
builder.Services.ConfigureJwtOptions(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureCorsPolicy();

builder.Services.AddGrpc();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseAppSwagger();
}

app.UseCorsPolicy();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => AppDomain.CurrentDomain.FriendlyName);
app.MapHealthChecks("/health");
app.MapVehicleEndpoints();
app.MapGrpcService<VehicleGrpcService>().RequireHost("*:50052"); ;

app.Run();
