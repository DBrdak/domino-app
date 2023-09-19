namespace Shared.Domain.Date
{
    /// <summary>
    /// <b>Use it instead of DateTime struct to define new date</b>
    /// </summary>
    public sealed class DateTimeService
    {
        public static DateTime UtcNow => DateTime.UtcNow;
    }
}