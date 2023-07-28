using Microsoft.AspNetCore.Mvc;
using OnlineShop.ShoppingCart.API.Repositories;
using System.Net;
using OnlineShop.ShoppingCart.API.Entities;

namespace OnlineShop.ShoppingCart.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _repository;

        public ShoppingCartController(IShoppingCartRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{shoppingCartId}")]
        public async Task<ActionResult<Entities.ShoppingCart>> GetShoppingCart(string shoppingCartId)
        {
            var shoppingCart = await _repository.GetShoppingCart(shoppingCartId);
            return Ok(shoppingCart);
        }

        [HttpPost]
        public async Task<ActionResult<Entities.ShoppingCart>> UpdateShoppingCart(
            [FromBody] Entities.ShoppingCart shoppingCart)
        {
            var updatedShoppingCart = await _repository.UpdateShoppingCart(shoppingCart);
            return Ok(updatedShoppingCart);
        }

        [HttpDelete("{shoppingCartId}")]
        public async Task<IActionResult> DeleteBasket(string shoppingCartId)
        {
            await _repository.DeleteShoppingCart(shoppingCartId);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Checkout([FromBody] ShoppingCartCheckout shoppingCartCheckout)
        {
            var result = await _repository.Checkout(shoppingCartCheckout);

            return result != null ?
                Ok(result) :
                BadRequest();
        }
    }
}