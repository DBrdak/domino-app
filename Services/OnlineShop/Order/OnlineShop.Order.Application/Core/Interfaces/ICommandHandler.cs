using MediatR;

namespace OnlineShop.Order.Application.Core.Interfaces
{
    public interface ICommandHandler<in TCommand, TResponse> :
        IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
    }
}