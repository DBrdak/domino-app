using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddLineItem;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddPriceList;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddRetailPriceList;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.GetPriceLists;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.RemoveLineItem;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.RemovePriceList;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.UpdateLineItemPrice;

namespace OnlineShop.Catalog.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/onlineshop/pricelist")]
    public class PriceListAdminController : ControllerBase
    {
        private readonly ISender _sender;

        public PriceListAdminController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetPriceLists(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetPriceListsQuery(), cancellationToken);

            return Ok(result.Value);
        }

        [HttpPost("retail")]
        public async Task<IActionResult> CreateRetailPriceList(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new AddRetailPriceListCommand(), cancellationToken);

            return result.IsSuccess ?
                Ok() :
                BadRequest(result.Error);
        }

        [HttpPost("{contractorName}")]
        public async Task<IActionResult> CreateBusinessPriceList(
            [FromBody] AddBusinessPriceListCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ?
                Ok() :
                BadRequest(result.Error);
        }

        [HttpDelete("{priceListId}")]
        public async Task<IActionResult> RemovePriceList(
            [FromRoute] string priceListId,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new RemovePriceListCommand(priceListId), cancellationToken);

            return result.IsSuccess ?
                Ok() :
                BadRequest(result.Error);
        }

        [HttpPut("{priceListId}/add")]
        public async Task<IActionResult> CreateLineItem(
            [FromRoute] string priceListId,
            [FromBody] AddLineItemRequestValues request,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new AddLineItemCommand(priceListId, request.LineItemName, request.Price),
                cancellationToken);

            return result.IsSuccess ?
                Ok() :
                BadRequest(result.Error);
        }

        [HttpPut("{priceListId}/update")]
        public async Task<IActionResult> UpdateLineItemPrice(
            [FromRoute] string priceListId,
            [FromBody] UpdateLineItemPriceRequestValues request,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateLineItemPriceCommand(priceListId, request.NewPrice, request.LineItemName),
                cancellationToken);

            return result.IsSuccess ?
                Ok() :
                BadRequest(result.Error);
        }

        [HttpPut("{priceListId}/remove/{lineItemName}")]
        public async Task<IActionResult> RemoveLineItem(
            [FromRoute] string priceListId,
            [FromRoute] string lineItemName,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new RemoveLineItemCommand(priceListId, lineItemName),
                cancellationToken);

            return result.IsSuccess ?
                Ok() :
                BadRequest(result.Error);
        }
    }
}