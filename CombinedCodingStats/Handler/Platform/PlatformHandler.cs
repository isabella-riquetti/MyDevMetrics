using CombinedCodingStats.Model.Platform;
using System.Collections.Generic;

namespace CombinedCodingStats.Handler.Theme
{
    public class PlatformHandler : IPlatformHandler
    {
        private readonly IGitHubService _gitHubService;
        private Dictionary<string, IPlatformService> _platforms = new Dictionary<string, IPlatformService>();

        public PlatformHandler(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
            _platforms.Add(Platforms.GitHub, _gitHubService);
        }

        public IPlatformService Handle(string platform)
        {
            if(!_platforms.ContainsKey(platform.ToUpper()))
            {
                throw new System.Exception("Platform not found");
            }

            return _platforms[platform.ToUpper()];
        }
    }
}
