using HeyTaxi.VehicleService.Domain.Entities;
using HeyTaxi.VehicleService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeyTaxi.VehicleService.Infrastructure.Persistence.Configurations;

internal class VehicleEntityConfigurationVehicle : VehicleEntityConfigurationBase<Vehicle>
{
    public override void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        base.Configure(builder);
        builder.ToTable("vehicles", VehicleDbContext.DefaultSchema);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.DriverId).IsRequired();
        builder.HasOne(x => x.Driver)
            .WithMany(d => d.Vehicles).OnDelete(DeleteBehavior.Cascade);
    }
}