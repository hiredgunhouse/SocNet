using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SocNet.Clock;
using SocNet.Time;

namespace SocNet.Tests.Unit.Time
{
    [TestFixture]
    public class TimeExtensionsTests
    {
        [Test]
        public void MinutesAgo_GivenAValue_SubtractsSpecifiedAmountOfMinutesFromNow()
        {
            // arrange
            var minutes = 5;
            var now = new DateTime(2000, 1, 1, 1, 10, 0);
            var ago = new DateTime(2000, 1, 1, 1, 5, 0);

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.SetupSequence(x => x.Now()).Returns(now);

            DateTimeProvider.SetCustomProvider(dateTimeProvider.Object);

            // act
            var timeAgo = TimeExtensions.MinutesAgo(minutes);

            // assert
            timeAgo.Should().Be(ago);
        }

        [Test]
        public void MinutesBefore_GivenAValueAndADate_SubtractsSpecifiedAmountOfMinutesFromSpecifiedDate()
        {
            // arrange
            var minutes = 5;
            var now = new DateTime(2000, 1, 1, 1, 10, 0);
            var ago = new DateTime(2000, 1, 1, 1, 5, 0);

            // act
            var timeAgo = TimeExtensions.MinutesBefore(minutes, now);

            // assert
            timeAgo.Should().Be(ago);
        }

        [Test]
        public void SecondsAgo_GivenAValue_SubtractsSpecifiedAmountOfSecondsFromNow()
        {
            // arrange
            var seconds = 15;
            var now = new DateTime(2000, 1, 1, 1, 1, 30);
            var ago = new DateTime(2000, 1, 1, 1, 1, 15);

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.SetupSequence(x => x.Now()).Returns(now);

            DateTimeProvider.SetCustomProvider(dateTimeProvider.Object);

            // act
            var timeAgo = TimeExtensions.SecondsAgo(seconds);

            // assert
            timeAgo.Should().Be(ago);
        }

        [Test]
        public void SecondsBefore_GivenAValueAndADate_SubtractsSpecifiedAmountOfSecondsFromSpecifiedDate()
        {
            // arrange
            var seconds = 15;
            var now = new DateTime(2000, 1, 1, 1, 1, 30);
            var ago = new DateTime(2000, 1, 1, 1, 1, 15);

            // act
            var timeAgo = TimeExtensions.SecondsBefore(seconds, now);

            // assert
            timeAgo.Should().Be(ago);
        }
    }
}