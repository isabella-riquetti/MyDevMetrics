using CombinedCodingStats.Infraestructure;
using CombinedCodingStats.Model.GitLab;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinedCodingStats.Service
{
    public class SVGService : ISVGService
    {
        public string BuildGraph(Dictionary<DateTime, int> metrics, GitLabMetricsOptions options)
        {
            options.Theme.Config(options.BackgroundEnabled); //TODO: Move this
            var svgBuilder = new SVGBuilder(options.Platform, options.Theme);

            var today = DateTime.Now.Date;

            var date = today.AddDays(-365+ (today.DayOfWeek == DayOfWeek.Sunday ? 1 : (8-(int)today.DayOfWeek))); //TODO: Improve the logic here

            bool canStart = !options.Platform.StartAtSameDay;
            int maxActivity = !metrics.Any() ? 0 : metrics.Values.Max();

            bool monthWritten = false;
            for (int column = 0; column < 53; column++)
            {
                for (int line = 0; line < 7; line++)
                {
                    if (date > today)
                        continue;

                    //TODO: Calculate the next month position so we won't need all of these checks
                    // Will write month day next sunday
                    if (date.Day == 1) 
                        monthWritten = false;

                    // Last week won't fit the month name
                    if (date.Day > 21) 
                        monthWritten = true;

                    if (date > today)
                        continue;

                    // Some platform does not show entire first week
                    if (!canStart) 
                        if (date.Day >= today.Day)
                            canStart = true;

                    // Start on first monday otm
                    if (!monthWritten && date.DayOfWeek == 0 && canStart) 
                    {
                        monthWritten = true;
                        svgBuilder.BuildMonthHeader(date, column);
                    }

                    if (canStart)
                    {
                        int dateActivity = metrics.GetValueOrDefault(date, 0);
                        var dateActivityLevel = options.Platform.GetActivityLevelIndex(dateActivity);

                        svgBuilder.BuildActivitySquare(dateActivity, dateActivityLevel, maxActivity, column, line);

                        if(options.AnimationEnabled)
                            svgBuilder.BuildAnimation(dateActivity, dateActivityLevel, maxActivity);

                        svgBuilder.BuildActivitySquareClosing();
                    }

                    date = date.AddDays(1);
                }

                if (!canStart)
                    column--;
            }

            svgBuilder.BuildCanvaClosing();
            return svgBuilder.SVG;
        }
    }
}
