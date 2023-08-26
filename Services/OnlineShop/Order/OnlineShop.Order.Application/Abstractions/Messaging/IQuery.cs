using MediatR;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Order.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}