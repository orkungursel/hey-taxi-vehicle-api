using HashidsNet;
using HeyTaxi.VehicleService.Application.Exceptions;
using HeyTaxi.VehicleService.Application.Interfaces;
using HeyTaxi.VehicleService.Domain.Core.Repository;
using HeyTaxi.VehicleService.Domain.Entities;
using HeyTaxi.VehicleService.Domain.Specifications;
using MediatR;

namespace HeyTaxi.VehicleService.Application.Commands.DeleteVehicle;

public class DeleteVehicleCommandHandler : ICommandHandler<DeleteVehicleCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashids _hashids;

    public DeleteVehicleCommandHandler(IUnitOfWork unitOfWork, IHashids hashids)
    {
        _hashids = hashids;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicleId = _hashids.DecodeSingleLong(request.VehicleId);
        var driver = await _unitOfWork.Repository<Driver>().FirstOrDefaultAsync(DriverSpecifications.GetDriverByIdSpec(request.DriverId), cancellationToken);
        
        if (driver == null)
        {
            throw new DriverNotFoundException(request.DriverId);
        }

        var vehicle = await _unitOfWork.Repository<Vehicle>().FirstOrDefaultAsync(
            VehicleSpecifications.VehicleByIdWithDriverSpec(vehicleId),
            cancellationToken);
        
        if (vehicle == null)
        {
            throw new VehicleNotFoundException(vehicleId);
        }
        
        if (vehicle.DriverId != driver.Id)
        {
            throw new DeleteVehicleNoPermissionException(driver.Id, vehicle.DriverId);
        }

        _unitOfWork.Repository<Vehicle>().Delete(vehicle);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}