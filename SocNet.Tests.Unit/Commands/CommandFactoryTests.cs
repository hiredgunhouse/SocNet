using FluentAssertions;
using NUnit.Framework;
using SocNet.Commands;

namespace SocNet.Tests.Unit.Commands
{
    [TestFixture]
    public class CommandFactoryTests
    {
        protected readonly string Alice = "Alice";
        protected readonly string Charlie = "Charlie";

        [Test]
        public void CreateFrom_GivenAPostCommand_RecognizesItCorrectly()
        {
            // arrange
            var user = Alice;
            var message = "I love the weather today";
            var postCommand = $"{user} -> {message}";

            // act 
            var command = CommandFactory.CreateFrom(postCommand);

            // assert
            var expectedCommand = new PostCommand(user, message);
            command.Should().Be(expectedCommand);
        }

        [Test]
        public void CreateFrom_GivenATimelineCommand_RecognizesItCorrectly()
        {
            // arrange
            var user = Alice;
            var timelineCommand = $"{user}";

            // act 
            var command = CommandFactory.CreateFrom(timelineCommand);

            // assert
            var expectedCommand = new TimelineCommand(user);
            command.Should().Be(expectedCommand);
        }

        [Test]
        public void CreateFrom_GivenAFollowCommand_RecognizesItCorrectly()
        {
            // arrange
            var user = Charlie;
            var whoToFollow = Alice;
            var followCommand = $"{user} follows {whoToFollow}";

            // act 
            var command = CommandFactory.CreateFrom(followCommand);

            // assert
            var expectedCommand = new FollowCommand(user, whoToFollow);
            command.Should().Be(expectedCommand);
        }

        [Test]
        public void CreateFrom_GivenAWallCommand_RecognizesItCorrectly()
        {
            // arrange
            var user = Charlie;
            var wallCommand = $"{user} wall";

            // act 
            var command = CommandFactory.CreateFrom(wallCommand);

            // assert
            var expectedCommand = new WallCommand(user);
            command.Should().Be(expectedCommand);
        }
    }
}