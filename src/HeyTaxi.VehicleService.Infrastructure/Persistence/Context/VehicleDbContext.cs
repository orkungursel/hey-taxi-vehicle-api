using System.Reflection;
using HeyTaxi.VehicleService.Domain.Core.Models;
using HeyTaxi.VehicleService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618

namespace HeyTaxi.VehicleService.Infrastructure.Persistence.Context;

public class VehicleDbContext : DbContext
{
    public const string DefaultSchema = "public";

    protected VehicleDbContext()
    {
    }

    public VehicleDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Driver> Drivers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
                typeof(BaseEntity).IsAssignableFrom(i.GenericTypeArguments[0]))
        );

        modelBuilder.UseSerialColumns();

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
            if (entry.Entity is AuditableEntity entity)
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedOn = DateTime.UtcNow;
                        entity.LastModifiedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entity.LastModifiedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                    case EntityState.Deleted:
                    default:
                        continue;
                }
    }
}