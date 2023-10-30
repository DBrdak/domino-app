using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Catalog.Application.Features.Admin.Products.Commands.AddProduct;
using OnlineShop.Catalog.Application.Features.Admin.Products.Commands.DeleteProduct;
using OnlineShop.Catalog.Application.Features.Admin.Products.Commands.UpdateProduct;
using OnlineShop.Catalog.Application.Features.Admin.Products.Queries.GetProducts;
using OnlineShop.Catalog.Domain.Products;

namespace OnlineShop.Catalog.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/onlineshop/product")]
    public class ProductAdminController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductAdminController(ISender sender)
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

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(
            [FromForm] UpdateValues newValues,
            CancellationToken cancellationToken,
            [FromForm] IFormFile photo = null)
        {
            var command = new UpdateProductCommand(newValues, photo);

            var response = await _sender.Send(command, cancellationToken);

            return response.IsSuccess ?
                    Ok(response.Value) :
                    BadRequest(response.Error);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(
            [FromForm] CreateValues values,
            [FromForm] IFormFile photo,
            CancellationToken cancellationToken)
        {
            var command = new AddProductCommand(values, photo);

            var response = await _sender.Send(command, cancellationToken);

            return response.IsSuccess ?
                    Ok(response.Value) :
                    BadRequest(response.Error);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(
            [FromRoute] string productId,
            CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand(productId);

            await _sender.Send(command, cancellationToken);

            return Ok();
        }
    }
}