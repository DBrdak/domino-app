using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;
using QuestPDF.Fluent;

namespace OnlineShop.Order.Application.Features.Queries.GetOrdersAsPdf
{
    internal sealed class GetOrdersAsPdfQueryHandler : IQueryHandler<GetOrdersAsPdfQuery, byte[]>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersAsPdfQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<byte[]>> Handle(GetOrdersAsPdfQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.PrepareOrdersForPrint(request.OrdersId, cancellationToken);

            if (!orders.Any())
            {
                _ = _orderRepository.CatchPrintingError(request.OrdersId, cancellationToken);
                return Result.Failure<byte[]>(Error.NotFound($"Cannot create pdf - no orders was found for given ids"));
            }

            var shopNames = await _orderRepository
                .GetShopsName(orders.Select(o => o.ShopId ?? throw new ApplicationException($"Order with ID {o.Id} has no aggregated shop")), 
                    cancellationToken);

            if (shopNames is null)
            {
                _ = _orderRepository.CatchPrintingError(request.OrdersId, cancellationToken);
                return Result.Failure<byte[]>(Error.NotFound($"No shops found for PDF generation"));
            }

            var groupedOrders = orders
                .GroupBy(o => shopNames.ShopNameWithId.First(s => s.Id == o.ShopId).Name)
                .ToList();

            try
            {
                using var stream = new MemoryStream();

                var document = new OrderCard(groupedOrders);

                document.GeneratePdf(stream);

                return stream.ToArray();
            }
            catch (Exception e)
            {
                _ = _orderRepository.CatchPrintingError(request.OrdersId, cancellationToken);
                return Result.Failure<byte[]>(Error.TaskFailed($"Error generating PDF file for orders"));
            }
        }
    }
}
