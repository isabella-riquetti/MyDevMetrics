using CombinedCodingStats.Model.Theme;

namespace CombinedCodingStats.Model.Platform
{
    public abstract class Platform : IPlatform
    {
        public virtual int ACTIVITY_LEVEL_1 => 0;
        public virtual int ACTIVITY_LEVEL_2 => 0;
        public virtual int ACTIVITY_LEVEL_3 => 0;
        public virtual int ACTIVITY_LEVEL_4 => 0;

        public virtual int SQUARE_SIZE => 0;
        public virtual int SQUARE_SPACING => 0;
        public virtual int SQUARE_DISTANCE => SQUARE_SIZE + SQUARE_SPACING;
        public virtual int SQUARE_ROUNDING => 0;

        public string GetColor(ITheme<GitHubModel> theme, int activity)
        {
            if (activity > ACTIVITY_LEVEL_4)
            {
                return theme.ACTIVITY_LEVEL_4_COLOR;
            }
            if (activity > ACTIVITY_LEVEL_3)
            {
                return theme.ACTIVITY_LEVEL_3_COLOR;
            }
            if (activity > ACTIVITY_LEVEL_2)
            {
                return theme.ACTIVITY_LEVEL_2_COLOR;
            }
            if (activity > ACTIVITY_LEVEL_1)
            {
                return theme.ACTIVITY_LEVEL_1_COLOR;
            }

            return theme.EMPTY_COLOR;
        }
    }
}
