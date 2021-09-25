using CombinedCodingStats.Constants;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CombinedCodingStats.Model.GitLab
{
    public class GitLabMetricsQueryParameters
    {
        private readonly Dictionary<string, Platform> _platformThemeConfiguration;
        public GitLabMetricsQueryParameters()
        {
            var data = System.IO.File.ReadAllText(@"./content/platform_themes_configuration.json");
            _platformThemeConfiguration = JsonConvert.DeserializeObject<Dictionary<string, Platform>>(data);
        }

        public string Platform { get; set; }

        public string Theme { get; set; }

        public string Background { get; set; }

        public string Animation { get; set; }


        public GitLabMetricsOptions GetOptions()
        {
            var platform = _GetPlatform();
            var theme = _GetTheme(platform);

            return new GitLabMetricsOptions()
            {
                Platform = platform,
                Theme = theme,
                BackgroundEnabled = _IsBackgroundEnabled(),
                AnimationEnabled = _IsAnimationEnabled()
            };
        }

        private Platform _GetPlatform()
        {
            string platformName = Platforms.DefaultName;
            if (Platform.HasValue() && _platformThemeConfiguration.ContainsKey(Platform.ToLower()))
                platformName = Platform.ToLower();

            return _platformThemeConfiguration[platformName];
        }

        private Theme _GetTheme(Platform platform)
        {
            string themeName = Themes.DefaultName;
            if (Theme.HasValue() && platform.Themes.ContainsKey(Theme.ToLower()))
                themeName = Theme.ToLower();

            return platform.Themes[themeName];
        }

        private bool _IsBackgroundEnabled()
        {
            return _IsEnabled(Background) ?? AnimationStatus.DefaultStatus;
        }

        private bool _IsAnimationEnabled()
        {
            return _IsEnabled(Animation) ?? AnimationStatus.DefaultStatus;
        }

        private bool? _IsEnabled(string status)
        {
            if(!status.HasValue())
                return null;

            if (status.IsPositive())
                return true;

            if (status.IsNegative())
                return false;

            return null;
        }
    }
}
