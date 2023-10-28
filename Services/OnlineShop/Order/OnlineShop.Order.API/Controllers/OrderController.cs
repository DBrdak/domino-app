using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Order.Application.Features.Commands.CancelOrder;
using OnlineShop.Order.Application.Features.Queries.GetCustomerOrder;

namespace OnlineShop.Order.API.Controllers
{
    [ApiController]
    [Route("api/v1/onlineshop/order")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerOrder([FromQuery] string phoneNumber, [FromQuery] string orderId)
        {
            var result = await _mediator.Send(new GetCustomerOrderQuery(phoneNumber, orderId));

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpPut("cancel")]
        public async Task<IActionResult> CancelOrder([FromBody] CancelOrderCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}