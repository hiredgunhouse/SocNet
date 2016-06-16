using System;

namespace SocNet.Clock
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}