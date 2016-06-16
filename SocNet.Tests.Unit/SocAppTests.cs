using System;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Moq;
using NUnit.Framework;
using SocNet.Model;
using SocNet.Storage;

namespace SocNet.Tests.Unit
{
    [TestFixture]
    public class SocAppTests
    {
        protected readonly string Alice = "Alice";
        protected readonly string Bob = "Bob";
        protected readonly string Charlie = "Charlie";

        private AutoMoqer _mocker;
        private SocApp _socApp;
        private Mock<IPostStore> _postStore;
        private Mock<IFollowStore> _followStore;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMoqer();
            _socApp = _mocker.Create<SocApp>();
            _postStore = _mocker.GetMock<IPostStore>();
            _followStore = _mocker.GetMock<IFollowStore>();
        }

        [Test]
        [TestCase("Alice", "I love the weather today")]
        [TestCase("Bob", "Damn! We lost!")]
        [TestCase("Bob", "Good game though.")]
        public void Command_GivenAPostCommand_SavesItInPostStore(string user, string message)
        {
            // arrange
            var post = Post(user, message);

            // act
            _socApp.Execute($"{user} -> {message}");

            // assert
            _postStore.Verify(x => x.StorePost(It.Is<Post>(p => post.User == user && post.Message == message)));
        }

        [Test]
        [TestCase("Alice")]
        [TestCase("Bob")]
        [TestCase("Charlie")]
        public void Command_GivenATimelineCommand_ReadsFromPostStore(string user)
        {
            // arrange

            // act
            _socApp.Execute(user);

            // assert
            _postStore.Verify(x => x.GetPostsByUser(It.Is<string>(u => u == user)));
        }

        [Test]
        [TestCase("Charlie", "Alice")]
        [TestCase("Charlie", "Bob")]
        public void Command_GivenAFollowCommand_SavesItInFollowStore(string user, string whoToFollow)
        {
            // arrange

            // act
            _socApp.Execute($"{user} follows {whoToFollow}");

            // assert
            _followStore.Verify(x => x.StoreFollow(
                It.Is<string>(u => u == user), 
                It.Is<string>(w => w == whoToFollow))); 
        }

        [Test]
        [TestCase("Alice")]
        [TestCase("Bob")]
        [TestCase("Charlie")]
        public void Command_GivenAWallCommand_LoadsFollowed(string user)
        {
            // arrange

            // act
            _socApp.Execute($"{user} wall");

            // assert
            _followStore.Verify(x => x.GetFollowedBy(It.Is<string>(u => u == user))); 
        }

        [TestCase("Alice")]
        [TestCase("Bob")]
        [TestCase("Charlie")]
        public void Command_GivenAWallCommand_LoadsOwnPostsFromPostStoreIfUserIsNotFollowingAnyone(string user)
        {
            // arrange
            _followStore
                .Setup(x => x.GetFollowedBy(It.IsAny<string>()))
                .Returns<string>(x => Enumerable.Empty<string>());

            // act
            _socApp.Execute($"{user} wall");

            // assert
            _postStore.Verify(x => x.GetPostsByUsers(
                It.Is<IEnumerable<string>>(users => users.SequenceEqual(Users(user)))));
        }

        [Test]
        public void Command_GivenAWallCommand_LoadsPostsFromUserAndAllUsersThatHeOrSheFollows()
        {
            // arrange
            _followStore
                .Setup(x => x.GetFollowedBy(It.IsAny<string>()))
                .Returns<string>(x => Users(Alice, Bob));

            // act
            _socApp.Execute($"{Charlie} wall");

            // assert
            _postStore.Verify(x => x.GetPostsByUsers(
                It.Is<IEnumerable<string>>(users => users.SequenceEqual(Users(Alice, Bob, Charlie)))));
        }

        private IEnumerable<string> Users(params string[] users)
        {
            return users;
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