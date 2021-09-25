using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace CombinedCodingStats.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubController : ControllerBase
	{
		private string _urlStats = "https://github-readme-stats.vercel.app/api?username={0}&show_icons=true&include_all_commits=true&count_private=true";

		private string _urlLanguages = "https://github-readme-stats.vercel.app/api/top-langs/?username={0}&layout=compact&hide=java";

		public GitHubController()
        {
		}

		[HttpGet]
		[Route("GitHubStats/Customize/{user}")]
		public IActionResult GetGitHubStats(string user)
		{
			var svg = new WebClient().DownloadString(String.Format(_urlStats, user));

			svg = svg.Replace("#fffefe", "transparent");
			svg = svg.Replace("#4c71f2", "#bf91f3");
			svg = svg.Replace("#2f80ed", "#70a5fd");
			svg = svg.Replace("#333", "#38bdae");
			svg = svg.Replace("#333333", "#38bdae");


			return Content(svg, "image/svg+xml");
		}

		[HttpGet]
		[Route("Languages/Customize/{user}")]
		public IActionResult GetLanguages(string user)
		{
			var svg = new WebClient().DownloadString(String.Format(_urlLanguages, user));

			svg = svg.Replace("#fffefe", "transparent");
			svg = svg.Replace("#4c71f2", "#bf91f3");
			svg = svg.Replace("#2f80ed", "#70a5fd");
			svg = svg.Replace("#333", "#38bdae");
			svg = svg.Replace("#333333", "#38bdae");


			return Content(svg, "image/svg+xml");
		}
	}
}
