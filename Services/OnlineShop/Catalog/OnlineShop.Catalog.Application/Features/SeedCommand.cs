using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features
{
    public record SeedCommand() : ICommand<bool>;

    internal sealed class SeedCommandHandler : ICommandHandler<SeedCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public SeedCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<bool>> Handle(SeedCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.Seed();

            return result;
        }
    }
}