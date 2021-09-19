using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using CombinedCodingStats.Infraestructure;
using System.Linq;
using CombinedCodingStats.Helper;
using CombinedCodingStats.Model.GitLab;

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
		public IActionResult Get(string user, [FromQuery] Dictionary<string, object> queryParameters)
		{
			var gitLabQueryParameters = queryParameters.ToObject<GitLabMetricsQueryParameters>();
			var parameters = gitLabQueryParameters.GetOptions();

			var activityPerDayResponse = new WebClient().DownloadString(String.Format(_apiUrl, user));
			var activityPerDay = JsonConvert.DeserializeObject<Dictionary<DateTime, int>>(activityPerDayResponse);

			var svg = _svgService.BuildGraph(
				activityPerDay,
				parameters);

			return Content(svg, "image/svg+xml");
		}
	}
}
