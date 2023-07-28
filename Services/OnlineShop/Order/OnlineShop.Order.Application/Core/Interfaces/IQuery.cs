using MediatR;

namespace OnlineShop.Order.Application.Core.Interfaces
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}