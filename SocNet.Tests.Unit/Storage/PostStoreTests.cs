using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SocNet.Model;
using SocNet.Storage;

namespace SocNet.Tests.Unit.Storage
{
    [TestFixture]
    public class PostStoreTests
    {
        protected readonly string Alice = "Alice";
        protected readonly string Bob = "Bob";
        protected readonly string Charlie = "Charlie";

        private TestablePostStore _postStore;

        [SetUp]
        public void SetUp()
        {
           _postStore = new TestablePostStore(); 
        }

        [Test]
        public void StorePost_GivenAPost_StoresIt()
        {
            // arrange
            var post = Post(Alice, "I love the weather today");

            // act
            _postStore.StorePost(post);

            // assert
            var posts = _postStore.GetPosts();
            posts.Should().Contain(post);
        }

        [Test]
        public void GetPostsByUser_GivenAUser_ReturnsOnlyItsPosts()
        {
            // arrange
            var postByAlice = Post(Alice, "I love the weather today");

            _postStore.SetPosts(Posts(
                postByAlice,
                Post(Bob, "Damn! We lost!")));

            // act
            var posts = _postStore.GetPostsByUser(Alice);

            // assert
            posts.Should().Equal(postByAlice);
        }

        [Test]
        public void GetPostsByUser_GivenAUserThatHasNoPosts_ReturnsEmptyList()
        {
            // arrange
            _postStore.SetPosts(Posts(
                Post(Alice, "I love the weather today"),
                Post(Bob, "Damn! We lost!")));

            // act
            var posts = _postStore.GetPostsByUser(Charlie);

            // assert
            posts.Should().BeEmpty();
        }

        [Test]
        public void GetPostsByUsers_GivenAUserList_ReturnsOnlyTheirPosts()
        {
            // arrange
            var postByAlice = Post(Alice, "I love the weather today");
            var postByCharlie = Post(Charlie, "I'm in New York today! Anyone want to have a coffee?");

            _postStore.SetPosts(Posts(
                postByAlice,
                Post(Bob, "Damn! We lost!"),
                postByCharlie));

            // act
            var posts = _postStore.GetPostsByUsers(Users(Alice, Charlie));

            // assert
            posts.Should().BeEquivalentTo(Posts(postByAlice, postByCharlie));
        }

        private IEnumerable<string> Users(params string[] users)
        {
            return users;
        }

        protected IList<Post> Posts(params Post[] posts)
        {
            return posts.ToList();
        }

        protected Post Post(string user, string message)
        {
            return new Post
            {
                User = user,
                Message = message,
            };
        }

        private class TestablePostStore : PostStore
        {
            public IList<Post> GetPosts()
            {
                return Posts;
            }

            public void SetPosts(IList<Post> posts)
            {
                Posts = posts;
            }
        }
    }
}