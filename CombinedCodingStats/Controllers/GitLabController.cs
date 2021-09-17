using CombinedCodingStats.Handler.Theme;
using CombinedCodingStats.Model.Platform;
using CombinedCodingStats.Model.Theme;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace CombinedCodingStats.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitLabController : ControllerBase
    {
		private readonly IPlatformHandler _platformHandler;

		public GitLabController(IThemeHandler<GitHubModel> themeHandler, IPlatformHandler platformHandler)
        {
			_platformHandler = platformHandler;
		}

		[Route("{user}")]
		public IActionResult Get(string user, [FromQuery] Dictionary<string, string> parameters)
		{
			var platformHandler = _platformHandler.Handle(parameters["platform"]);
			platformHandler.SetTheme(parameters["theme"]);

			var jsonResponse = new WebClient().DownloadString($"https://gitlab.com/users/{user}/calendar.json");
			var json = JsonConvert.DeserializeObject<Dictionary<DateTime, int>>(jsonResponse);

			var svg = "<svg height=\"105\" version=\"1.1\" width=\"795\" xmlns=\"http://www.w3.org/2000/svg\" style=\"overflow: hidden; position: relative;\"><rect xmlns=\"http://www.w3.org/2000/svg\" data-testid=\"card-bg\" x=\"0.5\" y=\"0.5\" rx=\"4.5\" height=\"99%\" stroke=\"#000000\" width=\"794\" fill=\"#1a1a1a\" stroke-opacity=\"1\"/>";

			var today = DateTime.Now.Date;
			var date = today.AddDays(-365 - 7 + (int)today.DayOfWeek);

			var vertical = 0;
			var horizontal = 0;

			var canStart = false;
			while (date <= today)
			{
				if (!canStart)
				{
					if (date.Day == today.Day)
					{
						canStart = true;
					}
				}

				if (canStart)
				{
					svg += platformHandler.GetDateSvgSquare(date, json.ContainsKey(date) ? json[date] : 0, horizontal, vertical);
				}

				date = date.AddDays(1);
				vertical++;

				if (vertical == 7)
				{
					vertical = 0;
					horizontal++;
				}
			}

			svg += "</svg>";

			return Content(svg, "image/svg+xml");
		}
    }
}
