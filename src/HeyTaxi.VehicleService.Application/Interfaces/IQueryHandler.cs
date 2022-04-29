using MediatR;

namespace HeyTaxi.VehicleService.Application.Interfaces;

public interface IQueryHandler<in TQuery, TResult> :
    IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{

}