using System.Collections.Generic;
using System.Linq;
using SocNet.Model;

namespace SocNet.Storage
{
    public static class PostExtensions
    {
        public static bool IsPostedByOneOf(this Post obj, IEnumerable<string> users)
        {
            return users.Contains(obj.User);
        }
    }
}