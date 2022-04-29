using AutoMapper;
using HeyTaxi.VehicleService.Application.Interfaces;
using HeyTaxi.VehicleService.Application.Models.DTOs;
using HeyTaxi.VehicleService.Domain.Core.Repository;
using HeyTaxi.VehicleService.Domain.Entities;
using HeyTaxi.VehicleService.Domain.Specifications;

namespace HeyTaxi.VehicleService.Application.Queries.GetDriverVehicles;

public class GetDriverVehiclesQueryHandler : IQueryHandler<GetDriverVehiclesQuery, IEnumerable<VehicleDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDriverVehiclesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<VehicleDTO>> Handle(GetDriverVehiclesQuery request,
        CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.Repository<Driver>().FirstOrDefaultAsync(
            DriverSpecifications.GetDriverByIdVehiclesWithPaginationSpec(request.DriverId),
            cancellationToken);
        
        return _mapper.Map<IEnumerable<VehicleDTO>>(driver?.Vehicles);
    }
}