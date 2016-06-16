using System;
using SocNet.Clock;

namespace SocNet.Tests.Integration.Time
{
    public class DateTimeProviderContext : IDisposable
    {
        public DateTimeProviderContext(IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider.SetCustomProvider(dateTimeProvider);
        }

        public void Dispose()
        {
            DateTimeProvider.SetDefaultProvider();
        }
    }
}