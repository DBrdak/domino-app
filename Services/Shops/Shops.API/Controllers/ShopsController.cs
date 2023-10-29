using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shops.Application.Features.Queries.GetDeliveryPoints;

namespace Shops.API.Controllers
{
    [ApiController]
    [Route("api/v1/shops")]
    public class ShopsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("delivery-points")]
        public async Task<IActionResult> GetDeliveryPoints(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDeliveryPointsQuery(), cancellationToken);

            return Ok(result.Value);
        }
    }
}