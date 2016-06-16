using System.Text.RegularExpressions;

namespace SocNet.Commands
{
    public static class CommandFactory
    {
        public static Command CreateFrom(string command)
        {
            if (command.Contains("->"))
            {
                var match = Regex.Match(command, "^(?<user>.+)->(?<message>.+)$");
                var user = match.Groups["user"].Value.TrimEnd();
                var message = match.Groups["message"].Value.TrimStart();

                return new PostCommand(user, message);
            }

            if (command.Contains(" follows "))
            {
                var match = Regex.Match(command, "^(?<user>.+) follows (?<whoToFollow>.+)$");
                var user = match.Groups["user"].Value.TrimEnd();
                var whoToFollow = match.Groups["whoToFollow"].Value.TrimEnd();

                return new FollowCommand(user, whoToFollow);
            }

            if (command.EndsWith(" wall"))
            {
                var match = Regex.Match(command, "^(?<user>.+) wall$");
                var user = match.Groups["user"].Value.TrimEnd();

                return new WallCommand(user);
            }

            return new TimelineCommand(command);
        }
    }
}