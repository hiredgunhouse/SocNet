using System;

namespace SocNet.Commands
{
    public class FollowCommand : Command, IEquatable<FollowCommand>
    {
        public string User { get; }
        public string WhoToFollow { get; }

        public FollowCommand(string user, string whoToFollow)
        {
            User = user;
            WhoToFollow = whoToFollow;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            return Equals((FollowCommand) obj);
        }

        public bool Equals(FollowCommand obj)
        {
            return base.Equals(obj) &&
                this.User == obj.User &&
                this.WhoToFollow == obj.WhoToFollow;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // a dirty trick for lazy boys and girls, don't use in production though :P
                var hash = new { User, WhoToFollow }.GetHashCode();
                return base.GetHashCode() * hash;
            }
        }
    }
}