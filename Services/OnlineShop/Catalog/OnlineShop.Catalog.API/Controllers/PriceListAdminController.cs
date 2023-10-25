using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddBusinessPriceList;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddLineItem;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddRetailPriceList;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.DownloadPriceListAsExcel;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.GetPriceLists;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.RemoveLineItem;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.RemovePriceList;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.UpdateLineItemPrice;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.UploadPriceListAsExcel;

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
                new AddLineItemCommand(priceListId, request.LineItemName, request.NewPrice),
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
                new UpdateLineItemPriceCommand(request.LineItemName, request.NewPrice, priceListId),
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

        [HttpGet("file/{priceListId}")]
        public async Task<IActionResult> DownloadPriceListAsExcel(
            string priceListId,
            CancellationToken cancellationToken)
        {
            var query = new GetPriceListSpreadsheetQuery(priceListId);

            var response = await _sender.Send(query, cancellationToken);

            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }

            var stream = new MemoryStream();

            response.Value.Spreadsheet.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);
            
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{response.Value.FileName}.xlsx");
        }

        [HttpPost("file/{priceListId}")]
        public async Task<IActionResult> UploadPriceListAsExcel(
            string priceListId,
            [FromForm] IFormFile priceListFile,
            CancellationToken cancellationToken)
        {
            var command = new UploadPriceListSpreadsheetCommand(priceListId, priceListFile);

            var response = await _sender.Send(command, cancellationToken);

            return response.IsSuccess ?
                Ok() :
                BadRequest(response.Error);
        }
    }
}