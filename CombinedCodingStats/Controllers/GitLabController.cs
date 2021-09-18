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

		private const string _defaultPlatformName = "gitlab";
		private const string _defaultThemeName = "light";
		private const string _defaultAnimationStatus = "true";
		private const string _apiUrl = "https://gitlab.com/users/{0}/calendar.json";
		private readonly Dictionary<string, Platform> _platformThemeConfiguration;

        public GitLabController(ISVGService svgService)
        {
			var data = System.IO.File.ReadAllText(@"platform_themes_configuration.json");
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

			string platformName = parametersLowerCase.GetValueOrDefault("platform", _defaultPlatformName);
			string themeName = parametersLowerCase.GetValueOrDefault("theme", _defaultThemeName);
			string animationResponse = parametersLowerCase.GetValueOrDefault("animation", _defaultAnimationStatus);

			Platform platform = _platformThemeConfiguration.GetValueOrDefault(platformName, _platformThemeConfiguration[_defaultPlatformName]);
			Theme theme = platform.Themes.GetValueOrDefault(themeName, platform.Themes[_defaultThemeName]);

			var svg = _svgService.BuildGraph(activityPerDay, animationResponse.IsNegativeResponse(), platform, theme);

			return Content(svg, "image/svg+xml");
		}
	}
}
