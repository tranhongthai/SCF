using System;

namespace Peyton.Core.Extensions
{
    public class Range<T>
    {
        public Range()
        {
            Start = (T) Activator.CreateInstance(typeof (T));
            End = (T) Activator.CreateInstance(typeof (T));
        }

        public T Start { get; set; }
        public T End { get; set; }
    }

    public class DateTimeRange : Range<DateTime>
    {
        public TimeSpan Duration {
            get { return End - Start; }
        }

        public DateTimeRange()
        {
            Start = DateTimeExt.MinDate;
            End = DateTimeExt.MinDate;
        }
    }
}