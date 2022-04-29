using AutoMapper;
using HashidsNet;
using HeyTaxi.VehicleService.Application.Interfaces;
using HeyTaxi.VehicleService.Application.Models.DTOs;
using HeyTaxi.VehicleService.Domain.Core.Repository;
using HeyTaxi.VehicleService.Domain.Entities;
using HeyTaxi.VehicleService.Domain.Specifications;

namespace HeyTaxi.VehicleService.Application.Queries.GetDriverVehicleById;

public class GetDriverVehicleByIdQueryHandler: IQueryHandler<GetDriverVehicleByIdQuery, VehicleDTO?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashids _hashids;
    private readonly IMapper _mapper;

    public GetDriverVehicleByIdQueryHandler(IMapper mapper, IHashids hashids, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _hashids = hashids;
        _unitOfWork = unitOfWork;
    }

    public async Task<VehicleDTO?> Handle(GetDriverVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var vehicleId = _hashids.DecodeSingleLong(request.VehicleId);
        var driver = await _unitOfWork.Repository<Driver>().FirstOrDefaultAsync(
            DriverSpecifications.GetDriverByIdWithVehiclesSpec(request.DriverId),
            cancellationToken);

        if (driver == null || driver.DriverId != request.DriverId)
            return null;
        
        var vehicle = driver.Vehicles?.FirstOrDefault(v => v.Id == vehicleId);
        
        return vehicle == null ? null : _mapper.Map<VehicleDTO>(vehicle);
    }
}