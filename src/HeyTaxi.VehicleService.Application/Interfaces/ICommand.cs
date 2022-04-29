using MediatR;

namespace HeyTaxi.VehicleService.Application.Interfaces;

public interface ICommand<out TResult> : IRequest<TResult>
{
}