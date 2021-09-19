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


        public GitLabMetricsOptions GetParameters()
        {
            var platform = _GetPlatform();
            var theme = _GetTheme(platform);

            return new GitLabMetricsOptions()
            {
                Platform = platform,
                Theme = theme,
                BackgroundEnabled = _IsBackgroundEnabled(),
                AnimationEnabled = _IsAnimationEnabled()
            }; ;
        }

        private Platform _GetPlatform()
        {
            string platformName = Platforms.DefaultName;
            if (Platforms.ValidNames.Contains(Platform))
                platformName = Platform;

            return _platformThemeConfiguration[platformName];
        }

        private Theme _GetTheme(Platform platform)
        {
            string themeName = Themes.DefaultName;
            if (Themes.ValidNames.Contains(Theme))
                themeName = Theme;

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
            if (status.IsPositive())
                return true;

            if (status.IsNegative())
                return false;

            return null;
        }
    }

    public static class Platforms
    {
        public const string GitLab = "gitlab";
        public const string GitHub = "github";

        public const string DefaultName = Platforms.GitLab;
        public static List<string> ValidNames = new List<string>()
        {
            Platforms.GitLab,
            Platforms.GitHub
        };
    }

    public static class Themes
    {
        public const string Light = "light";
        public const string Dark = "dark";

        public const string DefaultName = Themes.Light;
        public static List<string> ValidNames = new List<string>()
        {
            Themes.Light,
            Themes.Dark
        };
    }

    public static class BackgroundStatus
    {
        public const bool DefaultStatus = true;
    }

    public static class AnimationStatus
    {
        public const bool DefaultStatus = true;
    }
}
