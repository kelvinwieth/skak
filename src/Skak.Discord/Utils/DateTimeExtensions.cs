namespace Skak.Discord.Utils
{
    public static class DateTimeExtensions
    {
        public static DateTime ToBrasilia(this DateTime dateTime)
        {
            var utc = dateTime.ToUniversalTime();
            var brasiliaInfo = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utc, brasiliaInfo);
        }

        public static DateTime ToLisboa(this DateTime dateTime)
        {
            var utc = dateTime.ToUniversalTime();
            var lisboaInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utc, lisboaInfo);
        }
    }
}
