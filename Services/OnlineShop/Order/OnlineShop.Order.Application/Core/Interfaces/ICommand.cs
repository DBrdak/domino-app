using MediatR;

namespace OnlineShop.Order.Application.Core.Interfaces
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}