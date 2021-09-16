using Aspose.Svg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace GitLabStats.Controllers
{
    [Route("gitlab")]
    public class GitLabController : Controller
    {
        private readonly ILogger<GitLabController> _logger;

        public GitLabController(ILogger<GitLabController> logger)
        {
            _logger = logger;
        }

        [Route("{user}")]
        public IActionResult Index(string user, string theme = "dark")
        {
            ViewBag.User = user;

            var jsonResponse = new WebClient().DownloadString($"https://gitlab.com/users/{user}/calendar.json");
            var json = JsonConvert.DeserializeObject<Dictionary<DateTime, int>>(jsonResponse);

            var svg = "<svg height=\"105\" version=\"1.1\" width=\"780\" xmlns=\"http://www.w3.org/2000/svg\" style=\"overflow: hidden; position: relative;\">";

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
					string color = theme == "dark" ? "#2d333b" : "#ebedf0";
					if (json.ContainsKey(date))
					{
						var actions = json[date];
						if (actions > 30)
						{
							color = "#39d353";
						}
						else if (actions > 20)
						{
							color = "#26a641";
						}
						else if (actions > 10)
						{
							color = "#006d32";
						}
						else if (actions > 1)
						{
							color = "#0e4429";
						}
					}

					svg += $"<rect x=\"{horizontal*15}\" y=\"{vertical*15}\" width=\"11\" height=\"11\" r=\"2\" rx=\"2\" ry=\"2\" fill=\"{color}\" stroke=\"none\" style=\"-webkit-tap-highlight-color: rgba(0, 0, 0, 0);\"></rect>";
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

        public IActionResult Calendar(string user)
        {
            var json = new WebClient().DownloadString($"https://gitlab.com/users/{user}/calendar.json");
            return Json(json);
        }
    }
}
