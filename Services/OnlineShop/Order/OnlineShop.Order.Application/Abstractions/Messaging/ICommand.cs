using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Abstractions.Messaging
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