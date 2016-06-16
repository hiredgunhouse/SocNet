using FluentAssertions;
using NUnit.Framework;
using SocNet.Time;

namespace SocNet.Tests.Integration
{
    [TestFixture]
    public class TimelineTests : FeaturesTestBase
    {
        [SetUp]
        public void SetUp()
        {
            PostStore = new TestablePostStore();
            FollowStore = new TestableFollowStore();
            SocApp = new SocApp(PostStore, FollowStore, PostFormatter);
        }

        [Test]
        public void AliceAndBobsTimelinesCanBeViewed()
        {
            // arrange
            PostStore.SetPosts(Posts(
                CreatePost(Alice, "I love the weather today", 5.MinutesAgo()),
                CreatePost(Bob, "Damn! We lost!", 2.MinutesAgo()),
                CreatePost(Bob, "Good game though.", 1.MinutesAgo())));

            // act
            var aliceTimeline = GetTimeLine(Alice);
            var bobTimeline = GetTimeLine(Bob);

            // assert
            aliceTimeline.Should().Equal(Timeline(
                "I love the weather today (5 minutes ago)"));
            bobTimeline.Should().Equal(Timeline(
                "Good game though. (1 minute ago)",
                "Damn! We lost! (2 minutes ago)"));
        }
    }
}