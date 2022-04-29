using MediatR;

namespace HeyTaxi.VehicleService.Application.Interfaces;

public interface IQuery<out TResult> : IRequest<TResult>
{
}