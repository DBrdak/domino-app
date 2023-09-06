using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Order.Application.Features.Commands.CancelOrder;
using OnlineShop.Order.Application.Features.Commands.UpdateOrder;
using OnlineShop.Order.Application.Features.Queries.GetCustomerOrder;
using OnlineShop.Order.Application.Features.Queries.GetOrders;

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

        [HttpGet]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetOrdersQuery(), cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(
            [FromBody] UpdateOrderCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return result.IsSuccess ? Ok() : BadRequest();
        }

        //TODO
        // Zapobieganie powielaniu zamówień
        // Get dla admina - pobieranie wszystkich orderów
        // Put dla admina - zmienianie statusów ordera/ów
    }
}