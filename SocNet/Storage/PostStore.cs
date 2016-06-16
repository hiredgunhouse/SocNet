using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SocNet.Model;

namespace SocNet.Storage
{
    public class PostStore : IPostStore
    {
        protected IList<Post> Posts { get; set; }

        public PostStore()
        {
            Posts = new List<Post>();
        }

        public IEnumerable<Post> GetPostsByUser(string user)
        {
            var posts = Posts
                .Where(p => p.User == user)
                .ToList();

            return new ReadOnlyCollection<Post>(posts);
        }

        public void StorePost(Post post)
        {
            Posts.Add(post);
        }

        public IEnumerable<Post> GetPostsByUsers(IEnumerable<string> users)
        {
            var posts = Posts
                .Where(post => post.IsPostedByOneOf(users))
                .ToList();

            return new ReadOnlyCollection<Post>(posts);
        }
    }
}