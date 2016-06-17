using SocNet.Clock;
using SocNet.Commands;

namespace SocNet.Model
{
    public static class PostFactory
    {
        public static Post CreateFrom(PostCommand postCommand)
        {
            return new Post
            {
                Date = DateTimeProvider.Now,
                User = postCommand.User,
                Message = postCommand.Message
            };
        }
    }
}