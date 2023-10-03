namespace Shared.Domain.Date
{
    /// <summary>
    /// <b>Use it instead of DateTime struct to define new date</b>
    /// </summary>
    public sealed class DateTimeService
    {
        public static DateTime UtcNow => DateTime.UtcNow;
        public static DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);

        public static DateTime GetDateTimeForNextDayOfWeek(WeekDay nextDayOfWeek)
        {
            var todayDayOfWeek = WeekDay.FromDayOfWeekEnum(UtcNow.DayOfWeek);
            var resultDate = UtcNow;

            while (todayDayOfWeek != nextDayOfWeek)
            {
                todayDayOfWeek++;
                resultDate = resultDate.AddDays(1);
            }

            return resultDate;
        }

        public static DateOnly GetDateOnlyForNextDayOfWeek(WeekDay nextDayOfWeek)
        {
            var todayDayOfWeek = WeekDay.FromDayOfWeekEnum(UtcNow.DayOfWeek);
            var resultDate = DateOnly.FromDateTime(UtcNow);

            while (todayDayOfWeek != nextDayOfWeek)
            {
                todayDayOfWeek++;
                resultDate = resultDate.AddDays(1);
            }

            return resultDate;
        }
    }
}