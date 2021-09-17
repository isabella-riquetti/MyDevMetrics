using CombinedCodingStats.Model.Platform;

namespace CombinedCodingStats.Model.Theme
{
    public class DarkTheme<T> : IDarkTheme<T> where T : GitHubModel
    {
        public string Theme => Themes.Dark;

        public string BACKGROUND_COLOR => "#22272e";
        public string EMPTY_COLOR => "#2d333b";
        public string ACTIVITY_LEVEL_1_COLOR => "#0e4429";
        public string ACTIVITY_LEVEL_2_COLOR => "#006d32";
        public string ACTIVITY_LEVEL_3_COLOR => "#26a641";
        public string ACTIVITY_LEVEL_4_COLOR => "#39d353";

        public string GetTheme()
        {
            return this.Theme;
        }
    }
    public interface ITheme<T>
    {
        string BACKGROUND_COLOR { get; }
        string EMPTY_COLOR { get; }
        string ACTIVITY_LEVEL_1_COLOR { get; }
        string ACTIVITY_LEVEL_2_COLOR { get; }
        string ACTIVITY_LEVEL_3_COLOR { get; }
        string ACTIVITY_LEVEL_4_COLOR { get; }
    }

    public interface IDarkTheme<T> : ITheme<T>
    {
    }
}
