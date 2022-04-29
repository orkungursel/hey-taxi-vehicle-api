using Bogus;
using Bogus.Extensions.UnitedKingdom;
using HeyTaxi.VehicleService.Domain.Entities;
using HeyTaxi.VehicleService.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HeyTaxi.VehicleService.Infrastructure.Persistence.Context;

internal class SeedWork
{
    private readonly VehicleDbContext _context;

    public SeedWork(VehicleDbContext context)
    {
        _context = context;

        context.Database.Migrate();
    }

    private static List<Driver> GenerateDrivers()
    {
        return new Faker<Driver>("en")
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Name, f => f.Person.FullName)
            .RuleFor(x => x.Avatar, f => f.Person.Avatar)
            .RuleFor(x => x.DriverId, f => f.Random.String2(10))
            .Generate(20);
    }

    private static IEnumerable<Vehicle> GenerateVehicles(List<Driver> drivers)
    {
        return new Faker<Vehicle>("en")
            .RuleFor(x => x.Driver, f => f.PickRandom(drivers))
            .RuleFor(x => x.CreatedOn, f => f.Date.Past())
            .RuleFor(x => x.Name, f => f.Vehicle.Manufacturer())
            .RuleFor(x => x.Plate,
                f => f.Vehicle.GbRegistrationPlate(DateTime.UtcNow,
                    DateTime.UtcNow.Add(TimeSpan.FromDays(356))))
            .RuleFor(x => x.Type, f => f.PickRandom<VehicleType>())
            .RuleFor(x => x.Class, f => f.PickRandom<VehicleClass>())
            .RuleFor(x => x.Seats, f => f.Random.Number(1, 10))
            .Generate(100);
    }

    public async Task SeedAsync()
    {
        var drivers = GenerateDrivers();
        await _context.Drivers.AddRangeAsync(drivers);

        var vehicles = GenerateVehicles(drivers);
        await _context.Vehicles.AddRangeAsync(vehicles);

        await _context.SaveChangesAsync();
    }
}