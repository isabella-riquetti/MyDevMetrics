using CombinedCodingStats.Model.Platform;
using CombinedCodingStats.Model.Theme;
using System.Collections.Generic;

namespace CombinedCodingStats.Handler.Theme
{
    public class ThemeHandler<T> : IThemeHandler<T>
    {
        private readonly IDarkTheme<T> _darkTheme;
        private Dictionary<string, ITheme<T>> _themes = new Dictionary<string, ITheme<T>>();

        public ThemeHandler(IDarkTheme<T> darkTheme)
        {
            _darkTheme = darkTheme;
            _themes.Add(Themes.Dark, _darkTheme);
        }

        public ITheme<T> Handle(string theme)
        {
            if(!_themes.ContainsKey(theme.ToUpper()))
            {
                throw new System.Exception("Theme not found");
            }

            return _themes[theme.ToUpper()];
        }
    }
}
