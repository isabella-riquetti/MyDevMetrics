﻿using CombinedCodingStats.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinedCodingStats.Service
{
    public class SVGService : ISVGService
    {

        public string BuildGraph(Dictionary<DateTime, int> metrics, bool disableAnimation, Platform _platform, Theme _theme)
        {
            var svgBuilder = new SVGBuilder(_platform, _theme);
            _theme.LoadColors();

            var today = DateTime.Now.Date;

            // A year - days the week (0 = sunday) + one to go to monday
            var date = today.AddDays(-365 - (int)today.DayOfWeek + 1); 

            bool canStart = !_platform.StartAtSameDay;
            int maxActivity = metrics.Values.Max();

            bool monthWritten = false;
            for (int column = 0; column < 53; column++)
            {
                for (int line = 0; line < 7; line++)
                {
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
                        if (date.Day == today.Day)
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
                        var dateActivityLevel = _platform.GetActivityLevelIndex(dateActivity);

                        svgBuilder.BuildActivitySquare(dateActivity, dateActivityLevel, maxActivity, column, line);

                        if(!disableAnimation)
                            svgBuilder.BuildAnimation(dateActivity, dateActivityLevel, maxActivity);

                        svgBuilder.BuildActivitySquareClosing();
                    }

                    date = date.AddDays(1);
                }
            }

            svgBuilder.BuildCanvaClosing();
            return svgBuilder.SVG;
        }
    }
}
