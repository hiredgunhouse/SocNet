using System;
using FluentAssertions;
using NUnit.Framework;
using SocNet.Formatting;
using SocNet.Model;
using SocNet.Time;

namespace SocNet.Tests.Unit.Formatting
{
    [TestFixture]
    public class PostFormatterTests
    {
        protected readonly string Alice = "Alice";
        protected readonly string Bob = "Bob";
        protected readonly string Charlie = "Charlie";

        private PostFormatter _postFormatter;

        [SetUp]
        public void SetUp()
        {
            _postFormatter = new PostFormatter();
        }

        [Test]
        public void FormatAsTimelineEntry_GivenAPost_ReturnsNicelyFormattedEntry()
        {
            // arrange
            var post = Post(Alice, "I love the weather today", 1.MinutesAgo());
            var expected = $"{post.Message} (1 minute ago)";

            // act
            var formattedTimelineEntry = _postFormatter.FormatAsTimelineEntry(post);

            // assert
            formattedTimelineEntry.Should().Be(expected);
        }

        [Test]
        public void FormatAsWallEntry_GivenAPost_ReturnsNicelyFormattedEntry()
        {
            // arrange
            var post = Post(Alice, "I love the weather today", 1.MinutesAgo());
            var expected = $"{post.User} - {post.Message} (1 minute ago)";

            // act
            var formattedTimelineEntry = _postFormatter.FormatAsWallEntry(post);

            // assert
            formattedTimelineEntry.Should().Be(expected);
        }

        private Post Post(string user, string message, DateTime? date = null)
        {
            return new Post
            {
                Date = date ?? DateTimeProvider.Now,
                User = user,
                Message = message,
            };
        }
    }
}