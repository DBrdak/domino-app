using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain;
using OnlineShop.Catalog.Domain.Common;

namespace OnlineShop.Catalog.Application.Features.Admin.UpdateProduct
{
    public sealed record UpdateProductCommand(UpdateValues NewValues, IFormFile? PhotoFile) : ICommand<Product>;
}