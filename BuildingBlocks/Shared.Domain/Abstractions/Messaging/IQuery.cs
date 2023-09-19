using MediatR;
using Shared.Domain.ResponseTypes;

namespace Shared.Domain.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}