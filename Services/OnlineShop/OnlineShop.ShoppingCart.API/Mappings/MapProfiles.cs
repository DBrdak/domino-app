﻿using AutoMapper;
using EventBus.Messages.Events;
using OnlineShop.ShoppingCart.API.Entities;
using OnlineShop.ShoppingCart.API.Models;
using ShoppingCartItem = OnlineShop.ShoppingCart.API.Entities.ShoppingCartItem;

namespace OnlineShop.ShoppingCart.API.Mappings
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<ShoppingCartItem, EventBus.Messages.Common.ShoppingCartItem>()
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
                .ReverseMap();

            CreateMap<ShoppingCartCheckout, ShoppingCartCheckoutEvent>()
                .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.TotalPrice))
                .ForMember(d => d.Items, o => o.MapFrom(s => s.Items))
                .ForMember(d => d.DeliveryLocation, o => o.MapFrom(s => s.DeliveryLocation))
                .ForMember(d => d.DeliveryDate, o => o.MapFrom(s => s.DeliveryDate))
                .ReverseMap();

            CreateMap<Money, EventBus.Messages.Common.Money>().ReverseMap();
            CreateMap<Location, EventBus.Messages.Common.Location>().ReverseMap();
            CreateMap<DateTimeRange, EventBus.Messages.Common.DateTimeRange>().ReverseMap();
        }
    }
}