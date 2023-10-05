using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shops.Domain.Shared;

namespace Shops.Application.Features.Commands.UpdateShop;

public sealed record StationaryShopUpdateValues(
    List<ShopWorkingDay>? InitWeekSchedule,
    WeekDay? WeekDayToUpdate,
    TimeRange? NewWorkingHoursInWeekDay,
    WeekDay? WeekDayAsHoliday,
    WeekDay? WeekDayAsWorkingDay);