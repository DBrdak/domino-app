using Microsoft.AspNetCore.Mvc;
using OnlineShop.Catalog.API.Entities;
using OnlineShop.Catalog.API.Models;
using OnlineShop.Catalog.API.Repositories;

namespace OnlineShop.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/onlineshop/catalog")]
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
            [FromQuery] int pageSize = 9,
            [FromQuery] string searchPhrase = null,
            [FromQuery] string subcategory = null,
            [FromQuery] decimal minPrice = 0,
            [FromQuery] decimal maxPrice = decimal.MaxValue,
            [FromQuery] bool? isAvailable = null,
            [FromQuery] bool? isDiscounted = null,
            [FromQuery] bool pcsMode = false)
        {
            return await _repository.GetProductsAsync(
                page, sortOrder, sortBy, pageSize, category, subcategory, searchPhrase,
                minPrice, maxPrice, isAvailable, isDiscounted, pcsMode);
        }

        //TODO
        //Dodawanie produktu z ceną z cennika, jeżeli nie znaleziono, to wtedy z ustaloną, po czym dodaję do cennika
        //Usuwanie produktu
        //Edytowanie produktu z integracją ceny w cenniku, jeżeli dodam promocję tu to i tam
        //Get dla admina (w formie tabeli, może zrobić to agregatem z cennikiem?)
        //Możliwość ustawienia jednostki podglądowej jako sztuka (dla customera)
    }
}