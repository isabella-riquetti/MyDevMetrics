using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using CombinedCodingStats.Infraestructure;

namespace CombinedCodingStats.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitLabController : ControllerBase
    {
		private readonly ISVGService _svgService;

		private const string _defaultPlatformName = "gitlab";
		private const string _defaultThemeName = "light";
		private const string _apiUrl = "https://gitlab.com/users/{0}/calendar.json";
		private readonly Dictionary<string, Platform> _platformThemeConfiguration;

        public GitLabController(ISVGService svgService)
        {
			var data = System.IO.File.ReadAllText(@"platform_themes_configuration.json");
			_platformThemeConfiguration = JsonConvert.DeserializeObject<Dictionary<string, Platform>>(data);

			_svgService = svgService;
		}

		[Route("{user}")]
		public IActionResult Get(string user, [FromQuery] Dictionary<string, string> parameters)
		{
			var activityPerDayResponse = new WebClient().DownloadString(String.Format(_apiUrl, user));
			var activityPerDay = JsonConvert.DeserializeObject<Dictionary<DateTime, int>>(activityPerDayResponse);

			string platformName = parameters.GetValueOrDefault("platform", _defaultPlatformName);
			string themeName = parameters.GetValueOrDefault("theme", _defaultThemeName);

			Platform platform = _platformThemeConfiguration[platformName];
			Theme theme = platform.Themes[themeName];

			var svg = _svgService.BuildGraph(activityPerDay, platform, theme);

			return Content(svg, "image/svg+xml");
		}
	}
}
