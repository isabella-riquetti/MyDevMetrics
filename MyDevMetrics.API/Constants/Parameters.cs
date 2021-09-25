using System.Collections.Generic;

namespace CombinedCodingStats.Constants
{
    public static class Platforms
    {
        public const string GitLab = "gitlab";
        public const string GitHub = "github";

        public const string DefaultName = Platforms.GitLab;
    }

    public static class Themes
    {
        public const string Light = "light";
        public const string Dark = "dark";

        public const string DefaultName = Themes.Light;
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
