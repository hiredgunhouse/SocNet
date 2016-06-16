using SocNet.Model;

namespace SocNet.Formatting
{
    public interface IPostFormatter
    {
        string FormatAsTimelineEntry(Post post);
        string FormatAsWallEntry(Post post);
    }
}