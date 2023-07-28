using MediatR;

namespace OnlineShop.Order.Application.Core.Interfaces
{
    public interface IQueryHandler<in TQuery, TResponse> :
        IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
    }
}