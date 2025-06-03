using TimeZoneConverter;

namespace AtWork.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly TimeZoneInfo BrazilTimeZone =
            TZConvert.GetTimeZoneInfo("America/Sao_Paulo");

        public static DateTime GetFirstMomentOfDate(this DateTime dateTime)
        {
            var localDate = dateTime.Kind == DateTimeKind.Utc
                ? TimeZoneInfo.ConvertTimeFromUtc(dateTime, BrazilTimeZone)
                : dateTime;

            return new DateTime(localDate.Year, localDate.Month, localDate.Day, 0, 0, 1, DateTimeKind.Unspecified);
        }

        public static DateTime GetFirstMomentOfDateOrDefault(this DateTime? dateTime)
        {
            if (dateTime == null)
                return DateTime.MinValue;

            return dateTime.Value.GetFirstMomentOfDate();
        }

        public static DateTime GetLastMomentOfDate(this DateTime dateTime)
        {
            var localDate = dateTime.Kind == DateTimeKind.Utc
                ? TimeZoneInfo.ConvertTimeFromUtc(dateTime, BrazilTimeZone)
                : dateTime;

            return new DateTime(localDate.Year, localDate.Month, localDate.Day, 23, 59, 59, DateTimeKind.Unspecified);
        }

        public static DateTime GetLastMomentOfDateOrDefault(this DateTime? dateTime)
        {
            if (dateTime == null)
                return DateTime.MinValue;

            return dateTime.Value.GetLastMomentOfDate();
        }

        public static bool HasValue(this DateTime dateTime)
        {
            return dateTime != DateTime.MinValue && dateTime != DateTime.MaxValue;
        }

        public static DateTime Now()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, BrazilTimeZone);
        }

        public static DateTime ToBrazilianTime(this DateTime utcDateTime)
        {
            if (utcDateTime.Kind != DateTimeKind.Utc)
                throw new ArgumentException("A data deve estar em UTC", nameof(utcDateTime));

            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, BrazilTimeZone);
        }
    }
}
