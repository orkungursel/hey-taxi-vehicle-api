using HeyTaxi.VehicleService.Domain.Core.Specifications;
using HeyTaxi.VehicleService.Domain.Entities;

namespace HeyTaxi.VehicleService.Domain.Specifications;

public class DriverSpecifications
{
    public static BaseSpecification<Driver> GetDriverByIdVehiclesWithPaginationSpec(string driverId,
        int skip = 0,
        int take = 10)
    {
        var spec = new BaseSpecification<Driver>((x => x.DriverId == driverId));
        spec.AddInclude(x => x.Vehicles!);
        spec.ApplyPaging(skip, take);
        spec.ApplyOrderBy(x => x.Id);
        return spec;
    }
    
    public static BaseSpecification<Driver> GetDriverByIdSpec(string driverId)
    {
        var spec = new BaseSpecification<Driver>(x =>
            x.DriverId == driverId);
        return spec;
    }
    
    public static BaseSpecification<Driver> GetDriverByIdWithVehiclesSpec(string driverId)
    {
        var spec = new BaseSpecification<Driver>(x =>
            x.DriverId == driverId);
        spec.AddInclude(x => x.Vehicles!);
        return spec;
    }
}