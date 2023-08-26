using System.ComponentModel.DataAnnotations;
using EventBus.Domain.Common;
using EventBus.Domain.Events;
using OnlineShop.Order.Domain.OnlineOrders.Events;
using OnlineShop.Order.Domain.OrderItems;
using Shared.Domain.Abstractions;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shared.Domain.Money;

namespace OnlineShop.Order.Domain.OnlineOrders
{
    public class OnlineOrder : Entity
    {
        // Shopping Cart Info

        public Money TotalPrice { get; private set; }
        public List<OrderItem> Items { get; private set; }

        // Personal Info

        [RegularExpression("^[0-9]{9}$")]
        public string PhoneNumber { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        // Delivery Info

        public Location DeliveryLocation { get; private set; }
        public DateTimeRange DeliveryDate { get; private set; }

        // Order Info

        public DateTime CreatedDate { get; private set; }
        public DateTime? CompletionDate { get; private set; }
        public DateTime? ExpiryDate => CompletionDate?.AddDays(28);
        public OrderStatus Status { get; private set; }

        public OnlineOrder()
        { }

        private OnlineOrder(
            Money totalPrice,
            List<ShoppingCartCheckoutItem> items,
            string phoneNumber,
            string firstName,
            string lastName,
            Location deliveryLocation,
            DateTimeRange deliveryDate,
            string orderId,
            DateTime createdDate,
            DateTime? receiveDate,
            OrderStatus status
            ) : base(orderId)
        {
            TotalPrice = totalPrice;
            Items = OrderItem.CreateFromShoppingCartItems(orderId, items);
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            DeliveryLocation = deliveryLocation;
            DeliveryDate = deliveryDate;
            CreatedDate = createdDate;
            CompletionDate = receiveDate;
            Status = status;
        }

        private void Modify(OnlineOrder order, OrderStatus status)
        {
            TotalPrice = order.TotalPrice;
            Items = order.Items;
            PhoneNumber = order.PhoneNumber;
            FirstName = order.FirstName;
            LastName = order.LastName;
            DeliveryLocation = order.DeliveryLocation;
            DeliveryDate = order.DeliveryDate;
            Id = order.Id;
            CreatedDate = order.CreatedDate;
            CompletionDate = order.CompletionDate;
            Status = status;
        }

        private static string GenerateId() => Ulid.NewUlid(DateTimeOffset.UtcNow).ToString().Substring(4, 12);

        public void Validate(bool isValidationSucceed) =>
            Status = isValidationSucceed ? OrderStatus.Waiting : OrderStatus.Rejected;

        public void Canceled()
        {
            CompletionDate = DateTimeService.UtcNow;
            Status = OrderStatus.Cancelled;
        }

        public void Rejected()
        {
            CompletionDate = DateTimeService.UtcNow;
            Status = OrderStatus.Rejected;
        }

        public void Received()
        {
            CompletionDate = DateTimeService.UtcNow;
            Status = OrderStatus.Received;
        }

        public void Accepted(OnlineOrder? modifiedOrder)
        {
            if (modifiedOrder is null)
            {
                Status = OrderStatus.Accepted;
                return;
            }

            Modify(modifiedOrder, OrderStatus.Modified);
        }

        public static OnlineOrder Create(ShoppingCartCheckoutEvent shoppingCart)
        {
            var order = new OnlineOrder(
                shoppingCart.TotalPrice,
                shoppingCart.Items,
                shoppingCart.PhoneNumber,
                shoppingCart.FirstName,
                shoppingCart.LastName,
                shoppingCart.DeliveryLocation,
                shoppingCart.DeliveryDate,
                GenerateId(),
                DateTimeService.UtcNow,
                null,
                OrderStatus.Validating);

            order.RaiseDomainEvent(new OrderCreatedDomainEvent(order.Id, order.PhoneNumber));

            return order;
        }
    }
}