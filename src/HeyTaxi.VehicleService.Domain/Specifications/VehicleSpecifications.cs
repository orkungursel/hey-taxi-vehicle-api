using HeyTaxi.VehicleService.Domain.Core.Specifications;
using HeyTaxi.VehicleService.Domain.Entities;

namespace HeyTaxi.VehicleService.Domain.Specifications;

public class VehicleSpecifications
{
    public static BaseSpecification<Vehicle> VehiclesByDriverIdWithPaginationSpec(long driverId, int skip = 0, int take = 10)
    {
        var spec = new BaseSpecification<Vehicle>((x => x.DriverId == driverId));
        spec.ApplyPaging(skip, take);
        spec.ApplyOrderBy(x => x.Id);
        return spec;
    }

    public static BaseSpecification<Vehicle> VehicleByIdWithDriverSpec(long vehicleId)
    {
        var spec = new BaseSpecification<Vehicle>((x => x.Id == vehicleId));
        spec.AddInclude(x => x.Driver!);
        return spec;
    }
}