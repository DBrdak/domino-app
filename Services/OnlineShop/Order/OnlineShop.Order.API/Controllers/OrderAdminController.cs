using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Order.Application.Features.Commands.UpdateOrder;
using OnlineShop.Order.Application.Features.Queries.GetOrders;
using OnlineShop.Order.Application.Features.Queries.GetOrdersAsPdf;
using Shared.Domain.Date;

namespace OnlineShop.Order.API.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/v1/onlineshop/order")]
    public class OrderAdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderAdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetOrdersQuery(), cancellationToken);

            return result.IsSuccess ?
                Ok(result.Value) :
                NotFound(result.Error);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(
            [FromBody] UpdateOrderCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                Ok() :
                BadRequest(result.Error);
        }


        [HttpGet("pdf")]
        public async Task<IActionResult> GetOrderPdf([FromQuery]string[] ordersId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetOrdersAsPdfQuery(ordersId), cancellationToken);

            return result.IsSuccess ?
                File(result.Value, "application/pdf", $"Zamowienia-{DateTimeService.UtcNow}.pdf") :
                BadRequest(result.Error);
        }
    }
}