using System;
using SocNet.Clock;

namespace SocNet.Time
{
    public static class TimeExtensions
    {
        public static DateTime MinutesAgo(this int value)
        {
            var now = DateTimeProvider.Now;
            return MinutesBefore(value, now);
        }

        public static DateTime MinutesBefore(this int value, DateTime relativeTo)
        {
            return relativeTo.AddMinutes(-value);
        }

        public static DateTime SecondsAgo(this int value)
        {
            var now = DateTimeProvider.Now;
            return SecondsBefore(value, now);
        }

        public static DateTime SecondsBefore(this int value, DateTime relativeTo)
        {
            return relativeTo.AddSeconds(-value);
        }
    }
}