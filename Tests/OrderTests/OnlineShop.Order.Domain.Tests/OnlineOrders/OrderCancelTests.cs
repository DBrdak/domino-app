using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.IdGenerators;
using OnlineShop.Order.Domain.OnlineOrders;
using OnlineShop.Order.Domain.OrderItems;
using Shared.Domain.Exceptions;

namespace OnlineShop.Order.Domain.Tests.OnlineOrders
{
    public class OrderCancelTests
    {
        [Theory]
        [InlineData("Oczekuje na potwierdzenie")]
        [InlineData("Potwierdź kod SMS")]
        [InlineData("Potwierdzone")]
        [InlineData("Potwierdzone ze zmianami")]
        public void Cancel_ValidData_ShouldCancelSuccessfully(string status)
        {
            // Arrange
            var order = OnlineOrderTestData.TestOnlineOrder.Invoke();

            switch (status)
            {
                case "Oczekuje na potwierdzenie":
                    order.Validate(true);
                    break;
                case "Potwierdź kod SMS":
                    break;
                case "Potwierdzone":
                    order.Validate(true);
                    order.UpdateStatus(status, null);
                    break;
                case "Potwierdzone ze zmianami":
                    order.Validate(true);
                    order.UpdateStatus(status, new List<OrderItem>() { order.Items.First() });
                    break;
            }

            // Act
            order.Cancel();

            // Assert
            Assert.Equal(order.Status, Domain.OnlineOrders.OrderStatus.Cancelled);
        }

        [Theory]
        [InlineData("Odebrane")]
        [InlineData("Odrzucone")]
        public void Cancel_InvalidData_ShouldThrow(string status)
        {
            // Arrange
            var order = OnlineOrderTestData.TestOnlineOrder.Invoke();
            order.Validate(true);
            if(status == "Odebrane") order.UpdateStatus("Potwierdzone", null);
            order.UpdateStatus(status, null);

            // Act
            var cancelOrderFunc = () => order.Cancel();

            // Assert
            Assert.Throws<DomainException<OnlineOrder>>(cancelOrderFunc);
        }
    }
}
