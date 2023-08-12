using AutoMapper;
using EventBus.Messages.Common;
using EventBus.Messages.Events;
using OnlineShop.Order.Application.Features.Commands.CheckoutOrder;
using OnlineShop.Order.Domain.Entities;

namespace OnlineShop.Order.Application.Core.Mappings;

public class MapProfiles : Profile
{
    public MapProfiles()
    {
        CreateMap<ShoppingCartCheckoutEvent, CheckoutOrderCommand>()
            .ForMember(d => d.CheckoutOrder, o => o.MapFrom(s => s))
            .ReverseMap();

        CreateMap<OrderItem, ShoppingCartItem>()
            .ForMember(d => d.Price, o => o.MapFrom(s => s.Price)).ReverseMap();

        CreateMap<OnlineOrder, ShoppingCartCheckoutEvent>()
            .ForMember(d => d.Items, o => o.MapFrom(s => s.Items))
            .ForMember(d => d.DeliveryDate, o => o.MapFrom(s => s.DeliveryDate))
            .ForMember(d => d.DeliveryLocation, o => o.MapFrom(s => s.DeliveryLocation))
            .ReverseMap();

        CreateMap<ShoppingCartItem, OrderItem>().ReverseMap();
        CreateMap<Domain.Common.DateTimeRange, DateTimeRange>()
            .ForMember(d => d.Start, o => o.MapFrom(s => s.Start.ToUniversalTime()))
            .ForMember(d => d.End, o => o.MapFrom(s => s.End.ToUniversalTime()))
            .ReverseMap();
        CreateMap<Domain.Common.Location, Location>().ReverseMap();
        CreateMap<Money, Domain.Common.Money>().ReverseMap();
    }
}