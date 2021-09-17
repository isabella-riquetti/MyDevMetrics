using CombinedCodingStats.Handler.Theme;
using CombinedCodingStats.Model.Theme;
using System;

namespace CombinedCodingStats.Model.Platform
{
    public class GitHubService : IGitHubService
    {
		private readonly IGitHubModel _gitHub;
        private readonly IThemeHandler<GitHubModel> _themeHandler;
        private ITheme<GitHubModel> theme;

        public GitHubService(IGitHubModel platform, IThemeHandler<GitHubModel> themeHandler)
        {
			_gitHub = platform;
            _themeHandler = themeHandler;
        }

        public void SetTheme(string theme)
        {
            this.theme = _themeHandler.Handle(theme);
        }

        public string GetDateSvgSquare(DateTime date, int activity, int horizontal, int vertical)
        {
            string color = _gitHub.GetColor(this.theme, activity);

            return $"<rect x=\"{horizontal * _gitHub.SQUARE_DISTANCE}\" y=\"{vertical * _gitHub.SQUARE_DISTANCE}\" width=\"{_gitHub.SQUARE_SIZE}\" height=\"{_gitHub.SQUARE_SIZE}\" " +
				$"r=\"{_gitHub.SQUARE_ROUNDING}\" rx=\"{_gitHub.SQUARE_ROUNDING}\" ry=\"{_gitHub.SQUARE_ROUNDING}\" fill=\"{color}\" stroke=\"none\" style=\"-webkit-tap-highlight-color: rgba(0, 0, 0, 0);\"></rect>";
		}
    }

    public interface IGitHubService : IPlatformService
    {
    }

    public interface IPlatformService
    {
        string GetDateSvgSquare(DateTime date, int activity, int horizontal, int vertical);
        void SetTheme(string theme);
    }
}
