using Microsoft.AspNetCore.Mvc;
using Order.Application.Contracts;

namespace OnlineShop.Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }

        //TODO
        // Get dla jednego zamówienia jako klient
        // Post zamówienia jako klient
        // Delete zamówienia jako klient
    }
}