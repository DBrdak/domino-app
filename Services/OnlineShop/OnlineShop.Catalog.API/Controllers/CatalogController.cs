using Microsoft.AspNetCore.Mvc;
using OnlineShop.Catalog.API.CustomTypes;
using OnlineShop.Catalog.API.Entities;
using OnlineShop.Catalog.API.Repositories;

namespace OnlineShop.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public CatalogController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{category}")]
        public async Task<ActionResult<PagedList<Product>>> GetProducts(
            [FromRoute] string category,
            [FromQuery] int page = 1,
            [FromQuery] string sortOrder = "asc",
            [FromQuery] string sortBy = "Name",
            [FromQuery] int pageSize = 9)
        {
            return await _repository.GetProductsAsync(page, sortOrder, sortBy, pageSize, category);
        }
    }
}