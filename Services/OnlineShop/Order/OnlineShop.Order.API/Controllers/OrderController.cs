using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Order.Application.Features.Commands.CancelOrder;
using OnlineShop.Order.Application.Features.Queries.GetCustomerOrder;

namespace OnlineShop.Order.API.Controllers
{
    [Route("api/v1/onlineshop/order")]
    public class OrderController : BaseOrderController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerOrder([FromBody] GetCustomerOrderQuery query)
        {
            var result = await _mediator.Send(query);

            return HandleResult(result);
        }

        [HttpPut("cancel")]
        public async Task<IActionResult> CancelOrder([FromBody] CancelOrderCommand command)
        {
            var result = await _mediator.Send(command);

            return HandleResult(result);
        }

        //TODO
        // Get dla admina - pobieranie wszystkich orderów
        // Delete dla admina - usuwanie wszystkich orderów
        // Put dla admina - zmienianie statusów ordera/ów
    }
}