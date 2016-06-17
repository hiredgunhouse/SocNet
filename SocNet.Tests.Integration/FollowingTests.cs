using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SocNet.Clock;
using SocNet.Tests.Integration.Time;
using SocNet.Time;

namespace SocNet.Tests.Integration
{
    [TestFixture]
    public class FollowingTests : FeaturesTest
    {
        [SetUp]
        public void SetUp()
        {
            PostStore = new TestablePostStore();
            FollowStore = new TestableFollowStore();
            SocApp = new SocApp(PostStore, FollowStore, PostFormatter);
        }

        [Test]
        public void CharlieCanFollowAliceAndBobAndSeeTheirPostsInMyWall()
        {
            // arrange
            var now = new DateTime(2000, 1, 1);
            var later = now.AddSeconds(13);

            PostStore.SetPosts(Posts(
                CreatePost(Alice, "I love the weather today", 5.MinutesBefore(now)),
                CreatePost(Bob, "Damn! We lost!", 2.MinutesBefore(now)),
                CreatePost(Bob, "Good game though.", 1.MinutesBefore(now)),
                CreatePost(Charlie, "I'm in New York today! Anyone want to have a coffee?", 2.SecondsBefore(now))));

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.SetupSequence(x => x.Now())
                .Returns(now)
                .Returns(now)
                .Returns(later)
                .Returns(later)
                .Returns(later)
                .Returns(later);

            // act
            IEnumerable<string> wallWithAlice;
            IEnumerable<string> wallWithAliceAndBob;

            using (new DateTimeProviderContext(dateTimeProvider.Object))
            {
                Follow(Charlie, Alice);
                wallWithAlice = GetWall(Charlie);
                Follow(Charlie, Bob);
                wallWithAliceAndBob = GetWall(Charlie);
            }

            // assert
            wallWithAlice.Should().Equal(Wall(
                "Charlie - I'm in New York today! Anyone want to have a coffee? (2 seconds ago)",
                "Alice - I love the weather today (5 minutes ago)"));

            wallWithAliceAndBob.Should().Equal(Wall(
                "Charlie - I'm in New York today! Anyone want to have a coffee? (15 seconds ago)",
                "Bob - Good game though. (1 minute ago)",
                "Bob - Damn! We lost! (2 minutes ago)",
                "Alice - I love the weather today (5 minutes ago)"));

            dateTimeProvider.VerifyAll();
        }
    }
}