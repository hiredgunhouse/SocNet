using System;
using System.Collections.Generic;
using SocNet.Formatting;
using SocNet.Model;
using SocNet.Storage;

namespace SocNet.Tests.Integration
{
    public abstract class FeaturesTest
    {
        protected readonly string Alice = "Alice";
        protected readonly string Bob = "Bob";
        protected readonly string Charlie = "Charlie";

        protected SocApp SocApp { get; set; }
        protected TestablePostStore PostStore { get; set; }
        protected TestableFollowStore FollowStore { get; set; }
        protected PostFormatter PostFormatter { get; set; }


        public FeaturesTest()
        {
            PostFormatter = new PostFormatter();
        }

        protected void Post(string user, string message)
        {
            string postCommand = $"{user} -> {message}";
            SocApp.Execute(postCommand);
        }

        protected IEnumerable<Post> Posts(params Post[] posts)
        {
            return posts;
        }

        protected Post CreatePost(string user, string message, DateTime date)
        {
            return new Post
            {
                User = user,
                Message = message,
                Date = date,
            };
        }

        protected void Follow(string user, string whoToFollow)
        {
            string followCommand = $"{user} follows {whoToFollow}";
            SocApp.Execute(followCommand);
        }

        protected IEnumerable<string> Wall(params string[] wall)
        {
            return wall;
        }

        protected IEnumerable<string> GetWall(string user)
        {
            return SocApp.Execute($"{user} wall");
        }

        protected IEnumerable<string> Timeline(params string[] timeline)
        {
            return timeline;
        }

        protected IEnumerable<string> GetTimeLine(string user)
        {
            return SocApp.Execute(user);
        }

        protected class TestablePostStore : PostStore
        {
            public void SetPosts(IEnumerable<Post> posts)
            {
                foreach (var post in posts)
                    Posts.Add(post);
            }
        }

        protected class TestableFollowStore : FollowStore
        {
        }
    }
}