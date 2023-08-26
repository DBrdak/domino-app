using Microsoft.AspNetCore.Mvc;
using OnlineShop.ShoppingCart.API.Controllers.Requests;
using OnlineShop.ShoppingCart.API.Entities;
using OnlineShop.ShoppingCart.API.Repositories;

namespace OnlineShop.ShoppingCart.API.Controllers
{
    [ApiController]
    [Route("api/v1/onlineshop/shoppingcart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _repository;

        public ShoppingCartController(IShoppingCartRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{shoppingCartId}")]
        public async Task<IActionResult> GetShoppingCart(string shoppingCartId)
        {
            var shoppingCart = await _repository.GetShoppingCart(shoppingCartId);
            return Ok(shoppingCart);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShoppingCart(
            [FromBody] Entities.ShoppingCart shoppingCart)
        {
            var updatedShoppingCart = await _repository.UpdateShoppingCart(shoppingCart);
            return Ok(updatedShoppingCart);
        }

        [HttpDelete("{shoppingCartId}")]
        public async Task<IActionResult> DeleteShoppingCart(string shoppingCartId)
        {
            await _repository.DeleteShoppingCart(shoppingCartId);
            return Ok();
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] ShoppingCartCheckoutRequest request)
        {
            var result = await _repository.Checkout(request);

            return result.IsSuccess ?
                Ok(result.Value) :
                BadRequest(result.Error);
        }
    }
}