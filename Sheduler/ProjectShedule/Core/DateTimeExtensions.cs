using System;

namespace ProjectShedule.Core
{
    public static class DateTimeExtensions
    {
        public static DateTime LongInDateTime(long longDateTime)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(longDateTime).ToLocalTime();
        }
    }
}