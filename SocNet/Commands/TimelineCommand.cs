using System;

namespace SocNet.Commands
{
    public class TimelineCommand : Command, IEquatable<TimelineCommand>
    {
        public string User { get; }

        public TimelineCommand(string user)
        {
            User = user;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            return Equals((TimelineCommand) obj);
        }

        public bool Equals(TimelineCommand obj)
        {
            return base.Equals(obj) &&
                this.User == obj.User;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // a dirty trick for lazy boys and girls, don't use in production though :P
                var hash = new { User }.GetHashCode();
                return base.GetHashCode() * hash;
            }
        }
    }
}