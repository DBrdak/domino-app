using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Catalog.Application.Features.Admin.GetProducts;

namespace OnlineShop.Catalog.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/onlineshop/catalog")]
    public class CatalogAdminController : ControllerBase
    {
        private readonly ISender _sender;

        public CatalogAdminController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(
            CancellationToken cancellationToken,
            [FromQuery] string searchPhrase = "")
        {
            var query = new GetProductsAdminQuery(searchPhrase);

            var response = await _sender.Send(query, cancellationToken);

            return Ok(response.Value);
        }
    }
}