using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Catalog.Application.Features;
using OnlineShop.Catalog.Application.Features.Customer.GetProducts;

namespace OnlineShop.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/onlineshop/catalog")]
    public sealed class CatalogCustomerController : ControllerBase
    {
        private readonly ISender _sender;

        public CatalogCustomerController(ISender sender)
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
            [FromQuery] string subcategory = "",
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
                subcategory,
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
            var response = await _sender.Send(new SeedCommand());

            return response.Value ? Ok() : BadRequest("Database already contains data");
        }

        //TODO
        //Jak narazie zawieszam feature podkategorii, w przyszłości trzeba zrobić oddzielną kolekcję przechowującą podkategorie
        //Dodawanie produktu z ceną z cennika, jeżeli nie znaleziono, to wtedy z ustaloną, po czym dodaję do cennika
        //Usuwanie produktu
        //Edytowanie produktu z integracją ceny w cenniku, jeżeli dodam promocję tu to i tam
        //Get dla admina (w formie tabeli, może zrobić to agregatem z cennikiem?)
        //Możliwość ustawienia jednostki podglądowej jako sztuka (dla customera)
    }
}