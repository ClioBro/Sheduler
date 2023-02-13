using System;

namespace ProjectShedule.Core
{
    public struct DateTimeRange
    {
        public DateTimeRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
            if (Start > End)
                throw new ArgumentOutOfRangeException($"{nameof(Start)} value is greater than {nameof(End)} value");
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public TimeSpan SubtractToTimeSpan()
        {
            return SubtractToTimeSpan(End, Start);
        }
        public TimeSpan SubtractToTimeSpan(DateTimeRange dateTimeRange)
        {
            return SubtractToTimeSpan(this, dateTimeRange);
        }
        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}";
        }

        public static bool operator >(DateTimeRange range1, DateTimeRange range2)
        {
            TimeSpan timeSpan1 = range1.SubtractToTimeSpan();
            TimeSpan timeSpan2 = range2.SubtractToTimeSpan();

            return timeSpan1 > timeSpan2;
        }
        public static bool operator <(DateTimeRange range1, DateTimeRange range2)
        {
            TimeSpan timeSpan1 = range1.SubtractToTimeSpan();
            TimeSpan timeSpan2 = range2.SubtractToTimeSpan();

            return timeSpan1 < timeSpan2;
        }
        public static bool operator ==(DateTimeRange range1, DateTimeRange range2)
        {
            TimeSpan timeSpan1 = range1.SubtractToTimeSpan();
            TimeSpan timeSpan2 = range2.SubtractToTimeSpan();

            return timeSpan1 == timeSpan2;
        }
        public static bool operator !=(DateTimeRange range1, DateTimeRange range2)
        {
            TimeSpan timeSpan1 = range1.SubtractToTimeSpan();
            TimeSpan timeSpan2 = range2.SubtractToTimeSpan();

            return timeSpan1 != timeSpan2;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private TimeSpan SubtractToTimeSpan(DateTimeRange dateTimeRange1, DateTimeRange dateTimeRange2)
        {
            var timeSpan1 = dateTimeRange1.SubtractToTimeSpan();
            var timeSpan2 = dateTimeRange2.SubtractToTimeSpan();

            return timeSpan1.Subtract(timeSpan2);
        }
        private TimeSpan SubtractToTimeSpan(DateTime dateTime1, DateTime dateTime2)
        {
            return dateTime1.Subtract(dateTime2);
        }
    }
}
