using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SocNet.Storage
{
    public class FollowStore : IFollowStore
    {
        protected IDictionary<string, IList<string>> Follows { get; set; }

        public FollowStore()
        {
            Follows = new Dictionary<string, IList<string>>();
        }

        public void StoreFollow(string user, string whoToFollow)
        {
            if (!Follows.ContainsKey(user))
            {
                Follows[user] = new List<string> {whoToFollow};
            }
            else
            {
                var followed = Follows[user];
                if (!followed.Contains(whoToFollow))
                {
                    followed.Add(whoToFollow);
                }
            }
        }

        public IEnumerable<string> GetFollowedBy(string user)
        {
            if (!Follows.ContainsKey(user))
            {
                return Enumerable.Empty<string>();
            }

            return new ReadOnlyCollection<string>(Follows[user]);
        }
    }
}