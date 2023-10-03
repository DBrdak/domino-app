﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shops.Application.Features.Queries;
using Shops.Application.Features.Queries.GetDeliveryPoints;

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
        public async Task<IActionResult> GetDeliveryPoints(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDeliveryPointsQuery(), cancellationToken);

            return Ok(result.Value);
        }
    }
}