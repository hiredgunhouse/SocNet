namespace SocNet.Clock
{
    public class DefaultDateTimeProvider : IDateTimeProvider
    {
        public System.DateTime Now()
        {
            return System.DateTime.Now;
        }
    }
}