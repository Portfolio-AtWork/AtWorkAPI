namespace AtWork.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetFirstMomentOfDate(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        public static DateTime GetFirstMomentOfDateOrDefault(this DateTime? dateTime)
        {
            if (dateTime == null)
                return DateTime.MinValue;

            return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, 0, 0, 0);
        }

        public static DateTime GetLastMomentOfDate(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }

        public static DateTime GetLastMomentOfDateOrDefault(this DateTime? dateTime)
        {
            if (dateTime == null)
                return DateTime.MinValue;

            return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, 23, 59, 59);
        }

        public static bool HasValue(this DateTime dateTime)
        {
            return dateTime != DateTime.MinValue && dateTime != DateTime.MaxValue;
        }
    }
}
