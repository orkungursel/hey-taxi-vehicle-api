using MediatR;

namespace HeyTaxi.VehicleService.Application.Interfaces;

public interface ICommandHandler<in TCommand, TResult> :
    IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
}