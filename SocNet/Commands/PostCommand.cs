using System;

namespace SocNet.Commands
{
    public class PostCommand : Command, IEquatable<PostCommand>
    {
        public string User { get; }
        public string Message { get; }

        public PostCommand(string user, string message)
        {
            User = user;
            Message = message;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            return Equals((PostCommand) obj);
        }

        public bool Equals(PostCommand obj)
        {
            return base.Equals(obj) &&
                this.User == obj.User &&
                this.Message == obj.Message;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // a dirty trick for lazy boys and girls, don't use in production though :P
                var hash = new { User, Message }.GetHashCode();
                return base.GetHashCode() * hash;
            }
        }
    }
}