using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HeyTaxi.VehicleService.Infrastructure.Persistence.Context;

internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<VehicleDbContext>
{
    public VehicleDbContext CreateDbContext(string[] args)
    {
        const string connectionString =
            "User ID=postgres;Password=postgres;Server=localhost;Port=5432;Database=postgres;Integrated Security=true;Pooling=true;";

        var builder = new DbContextOptionsBuilder<VehicleDbContext>()
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention();
        
        return new VehicleDbContext(builder.Options);
    }
}