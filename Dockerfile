FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 50052

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/HeyTaxi.VehicleService.WebApi/HeyTaxi.VehicleService.WebApi.csproj", "HeyTaxi.VehicleService.WebApi/"]
COPY ["src/HeyTaxi.VehicleService.Infrastructure/HeyTaxi.VehicleService.Infrastructure.csproj", "HeyTaxi.VehicleService.Infrastructure/"]
COPY ["src/HeyTaxi.VehicleService.Application/HeyTaxi.VehicleService.Application.csproj", "HeyTaxi.VehicleService.Application/"]
COPY ["src/HeyTaxi.VehicleService.Domain/HeyTaxi.VehicleService.Domain.csproj", "HeyTaxi.VehicleService.Domain/"]
RUN dotnet restore "HeyTaxi.VehicleService.WebApi/HeyTaxi.VehicleService.WebApi.csproj"

COPY src/ .
WORKDIR "/src/HeyTaxi.VehicleService.WebApi"
RUN dotnet build "HeyTaxi.VehicleService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HeyTaxi.VehicleService.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HeyTaxi.VehicleService.WebApi.dll"]
