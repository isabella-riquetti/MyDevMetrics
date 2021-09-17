using CombinedCodingStats.Model.Theme;

namespace CombinedCodingStats.Handler.Theme
{
    public interface IThemeHandler<T>
    {
        ITheme<T> Handle(string theme);
    }
}
