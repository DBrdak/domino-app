using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Date
{
    public sealed record WeekDay
    {
        internal static readonly WeekDay None = new("");
        public static readonly WeekDay Monday = new("Poniedziałek");
        public static readonly WeekDay Tuesday = new("Wtorek");
        public static readonly WeekDay Wednesday = new("Środa");
        public static readonly WeekDay Thursday = new("Czwartek");
        public static readonly WeekDay Friday = new("Piątek");
        public static readonly WeekDay Saturday = new("Sobota");
        public static readonly WeekDay Sunday = new("Niedziela");

        public WeekDay()
        { }

        private WeekDay(string code)
        {
            Code = code;
        }

        public string Code { get; init; }

        public static int GetIndex(string weekDay) => All.ToList().IndexOf(FromCode(weekDay));
        public static int GetIndex(WeekDay weekDay) => All.ToList().IndexOf(weekDay);

        public static WeekDay FromCode(string code)
        {
            return All.FirstOrDefault(c => c.Code.ToLower() == code.ToLower()) ??
                   throw new ApplicationException("The week day code is invalid");
        }

        public static WeekDay FromDayOfWeekEnum(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return Monday;

                case DayOfWeek.Tuesday:
                    return Tuesday;

                case DayOfWeek.Wednesday:
                    return Wednesday;

                case DayOfWeek.Thursday:
                    return Thursday;

                case DayOfWeek.Friday:
                    return Friday;

                case DayOfWeek.Saturday:
                    return Saturday;

                case DayOfWeek.Sunday:
                    return Sunday;

                default:
                    throw new ApplicationException($"Cannot convert {dayOfWeek} to WeekDay type");
            }
        }

        public static WeekDay operator ++(WeekDay day)
        {
            var currentIndex = All.ToList().IndexOf(day);

            if (currentIndex >= 6)
            {
                return All.ToArray()[0];
            }

            return All.ToArray()[currentIndex + 1];
        }

        public static WeekDay operator --(WeekDay day)
        {
            var currentIndex = All.ToList().IndexOf(day);

            if (currentIndex <= 0)
            {
                return All.ToArray()[6];
            }

            return All.ToArray()[currentIndex - 1];
        }

        public static readonly IReadOnlyCollection<WeekDay> All = new[]
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday,
        };
    }
}