using FluentValidation;
using HeyTaxi.VehicleService.Domain.Enums;

namespace HeyTaxi.VehicleService.Application.Commands.AddVehicle;

public class AddVehicleRequestValidator : AbstractValidator<AddVehicleRequest>
{
    public AddVehicleRequestValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .NotEmpty();
        
        RuleFor(x => x.Plate)
            .MaximumLength(20)
            .NotEmpty();

        var validVehicleClassValues = Enum.GetNames(typeof(VehicleClass));
        RuleFor(x => x.Class)
            .IsEnumName(typeof(VehicleClass))
            .WithMessage(m => $"{m.Class} is not a valid vehicle class. Valid values are: {string.Join(", ", validVehicleClassValues)}");
        
        var validVehicleTypeValues = Enum.GetNames(typeof(VehicleType));
        RuleFor(x => x.Type)
            .IsEnumName(typeof(VehicleType))
            .WithMessage(m => $"{m.Type} is not a valid vehicle type. Valid values are: {string.Join(", ", validVehicleTypeValues)}");
        
        RuleFor(x => x.Seats)
            .GreaterThan(0)
            .LessThanOrEqualTo(20);
    }
}