using System;

namespace SocNet.Time
{
    public static class TimeExtensions
    {
        public static DateTime MinutesAgo(this int value)
        {
            return MinutesBefore(value, DateTimeProvider.Now);
        }

        public static DateTime MinutesBefore(this int value, DateTime relativeTo)
        {
            return relativeTo.AddMinutes(-value);
        }

        public static DateTime SecondsAgo(this int value)
        {
            return SecondsBefore(value, DateTimeProvider.Now);
        }

        public static DateTime SecondsBefore(this int value, DateTime relativeTo)
        {
            return relativeTo.AddSeconds(-value);
        }
    }
}