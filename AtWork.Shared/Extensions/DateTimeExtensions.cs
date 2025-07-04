﻿using TimeZoneConverter;

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

        public static DateTime ToBrazilianTime(this DateTime dateTime)
        {
            var brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            // Se a data já estiver no horário do Brasil, apenas retorne
            if (dateTime.Kind == DateTimeKind.Local &&
                TimeZoneInfo.Local.Id == brazilTimeZone.Id)
            {
                return dateTime;
            }

            // Se for UTC, converta para o horário do Brasil
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return TimeZoneInfo.ConvertTimeFromUtc(dateTime, brazilTimeZone);
            }

            return dateTime;
        }
    }
}
