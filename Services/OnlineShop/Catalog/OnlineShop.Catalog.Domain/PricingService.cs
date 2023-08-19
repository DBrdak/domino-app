using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Domain.Common;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain
{
    public static class PricingService
    {
        public static Money CalculatePrice(Money sourcePrice, ProductDetails details, Unit destinationUnit)
        {
            if (sourcePrice.Unit == destinationUnit ||
                !details.IsWeightSwitchAllowed ||
                details.SingleWeight is null)
            {
                return sourcePrice;
            }

            var convertedPriceAmount = sourcePrice.Unit.Code == "kg" ?
                sourcePrice.Amount * details.SingleWeight :
                sourcePrice.Amount / details.SingleWeight;

            return new(convertedPriceAmount.Value, sourcePrice.Currency, destinationUnit);
        }
    }
}