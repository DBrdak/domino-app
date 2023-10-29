using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Catalog.Application.Features.Customer.Queries.GetProducts;

namespace OnlineShop.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/onlineshop/product")]
    public sealed class ProductCustomerController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductCustomerController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> GetProducts(
            [FromRoute] string category,
            CancellationToken cancellationToken,
            [FromQuery] int page = 1,
            [FromQuery] string sortOrder = "asc",
            [FromQuery] string sortBy = "name",
            [FromQuery] int pageSize = 12,
            [FromQuery] string searchPhrase = "",
            [FromQuery] decimal minPrice = 0,
            [FromQuery] decimal maxPrice = 0,
            [FromQuery] bool isAvailable = false,
            [FromQuery] bool isDiscounted = false)
        {
            var query = new GetProductsQuery(
                category,
                page,
                sortOrder,
                sortBy,
                pageSize,
                searchPhrase,
                minPrice,
                maxPrice,
                isAvailable,
                isDiscounted);

            var response = await _sender.Send(query, cancellationToken);

            return Ok(response.Value);
        }

        [HttpPost("seed")]
        [EndpointDescription("Development endpoint")]
        public async Task<IActionResult> Seed()
        {
            //var response = await _sender.Send(new SeedCommand());

            //return response.Value ? Ok() : BadRequest("Database already contains data");
            return StatusCode(405);
        }

        //TODO
        //Jak narazie zawieszam feature podkategorii, w przyszłości trzeba zrobić oddzielną kolekcję przechowującą podkategorie
    }
}