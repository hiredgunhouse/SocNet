using System;
using SocNet.Clock;

namespace SocNet
{
    public static class DateTimeProvider 
    {
        private static readonly IDateTimeProvider Default = new DefaultDateTimeProvider();
        private static IDateTimeProvider _dateTimeProvider;

        public static DateTime Now => _dateTimeProvider.Now();

        static DateTimeProvider()
        {
            SetDefaultProvider();
        }

        public static void SetDefaultProvider()
        {
            _dateTimeProvider = Default;
        }

        public static void SetCustomProvider(IDateTimeProvider provider)
        {
            _dateTimeProvider = provider;
        }
    }
}