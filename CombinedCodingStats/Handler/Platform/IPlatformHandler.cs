using CombinedCodingStats.Model.Platform;

namespace CombinedCodingStats.Handler.Theme
{
    public interface IPlatformHandler
    {
        IPlatformService Handle(string platform);
    }
}
