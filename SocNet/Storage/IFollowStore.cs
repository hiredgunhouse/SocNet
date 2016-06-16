using System.Collections.Generic;

namespace SocNet.Storage
{
    public interface IFollowStore
    {
        void StoreFollow(string user, string whoToFollow);
        IEnumerable<string> GetFollowedBy(string user);
    }
}