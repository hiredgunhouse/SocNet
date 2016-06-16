using SocNet.Formatting;
using SocNet.Storage;

namespace SocNet
{
    public static class SocAppFactory
    {
        public static SocApp Create()
        {
            return new SocApp(
                new PostStore(),
                new FollowStore(),
                new PostFormatter());
        }
    }
}