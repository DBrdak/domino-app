﻿using OnlineShop.Order.Application.Core;
using OnlineShop.Order.Application.Core.Interfaces;
using OnlineShop.Order.Domain.Entities;

namespace OnlineShop.Order.Application.Features.Queries.GetCustomerOrder
{
    public record GetCustomerOrderQuery(string PhoneNumber, string OrderId) : IQuery<Result<OnlineOrder>>;
}