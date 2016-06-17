using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SocNet.Storage;
using SocNet.Tests.Unit.Assert;

namespace SocNet.Tests.Unit.Storage
{
    [TestFixture]
    public class FollowStoreTests
    {
        protected readonly string Alice = "Alice";
        protected readonly string Bob = "Bob";
        protected readonly string Charlie = "Charlie";

        private TestableFollowStore _followStore;

        [SetUp]
        public void SetUp()
        {
           _followStore = new TestableFollowStore(); 
        }

        [TestCase("Charlie", "Alice")]
        [TestCase("Charlie", "Bob")]
        public void StoreFollow_GivenAUserAndWhoToFollow_StoresTheDetails(string user, string whoToFollow)
        {
            // arrange

            // act
            _followStore.StoreFollow(user, whoToFollow);

            // assert
            var follows = _followStore.GetFollows();
            follows.ShouldInclude(Charlie);
            follows.For(Charlie).Should().Equal(Followed(whoToFollow));
        }

        [Test]
        public void StoreFollow_CalledTwiceGivenTheSameUserAndTwoDifferentWhoToFollowValues_StoresBoth()
        {
            // arrange

            // act
            _followStore.StoreFollow(Charlie, Alice);
            _followStore.StoreFollow(Charlie, Bob);

            // assert
            var follows = _followStore.GetFollows();
            follows.ShouldInclude(Charlie);
            follows.For(Charlie).Should().Equal(Followed(Alice, Bob));
        }

        [Test]
        public void StoreFollow_CalledTwiceGivenTheSameUserAndTheSameWhoToFollowValues_StoresOnlyOnce()
        {
            // arrange

            // act
            _followStore.StoreFollow(Charlie, Alice);
            _followStore.StoreFollow(Charlie, Alice);

            // assert
            var follows = _followStore.GetFollows();
            follows.ShouldInclude(Charlie);
            follows.For(Charlie).Should().Equal(Followed(Alice));
        }

        [TestCase("Alice")]
        [TestCase("Bob")]
        [TestCase("Charlie")]
        public void GetFollowedBy_WhenPassedAUserThatDoesNotFollowAnyone_ReturnsAnEmptyList(string user)
        {
            // arrange

            // act
            var followed = _followStore.GetFollowedBy(user);

            // assert
            followed.Should().BeEmpty();
        }

        [TestCase("Alice", "Bob")]
        [TestCase("Alice", "Bob", "Charlie")]
        [TestCase("Bob", "Alice")]
        [TestCase("Bob", "Alice", "Charlie")]
        [TestCase("Charlie", "Alice")]
        [TestCase("Charlie", "Alice", "Bob")]
        public void GetFollowedBy_WhenPassedAUserThatDoesFollowOthers_ReturnsCorrectList(string user, params string[] toBeFollowed)
        {
            // arrange
            _followStore.SetFollows(FollowDict(
                FollowKVP(user, Followed(toBeFollowed))));

            // act
            var followed = _followStore.GetFollowedBy(user);

            // assert
            followed.Should().Equal(toBeFollowed);
        }

        private IDictionary<string, IList<string>> FollowDict(params KeyValuePair<string, IList<string>>[] follows)
        {
            return follows.ToDictionary(x => x.Key, x => x.Value);
        }

        private KeyValuePair<string, IList<string>> FollowKVP(string user, IEnumerable<string> followed)
        {
            return new KeyValuePair<string, IList<string>>(user, followed.ToList());
        }

        private IEnumerable<string> Followed(params string[] followed)
        {
            return followed;
        }

        private class TestableFollowStore : FollowStore
        {
            public IDictionary<string, IList<string>> GetFollows()
            {
                return Follows;
            }

            public void SetFollows(IDictionary<string, IList<string>> follows)
            {
                Follows = follows;
            }
        }
    }
}