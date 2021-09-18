using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using CombinedCodingStats.Infraestructure;
using System.Linq;
using CombinedCodingStats.Helper;

namespace CombinedCodingStats.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitLabController : ControllerBase
    {
		private readonly ISVGService _svgService;

		private const string _apiUrl = "https://gitlab.com/users/{0}/calendar.json";
		private readonly Dictionary<string, Platform> _platformThemeConfiguration;

        public GitLabController(ISVGService svgService)
        {
			var data = System.IO.File.ReadAllText(@"./content/platform_themes_configuration.json");
			_platformThemeConfiguration = JsonConvert.DeserializeObject<Dictionary<string, Platform>>(data);

			_svgService = svgService;
		}

		[HttpGet]
		[Route("{user}")]
		public IActionResult Get(string user, [FromQuery] Dictionary<string, string> parameters)
		{
			var parametersLowerCase = parameters.ToDictionary(x => x.Key?.ToLower(), x => x.Value?.ToLower());

			var activityPerDayResponse = new WebClient().DownloadString(String.Format(_apiUrl, user));
			var activityPerDay = JsonConvert.DeserializeObject<Dictionary<DateTime, int>>(activityPerDayResponse);

			string platformName = parametersLowerCase.GetValueOrDefault("platform", DefaultParameter.PLATFORM);
			string themeName = parametersLowerCase.GetValueOrDefault("theme", DefaultParameter.THEME);
			string animationResponse = parametersLowerCase.GetValueOrDefault("animation", DefaultParameter.ANIMATION);
			string backgroundResponse = parametersLowerCase.GetValueOrDefault("background", DefaultParameter.BACKGROUND);

			Platform platform = 
				_platformThemeConfiguration.GetValueOrDefault(platformName, _platformThemeConfiguration[DefaultParameter.PLATFORM]);
			Theme theme = 
				platform.Themes.GetValueOrDefault(themeName, platform.Themes[DefaultParameter.THEME]);

			var svg = _svgService.BuildGraph(
				activityPerDay,
				platform,
				theme,
				animationEnabled: !animationResponse.IsNegativeResponse(),
				backgroundEnabled: !backgroundResponse.IsNegativeResponse());

			return Content(svg, "image/svg+xml");
		}
	}
}
