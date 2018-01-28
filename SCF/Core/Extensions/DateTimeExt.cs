using System.Globalization;

// ReSharper disable once CheckNamespace

namespace System
{
    public static class DateTimeExt
    {
        public static DateTime MinDate = new DateTime(1753, 1, 1);

        public static TimeZoneInfo EasternTimeZone
        {
            get { return TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"); }
        }

        public static TimeZoneInfo PacificTimeZone
        {
            get { return TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"); }
        }

        public static int EstToUnix(this DateTime val)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            date = TimeZoneInfo.ConvertTimeFromUtc(date, EasternTimeZone);
            return (int) (val - date).TotalSeconds;
        }

        public static DateTime UnixToEst(long seconds)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            date = date.AddSeconds(seconds);
            date = TimeZoneInfo.ConvertTimeFromUtc(date, EasternTimeZone);
            return date;
        }

        public static string ToDateString(this DateTime date)
        {
            return date.ToString("MMMM dd, yyyy");
        }

        public static string ToDateString(this DateTime? value)
        {
            if (!value.HasValue)
                return "";
            return value.Value.ToDateString();
        }

        public static string ToTimeString(this DateTime? value)
        {
            if (!value.HasValue)
                return "";
            return value.Value.ToTimeString();
        }

        public static string ToTimeString(this DateTime value)
        {
            return value.ToString("hh:mm");
        }

        public static DateTime? ToDateTime(this string s)
        {
            var date = new DateTime();
            if (
                !DateTime.TryParseExact(s, new[] { "dd MMM yyyy", "d MMM yyyy", "dd MMMM yyyy", "MM/dd/yyyy", "MMMM dd, yyyy", "MMM dd, yyyy"},
                    CultureInfo.CurrentUICulture.DateTimeFormat, DateTimeStyles.None, out date))
                return null;
            return date;
        }
    }
}