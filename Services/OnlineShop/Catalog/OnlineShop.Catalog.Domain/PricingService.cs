using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain
{
    internal static class PricingService
    {
        internal static Money CalculatePrice(Money sourcePrice, ProductDetails details, Unit destinationUnit)
        {
            if (sourcePrice.Unit == destinationUnit ||
                !details.IsWeightSwitchAllowed ||
                details.SingleWeight is null)
            {
                return sourcePrice;
            }

            var convertedPriceAmount = sourcePrice.Unit.Code == "kg" ?
                sourcePrice.Amount * details.SingleWeight.Value :
                sourcePrice.Amount / details.SingleWeight.Value;

            return new(convertedPriceAmount, sourcePrice.Currency, destinationUnit);
        }
    }
}