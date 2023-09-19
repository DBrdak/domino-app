using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shops.Application.Features.Queries;
using Shops.Application.Features.Queries.GetSalePoints;

namespace Shops.API.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/v1/shops")]
    public class ShopsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSalePoints(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetSalePointsQuery(), cancellationToken);

            return Ok(result.Value);
        }
    }
}