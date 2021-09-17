
namespace CombinedCodingStats.Model.Theme
{
    public abstract class ThemeBase<T>
    {
        public abstract string BACKGROUND_COLOR { get; }
        public abstract string EMPTY_COLOR { get; }
        public abstract string ACTIVITY_LEVEL_1_COLOR { get; }
        public abstract string ACTIVITY_LEVEL_2_COLOR { get; }
        public abstract string ACTIVITY_LEVEL_3_COLOR { get; }
        public abstract string ACTIVITY_LEVEL_4_COLOR { get; }
    }
}
