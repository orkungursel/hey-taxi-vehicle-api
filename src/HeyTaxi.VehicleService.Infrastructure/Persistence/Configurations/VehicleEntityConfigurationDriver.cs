using HeyTaxi.VehicleService.Domain.Entities;
using HeyTaxi.VehicleService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeyTaxi.VehicleService.Infrastructure.Persistence.Configurations;

internal class VehicleEntityConfigurationDriver : VehicleEntityConfigurationBase<Driver>
{
    public override void Configure(EntityTypeBuilder<Driver> builder)
    {
        base.Configure(builder);
        builder.ToTable("drivers", VehicleDbContext.DefaultSchema);
        builder.Property(x => x.DriverId).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
        builder.HasMany(x => x.Vehicles)
            .WithOne(x => x.Driver)
            .HasForeignKey(x => x.DriverId);
    }
}