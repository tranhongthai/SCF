using Peyton.Core.Web;
using System.Globalization;

// ReSharper disable once CheckNamespace

namespace System
{
    public static class DateTimeExt
    {
        public static DateTime MinDate = new DateTime(1900, 1, 1);
        public static DateTime StartUnixDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
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
            var date = StartUnixDate;
            date = TimeZoneInfo.ConvertTimeFromUtc(date, EasternTimeZone);
            return (int) (val - date).TotalSeconds;
        }

        public static string ToTimeString(this int val)
        {
            var time = DateTime.Today.AddHours(val);
            return time.ToString(SettingManager.Get<string>("TimeFormat"));
        }

        public static DateTime UnixToEst(long seconds)
        {
            var date = StartUnixDate;
            date = date.AddSeconds(seconds);
            date = TimeZoneInfo.ConvertTimeFromUtc(date, EasternTimeZone);
            return date;
        }

        public static string ToDateString(this DateTime date)
        {
            return date.ToString(SettingManager.Get<string>("DateFormat"));
        }

        public static string ToRealTimeString(this DateTime date)
        {
            var now = DateTime.Now;
            var today = DateTime.Today;
            var yesterday = DateTime.Today.AddDays(-1);
            var duration = now - date;
            var thisYear = DateTime.Today.Year;
            if (duration.TotalMinutes < 1)
                return "just now";
            if (duration.TotalMinutes < 2)
                return "1 min ago";
            if (duration.TotalHours<1)
                return duration.Minutes.ToString() + " mins ago";
            if (duration.TotalHours < 2)
                return "1 hour ago";
            if (duration.TotalHours < 4)
                return duration.Hours.ToString() + " hours ago";
            if (date >= today)
                return "Today " + date.ToTimeString();
            if(date >= yesterday)
                return "Yesterday " + date.ToTimeString();
            if (date > DateTime.Today.AddDays(-7))
                return date.ToString("dddd");
            if (date.Year == thisYear)
                return date.ToString("MMM dd");
            return date.ToString("MMM dd, yyyy");
        }

        public static DateTime AddTime(this DateTime date, string time)
        {
            var s = date.ToDateString() + " " + time;
            var result = s.ToDateTime();
            return result.HasValue ? result.Value : date;
        }

        public static string ToDateTimeString(this DateTime date)
        {
            return date.ToString(SettingManager.Get<string>("DateTimeFormat"));
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
            return value.ToString("hh:mm tt");
        }

        public static DateTime ToDateTime(string date, string time)
        {
            var value = date.ToDateTime();
            if (!value.HasValue) return MinDate;
            return value.Value.AddTime(time);
        }

        public static DateTime? ToDateTime(this string s)
        {
            var date = new DateTime();
            if (!DateTime.TryParseExact(s, new[] { "dd.MM.yyyy hh:mm tt", "dd.MM.yyyy", "dd/MM/yyyy hh:mm tt", "dd MMM yyyy", "d MMM yyyy", "dd MMMM yyyy", "dd/MM/yyyy", "MMMM dd, yyyy", "MMM dd, yyyy", ""},
                    CultureInfo.CurrentUICulture.DateTimeFormat, DateTimeStyles.None, out date))
                return null;
            return date;
        }
    }
}