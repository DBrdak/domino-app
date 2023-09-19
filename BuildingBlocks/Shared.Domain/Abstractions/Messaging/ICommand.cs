using MediatR;
using Shared.Domain.ResponseTypes;

namespace Shared.Domain.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand
    {
    }

    public interface IBaseCommand
    {
    }
}