using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Order.Application.Features.Commands.CancelOrder;
using OnlineShop.Order.Domain.OnlineOrders;

namespace OnlineShop.Order.IntegrationTests.FeatureTests.Customer;

public class CancelOrderTests : BaseIntegrationTest
{
    public CancelOrderTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CancelOrder_ValidData_ShouldCancel()
    {
        // Arrange
        var orderToDelete = await Context.Set<OnlineOrder>()
            .FirstAsync(o => o.Status != OrderStatus.Received && o.Status != OrderStatus.Rejected);
        
        var command = new CancelOrderCommand(orderToDelete.Id);
        // Act
        var result = await Sender.Send(command);
        var isOrderCancelled = (await Context.Set<OnlineOrder>()
            .Include(onlineOrder => onlineOrder.Status)
            .FirstAsync(o => o.Id == orderToDelete.Id)).Status == OrderStatus.Cancelled;

        // Assert
        Assert.True(isOrderCancelled);
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }

}

