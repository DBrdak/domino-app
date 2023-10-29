using IntegrationEvents.Domain.Results;
using MediatR;
using OnlineShop.Order.Domain.OnlineOrders;

namespace OnlineShop.Order.Application.Features.Commands.CheckoutOrder
{
    public sealed record CheckoutOrderCommand(OnlineOrder CheckoutOrder) : IRequest<CheckoutOrderResult>;
}