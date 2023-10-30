using OnlineShop.Order.Domain.OnlineOrders;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace OnlineShop.Order.Application.Features.Queries.GetOrdersAsPdf
{
    public class OrderCard : IDocument
    {
        private readonly IEnumerable<IGrouping<string, OnlineOrder>> _ordersGroupedByShopName;
        private IGrouping<string, OnlineOrder>? _currentGroup;
        private OnlineOrder? _currentOnlineOrder;

        public OrderCard(
            IEnumerable<IGrouping<string, OnlineOrder>> ordersGroupedByShopName)
        {
            _ordersGroupedByShopName = ordersGroupedByShopName;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;


        public void Compose(IDocumentContainer container)
        {
            foreach (var group in _ordersGroupedByShopName)
            {
                _currentGroup = group;

                container
                    .Page(page =>
                    {
                        page.Margin(40);

                        page.Header().Element(ComposeHeader);
                        page.Content().Element(ComposeContent);
                    });
            }
        }

        private void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(30).Bold();

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().AlignCenter().Text(_currentGroup!.Key).Style(titleStyle);
                });
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(25).Column(column =>
            {
                _currentGroup!.ToList().ForEach(
                    order =>
                    {
                        _currentOnlineOrder = order;
                        column.Item().Element(ComposeTable);
                        column.Item().AlignRight().PaddingBottom(20).BorderBottom(1).BorderColor(Colors.Black)
                            .Text($"{_currentOnlineOrder.FirstName} {_currentOnlineOrder.LastName}").FontSize(20);
                    });
            });
        }

        private void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                    columns.RelativeColumn(2);
                });


                table.Header(header =>
                {
                    var locationName = _currentOnlineOrder!.DeliveryLocation.Name != _currentGroup!.Key ? 
                        _currentOnlineOrder.DeliveryLocation.Name : string.Empty;

                    if(!string.IsNullOrWhiteSpace(locationName)) header.Cell().Element(CellStyle).Text($"{locationName}");
                    header.Cell().Element(CellStyle).Text($"{_currentOnlineOrder.DeliveryDate.Start:d.M}");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold().FontSize(20)).AlignCenter().PaddingVertical(5);
                    }
                });

                foreach (var item in _currentOnlineOrder!.Items)
                {
                    table.Cell().Element(CellStyle).Text(item.ProductName).FontSize(21).WrapAnywhere(false);
                    table.Cell().Element(CellStyle).Text($"{item.Quantity}").FontSize(21).WrapAnywhere(false);
                    table.Cell().Element(CellStyle).Text($"{item.Price}").FontSize(21).WrapAnywhere(false);

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2).AlignCenter();
                    }
                }
            });
        }
    }
}
