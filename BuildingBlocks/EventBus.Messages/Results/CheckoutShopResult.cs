using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Domain.Results
{
    public sealed record CheckoutShopResult(string ShopId, string? Error, bool IsSuccess)
    {
        public static CheckoutShopResult Success(string shopId)
        {
            return new(shopId, null, true);
        }

        public static CheckoutShopResult Failure(string message)
        {
            return new(null, message, false);
        }
    }
}
