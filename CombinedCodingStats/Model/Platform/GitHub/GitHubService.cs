using CombinedCodingStats.Handler.Theme;
using CombinedCodingStats.Infraestructure;
using CombinedCodingStats.Model.Theme;
using System;

namespace CombinedCodingStats.Model.Platform
{
    public class GitHubService : IGitHubService
    {
        private readonly ISVGService _svgService;
        private readonly IGitHubModel _gitHub;
        private readonly IThemeHandler<GitHubModel> _themeHandler;
        private ITheme<GitHubModel> theme;

        public GitHubService(ISVGService svgService, IGitHubModel platform, IThemeHandler<GitHubModel> themeHandler)
        {
            _svgService = svgService;
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

            return String.Format(_svgService.GetActivitySquare(), 
                horizontal * _gitHub.SQUARE_DISTANCE,
                vertical * _gitHub.SQUARE_DISTANCE,
                _gitHub.SQUARE_SIZE,
                _gitHub.SQUARE_ROUNDING,
                color);
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
