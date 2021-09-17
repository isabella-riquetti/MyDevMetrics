using CombinedCodingStats.Model.Theme;

namespace CombinedCodingStats.Model.Platform
{
    public interface IPlatform
    {
        int ACTIVITY_LEVEL_1 { get; }
        int ACTIVITY_LEVEL_2 { get; }

        string GetColor(ITheme<GitHubModel> theme, int activity);

        int ACTIVITY_LEVEL_3 { get; }
        int ACTIVITY_LEVEL_4 { get; }

        int SQUARE_SIZE { get; }
        int SQUARE_SPACING { get; }
        int SQUARE_DISTANCE { get; }
        int SQUARE_ROUNDING { get; }

    }
}
