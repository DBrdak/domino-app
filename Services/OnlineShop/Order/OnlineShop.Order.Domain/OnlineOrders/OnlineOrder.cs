using OnlineShop.Order.Domain.OnlineOrders.Events;
using OnlineShop.Order.Domain.OrderItems;
using Shared.Domain.Abstractions.Entities;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shared.Domain.Money;
using System.ComponentModel.DataAnnotations;
using IntegrationEvents.Domain.Events.ShoppingCartCheckout;
using Shared.Domain.Exceptions;

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
        public string? ShopId { get; private set; }

        // Order Info

        public DateTime CreatedDate { get; private set; }

        private DateTime? _completionDate;

        public DateTime? CompletionDate
        {
            get => _completionDate;
            private set
            {
                _completionDate = value;
                ExpiryDate = value?.AddDays(28);
            }
        }

        public DateTime? ExpiryDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public bool IsPrinted { get; private set; }

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
            ShopId = null;
            IsPrinted = false;
        }

        private void Modify(IEnumerable<OrderItem> orderItems)
        {
            Items = orderItems.ToList();
            Status = OrderStatus.Modified;
            CompletionDate = DateTimeService.UtcNow.ToLocalTime();
        }

        private static string GenerateId() => Ulid.NewUlid(DateTimeOffset.UtcNow).ToString().Substring(4, 12);

        public void Validate(bool isValidationSucceed)
        {
            if (Status != OrderStatus.Validating)
            {
                throw new DomainException<OnlineOrder>($"Order cannot be validated due to it's staus is: {Status}");
            }

            Status = isValidationSucceed ? OrderStatus.Waiting : OrderStatus.Rejected;
        }

        public void UpdateStatus(string status, IEnumerable<OrderItem>? modifiedOrderItems)
        {
            var actions = new Dictionary<OrderStatus, Action>()
            {
                {OrderStatus.Accepted, () => Accept(null)},
                {OrderStatus.Received, Receive},
                {OrderStatus.Rejected, Reject},
                {OrderStatus.Modified, () => Accept(modifiedOrderItems)},
            };

            if (actions.TryGetValue(OrderStatus.FromMessage(status), out Action updateAction))
            {
                updateAction.Invoke();
            }
            else
            {
                throw new DomainException<OnlineOrder>("Wrong order status provided");
            }
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Received || Status == OrderStatus.Rejected)
            {
                throw new DomainException<OnlineOrder>(
                    $"Cannot cancel order with id: {Id} because of it status is {Status.StatusMessage}");
            }

            CompletionDate = DateTimeService.UtcNow.ToLocalTime();
            Status = OrderStatus.Cancelled;
        }

        private void Reject()
        {
            if (Status == OrderStatus.Received || Status == OrderStatus.Accepted || Status == OrderStatus.Modified)
            {
                throw new DomainException<OnlineOrder>(
                    $"Cannot reject order with id: {Id} because of it status is {Status.StatusMessage}");
            }

            CompletionDate = DateTimeService.UtcNow.ToLocalTime();
            Status = OrderStatus.Rejected;
        }

        private void Receive()
        {
            if (Status != OrderStatus.Accepted && Status != OrderStatus.Modified)
            {
                throw new DomainException<OnlineOrder>(
                    $"Cannot receive order with id: {Id} because of it status is {Status.StatusMessage}");
            }

            CompletionDate = DateTimeService.UtcNow.ToLocalTime();
            Status = OrderStatus.Received;
        }

        private void Accept(IEnumerable<OrderItem>? modifiedOrderItems)
        {
            if (Status != OrderStatus.Waiting)
            {
                throw new DomainException<OnlineOrder>(
                    $"Cannot accept order with id: {Id} because of it status is {Status.StatusMessage}");
            }

            if (modifiedOrderItems is null)
            {
                CompletionDate = DateTimeService.UtcNow.ToLocalTime();
                Status = OrderStatus.Accepted;
                return;
            }

            Modify(modifiedOrderItems);
        }

        public void SetShopId(string shopId)
        {
            if (string.IsNullOrWhiteSpace(shopId))
            {
                throw new DomainException<OnlineOrder>("ShopId is required when setting ShopId");
            }

            ShopId = shopId;
        }

        public void Print()
        {
            if (Status != OrderStatus.Accepted && Status != OrderStatus.Modified)
            {
                throw new DomainException<OnlineOrder>($"Cannot print order with id: {Id} because of it status is {Status.StatusMessage}");
            }

            IsPrinted = true;
        }

        public void PrintLost() 
        {
            if (Status != OrderStatus.Accepted && Status != OrderStatus.Modified)
            {
                throw new DomainException<OnlineOrder>($"Cannot change print status of order with id: {Id} because of it status is {Status.StatusMessage}");
            }

            IsPrinted = false;
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
                shoppingCart.DeliveryDate.ParseToUTC(),
                GenerateId(),
                DateTimeService.UtcNow.ToLocalTime(),
                null,
                OrderStatus.Validating);

            // TODO TEMPORARY SOLUTION
            //order.Validate(true);
            // TODO TEMPORARY SOLUTION

            order.RaiseDomainEvent(new OrderCreatedDomainEvent(order.Id, order.PhoneNumber));

            return order;
        }

        public void SafeDelete()
        {
            if (ShopId is null)
            {
                return;
            }

            RaiseDomainEvent(new OrderDeletedDomainEvent(ShopId, Id));
        }
    }
}