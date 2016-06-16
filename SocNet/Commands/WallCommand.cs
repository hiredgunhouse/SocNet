namespace SocNet.Commands
{
    public class WallCommand : Command
    {
        public string User { get; }

        public WallCommand(string user)
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

            return Equals((WallCommand) obj);
        }

        public bool Equals(WallCommand obj)
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