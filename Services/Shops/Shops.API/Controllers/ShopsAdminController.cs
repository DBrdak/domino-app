using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shops.Application.Features.Commands.AddShop;
using Shops.Application.Features.Commands.DeleteShop;
using Shops.Application.Features.Commands.UpdateShop;
using Shops.Application.Features.Queries.GetShops;

namespace Shops.API.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/v1/shops")]
    public class ShopsAdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopsAdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetShops(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetShopsQuery(), cancellationToken);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddShop([FromBody] AddShopCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                Ok(result.Value) :
                BadRequest(result.Error);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShopData(
            [FromBody] UpdateShopCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                Ok(result.Value) :
                BadRequest(result.Error);
        }

        [HttpDelete("{shopId}")]
        public async Task<IActionResult> DeleteShop(string shopId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteShopCommand(shopId), cancellationToken);

            return result.IsSuccess ?
                Ok() :
                BadRequest(result.Error);
        }
    }
}