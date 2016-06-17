using System;
using System.Globalization;
using SocNet.Clock;
using SocNet.Model;
using TimeAgo;

namespace SocNet.Formatting
{
    public class PostFormatter : IPostFormatter
    {
        public string FormatAsTimelineEntry(Post post)
        {
            return $"{post.Message} ({TimeAgo(post.Date)})";
        }

        public string FormatAsWallEntry(Post post)
        {
            return $"{post.User} - {post.Message} ({TimeAgo(post.Date)})";
        }

        private string TimeAgo(DateTime date)
        {
            var now = DateTimeProvider.Now;
            return date.TimeAgo(now, CultureInfo.CurrentUICulture);
        }
    }
}