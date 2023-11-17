using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Infrastructure.Repositories
{
    public sealed class PriceListRepository : IPriceListRepository
    {
        private readonly CatalogContext _context;
        private readonly ILogger<PriceListRepository> _logger;

        public PriceListRepository(CatalogContext context, ILogger<PriceListRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<PriceList>> GetPriceListsAsync(CancellationToken cancellationToken)
        {
            var priceListsCursor = await _context.PriceLists.FindAsync(_ => true, null, cancellationToken);

            var priceLists = await priceListsCursor.ToListAsync(cancellationToken);

            var isRetailPriceListExist = priceLists.Any(pl => pl.Contractor == Contractor.Retail);

            if (!isRetailPriceListExist)
            {
                await AddPriceList(PriceList.CreateRetail("Cennik detaliczny mięsa", Category.Meat), cancellationToken);
                await AddPriceList(PriceList.CreateRetail("Cennik detaliczny wędlin", Category.Sausage), cancellationToken);
                await GetPriceListsAsync(cancellationToken);
            }

            return priceLists;
        }

        public async Task<PriceList?> GetRetailPriceList(Category category, CancellationToken cancellationToken)
        {
            var retailPriceListCursor = await _context.PriceLists.FindAsync(
                pl => pl.Contractor.Name == Contractor.Retail.Name && pl.Category.Value == category.Value,
                null, cancellationToken);

            return await retailPriceListCursor.SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<PriceList?> GetRetailPriceList(string lineItemName, CancellationToken cancellationToken)
        {
            var retailPriceListCursor = await _context.PriceLists.FindAsync(
                pl => pl.Contractor.Name == Contractor.Retail.Name && pl.LineItems.Any(li => li.Name == lineItemName),
                null, cancellationToken);

            return await retailPriceListCursor.SingleOrDefaultAsync(cancellationToken);
        }

        public async Task AddPriceList(PriceList priceList, CancellationToken cancellationToken)
        {
            if (ContractorDuplicatesExists(priceList.Contractor, priceList.Category, cancellationToken))
            {
                throw new ApplicationException($"Price list for contractor {priceList.Contractor.Name} with category {priceList.Category.Value} already exists");
            }

            await _context.PriceLists.InsertOneAsync(priceList, null, cancellationToken);
        }

        public async Task<bool> RemovePriceList(string priceListId, CancellationToken cancellationToken)
        {
            var priceList = await GetPriceList(priceListId, cancellationToken);

            if (priceList is null)
            {
                return false;
            }

            if (priceList.Contractor == Contractor.Retail)
            {
                return false;
            }

            //TODO Check if contractor is null

            var result = await _context.PriceLists.DeleteOneAsync(pl => pl.Id == priceList.Id, null, cancellationToken);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<PriceList?> RemoveLineItem(string priceListId, string lineItemName, CancellationToken cancellationToken)
        {
            var priceList = await GetPriceList(priceListId, cancellationToken);

            if (priceList is null)
            {
                return null;
            }

            priceList.DeleteLineItem(lineItemName);

            var result = await _context.PriceLists.ReplaceOneAsync(
                pl => pl.Id == priceListId,
                priceList, new ReplaceOptions(), cancellationToken);

            if (!result.IsAcknowledged && !(result.ModifiedCount > 0))
            {
                return null;
            }

            return priceList;
        }

        public async Task<PriceList?> UpdateLineItemPrice(string priceListId, string lineItemName, Money newPrice, CancellationToken cancellationToken)
        {
            var priceList = await GetPriceList(priceListId, cancellationToken);

            if (priceList is null)
            {
                return null;
            }

            priceList.UpdateLineItemPrice(lineItemName, newPrice);

            var result = await _context.PriceLists.ReplaceOneAsync(
                pl => pl.Id == priceListId,
                priceList, new ReplaceOptions(), cancellationToken);

            if (!result.IsAcknowledged && !(result.ModifiedCount > 0))
            {
                return null;
            }

            return priceList;
        }

        public async Task<bool> AddLineItem(string priceListId, LineItem lineItem, CancellationToken cancellationToken)
        { 
            var priceList = await GetPriceList(priceListId, cancellationToken);

            if (priceList == null)
            {
                return false;
            }

            var isValid = PriceListslLineItemDuplicatesNotExists(lineItem.Name, priceList.Contractor, cancellationToken);

            if (!isValid)
            {
                return false;
            }

            priceList.AddLineItem(lineItem);

            var result = await _context.PriceLists.ReplaceOneAsync(
                pl => pl.Id == priceListId,
                priceList, new ReplaceOptions(), cancellationToken);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> UploadPriceListFile(string priceListId, IFormFile priceListFile, CancellationToken cancellationToken)
        {
            IXLWorkbook workbook;

            try
            {
                await using var stream = priceListFile.OpenReadStream();
                workbook = new XLWorkbook(stream);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to open XLSX file during upload. Price list ID: {priceListId}, file: {priceListFile}");
                return false;
            }

            var isWorksheetValid = workbook.Worksheets.Count(ws => ws.Name == "Cennik") == 1;

            if (!isWorksheetValid)
            {
                return false;
            }

            workbook.TryGetWorksheet("Cennik", out var worksheet);

            if (worksheet is null)
            {
                return false;
            }

            var lineItems = RetriveLineItemsNames(worksheet);
            if (lineItems is null)
            {
                return false;
            }
            
            var results = lineItems.Select(li => AddLineItem(priceListId, li, cancellationToken).Result).ToList();
            
            return results.All(r => r);
        }

        private List<LineItem>? RetriveLineItemsNames(IXLWorksheet worksheet)
        {
            if (string.IsNullOrWhiteSpace(worksheet.Cell(1, 1).GetText()) ||
                string.IsNullOrWhiteSpace(worksheet.Cell(1, 2).GetText()))
            {
                return null;
            }

            var names = new List<string>();
            var prices = new List<Money>();
            var lineItems = new List<LineItem>();
            var row = 2;
            var isCellEmpty = worksheet.Cell(2,1).IsEmpty();

            while (!isCellEmpty)
            {
                try
                {
                    names.Add(worksheet.Cell(row, 1).GetText());
                    var moneyString = worksheet.Cell(row, 2).GetText();
                    prices.Add(Money.FromString(moneyString));
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "{Exception} occurred: {Message}", e.GetType(), e.Message);
                    return null;
                }

                row++;
                isCellEmpty = worksheet.Cell(row, 1).IsEmpty() || worksheet.Cell(row, 2).IsEmpty();
            }

            for (int i = 0; i < names.Count && i < prices.Count; i++)
            {
                lineItems.Add(new (names[i], prices[i]));
            }

            return lineItems;
        }

        public async Task<PriceList?> AggregateLineItemWithProduct(
            string productId,
            string lineItemName,
            CancellationToken cancellationToken)
        {
            var priceList = await GetRetailPriceList(lineItemName, cancellationToken);

            if (priceList is null)
            {
                return null;
            }

            priceList.AggregateLineItemWithProduct(lineItemName, productId);

            return priceList;
        }

        public async Task<bool> SplitLineItemFromProduct(string productId, Category productCategory, CancellationToken cancellationToken)
        {
            var priceList = await GetRetailPriceList(productCategory, cancellationToken);

            if (priceList is null)
            {
                return false;
            }

            var isExist = priceList.LineItems.SingleOrDefault(li => li.ProductId == productId) is not null;

            if (!isExist)
            {
                return true;
            }

            priceList.SplitLineItemFromProduct(productId);

            var result = await _context.PriceLists.ReplaceOneAsync(
                pl => pl.Id == priceList.Id,
                priceList, new ReplaceOptions(), cancellationToken);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        private bool ContractorDuplicatesExists(Contractor contractor, Category category, CancellationToken cancellationToken) =>
            _context.PriceLists
                .FindAsync(pl => pl.Contractor.Name == contractor.Name && pl.Category.Value == category.Value, null, cancellationToken)
                .Result.ToList(cancellationToken).Any();

        private bool PriceListslLineItemDuplicatesNotExists(string lineItemName, Contractor contractor, CancellationToken cancellationToken) =>
            _context.PriceLists.FindAsync(pl => pl.Contractor.Name == contractor.Name, null, cancellationToken)
                .Result.ToList(cancellationToken)
                .SelectMany(pl => pl.LineItems)
                .All(li => li.Name != lineItemName);

        private async Task<PriceList?> GetPriceList(string priceListId, CancellationToken cancellationToken) =>
            (await GetPriceListsAsync(cancellationToken)).SingleOrDefault(pl => pl.Id == priceListId);

        //TODO Kontrahent się loguje -> tworzy się cennik na podstawie retailu -> admin edytuje -> cennika nie można usunąć dopóki istnieje kontrahent
    }
}