using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;
using Shops.Domain.Abstractions;
using Shops.Domain.MobileShops;
using Shops.Domain.StationaryShops;

namespace Shops.Application.Features.Commands.UpdateShop
{
    internal sealed class UpdateShopCommandHandler : ICommandHandler<UpdateShopCommand, Shop>
    {
        private readonly IShopRepository _shopRepository;
        private Result<Shop> _result;

        public UpdateShopCommandHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
            _result = Result.Failure<Shop>(
                Error.InvalidRequest("Update values not provided - unable to update shop")
            );
        }

        public async Task<Result<Shop>> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
        {
            var shops = await _shopRepository.GetShops(cancellationToken);
            var shopToUpdate = shops.FirstOrDefault(s => s.Id == request.ShopToUpdateId);

            if (shopToUpdate is null)
            {
                _result = Result.Failure<Shop>(
                    Error.InvalidRequest($"Shop with ID: {request.ShopToUpdateId} not found")
                    );
                return _result;
            }

            if (request.NewSeller is not null)
            {
                shopToUpdate.AddSeller(request.NewSeller);
                var result = await _shopRepository.UpdateShop(shopToUpdate, cancellationToken);

                _result = result;
            }
            //TODO Remove seller

            if (request.MobileShopUpdateValues is not null)
            {
                var mobileShopToUpdate = shopToUpdate as MobileShop;

                if (mobileShopToUpdate is null)
                {
                    _result = Result.Failure<Shop>(Error.InvalidRequest(
                        $"Cannot convert provided shop to {typeof(MobileShop)}"));
                    return _result;
                }

                UpdateMobileShop(ref mobileShopToUpdate, request.MobileShopUpdateValues);
                var result = await _shopRepository.UpdateShop(mobileShopToUpdate, cancellationToken);

                _result = result;
            }
            else if (request.StationaryShopUpdateValues is not null)
            {
                var stationaryShopToUpdate = shopToUpdate as StationaryShop;

                if (stationaryShopToUpdate is null)
                {
                    _result = Result.Failure<Shop>(Error.InvalidRequest(
                        $"Cannot convert provided shop to {typeof(StationaryShop)}"));
                    return _result;
                }

                UpdateStationaryShop(ref stationaryShopToUpdate, request.StationaryShopUpdateValues);
                var result = await _shopRepository.UpdateShop(stationaryShopToUpdate, cancellationToken);

                _result = result;
            }

            return _result;
        }

        private void UpdateMobileShop(ref MobileShop mobileShopToUpdate, MobileShopUpdateValues values)
        {
            if (values.NewSalePoint is not null)
            {
                mobileShopToUpdate.AddSalePoint(
                    values.NewSalePoint.Location,
                    values.NewSalePoint.OpenHours!,
                    values.NewSalePoint.WeekDay.Value);
            }
            if (values.SalePointToRemove is not null)
            {
                mobileShopToUpdate.RemoveSalePoint(values.SalePointToRemove);
            }
            if (values.SalePointToEnable is not null)
            {
                mobileShopToUpdate.EnableSalePoint(values.SalePointToEnable);
            }
            if (values.SalePointToDisable is not null)
            {
                mobileShopToUpdate.DisableSalePoint(values.SalePointToDisable);
            }
        }

        private void UpdateStationaryShop(ref StationaryShop stationaryShopToUpdate, StationaryShopUpdateValues values)
        {
            if (values.InitWeekSchedule is not null)
            {
                stationaryShopToUpdate.CreateWeekSchedule(values.InitWeekSchedule);
            }
            if (values.WeekDayToUpdate is not null && values.NewWorkingHoursInWeekDay is not null)
            {
                stationaryShopToUpdate.UpdateOpenHoursForWeekDay(
                    values.WeekDayToUpdate,
                    values.NewWorkingHoursInWeekDay);
            }
            if (values.WeekDayAsHoliday is not null)
            {
                stationaryShopToUpdate.SetHolidayForWeekDay(values.WeekDayAsHoliday);
            }
            if (values.WeekDayAsWorkingDay is not null)
            {
                stationaryShopToUpdate.SetWorkForWeekDay(values.WeekDayAsWorkingDay);
            }
        }
    }
}