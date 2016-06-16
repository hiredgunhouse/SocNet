using System.Collections.Generic;
using SocNet.Model;

namespace SocNet.Storage
{
    public interface IPostStore
    {
        IEnumerable<Post> GetPostsByUser(string user);
        void StorePost(Post post);
        IEnumerable<Post> GetPostsByUsers(IEnumerable<string> users);
    }
}