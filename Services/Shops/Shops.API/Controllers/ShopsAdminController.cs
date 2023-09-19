using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}