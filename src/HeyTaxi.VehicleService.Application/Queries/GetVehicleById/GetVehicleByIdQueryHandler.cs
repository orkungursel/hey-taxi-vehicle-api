using HeyTaxi.VehicleService.Domain.Entities;
using HeyTaxi.VehicleService.Application.Interfaces;
using HeyTaxi.VehicleService.Application.Models.DTOs;
using HeyTaxi.VehicleService.Domain.Core.Repository;
using HashidsNet;
using HeyTaxi.VehicleService.Domain.Specifications;
using AutoMapper;

namespace HeyTaxi.VehicleService.Application.Queries.GetVehicleById;

public class GetVehicleByIdHandler : IQueryHandler<GetVehicleByIdQuery, VehicleWithDriverDTO?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashids _hashIds;
    private readonly IMapper _mapper;

    public GetVehicleByIdHandler(IUnitOfWork unitOfWork, IHashids hashIds, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _hashIds = hashIds;
        _mapper = mapper;
    }

    public async Task<VehicleWithDriverDTO?> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var vehicleId = _hashIds.DecodeSingleLong(request.VehicleId);
        var vehicle = await _unitOfWork.Repository<Vehicle>().FirstOrDefaultAsync(VehicleSpecifications.VehicleByIdWithDriverSpec(vehicleId), cancellationToken);
        return vehicle is null ? null : _mapper.Map<VehicleWithDriverDTO>(vehicle);
    }
}
