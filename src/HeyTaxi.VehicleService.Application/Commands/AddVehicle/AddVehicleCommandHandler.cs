using AutoMapper;
using HeyTaxi.VehicleService.Application.Interfaces;
using HeyTaxi.VehicleService.Application.Models.DTOs;
using HeyTaxi.VehicleService.Application.Services;
using HeyTaxi.VehicleService.Domain.Core.Repository;
using HeyTaxi.VehicleService.Domain.Entities;

namespace HeyTaxi.VehicleService.Application.Commands.AddVehicle;

public class AddVehicleCommandHandler : ICommandHandler<AddVehicleCommand, VehicleDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDriverService _driverService;
    private readonly IMapper _mapper;


    public AddVehicleCommandHandler(IUnitOfWork unitOfWork,
        IDriverService driverService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _driverService = driverService;
        _mapper = mapper;
    }

    public async Task<VehicleDTO> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
    {
        var driver = await _driverService.GetOrCreateDriverAsync(request.DriverId, cancellationToken);
        var vehicle = _mapper.Map<Vehicle>(request.Data);
        vehicle.Driver = driver;

        await _unitOfWork.Repository<Vehicle>().AddAsync(vehicle, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<VehicleDTO>(vehicle);
    }
}