namespace SocNet.Commands
{
    public abstract class Command
    {
        public bool Equals(Command other)
        {
            // nothing to compare so far structurally
            return true;
        }
    }
}