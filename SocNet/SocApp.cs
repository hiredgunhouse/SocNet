using System;
using System.Collections.Generic;
using System.Linq;
using SocNet.Commands;
using SocNet.Formatting;
using SocNet.Model;
using SocNet.Storage;

namespace SocNet
{
    public class SocApp
    {
        private readonly IPostStore _postStore;
        private readonly IFollowStore _followStore;
        private readonly IPostFormatter _postFormatter;

        public SocApp(
            IPostStore postStore,
            IFollowStore followStore, 
            IPostFormatter postFormatter)
        {
            _postStore = postStore;
            _followStore = followStore;
            _postFormatter = postFormatter;
        }

        public IEnumerable<string> Execute(string command)
        {
            var internalCommand = CommandFactory.CreateFrom(command);

            var postCommand = internalCommand as PostCommand;
            if (postCommand != null)
            {
                StorePost(postCommand);
            }

            var timelineCommand = internalCommand as TimelineCommand;
            if (timelineCommand != null)
            {
                return GetTimeline(timelineCommand);
            }

            var followCommand = internalCommand as FollowCommand;
            if (followCommand != null)
            {
                Follow(followCommand);
            }

            var wallCommand = internalCommand as WallCommand;
            if (wallCommand != null)
            {
                return Wall(wallCommand);
            }

            return null;
        }

        private IEnumerable<string> Wall(WallCommand wallCommand)
        {
            var followed = _followStore.GetFollowedBy(wallCommand.User);
            var users = followed.Concat(new[] {wallCommand.User});

            return _postStore
                .GetPostsByUsers(users)
                .OrderByDescending(x => x.Date)
                .Select(_postFormatter.FormatAsWallEntry)
                .ToList();
        }

        private void Follow(FollowCommand followCommand)
        {
            _followStore.StoreFollow(followCommand.User, followCommand.WhoToFollow);
        }

        private IEnumerable<string> GetTimeline(TimelineCommand timelineCommand)
        {
            var posts = _postStore.GetPostsByUser(timelineCommand.User);

            return posts
                .OrderByDescending(p => p.Date)
                .Select(_postFormatter.FormatAsTimelineEntry)
                .ToList();
        }

        private void StorePost(PostCommand postCommand)
        {
            _postStore.StorePost(PostFactory.CreateFrom(postCommand));
        }
    }
}