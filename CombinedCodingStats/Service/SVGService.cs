using Aspose.Svg;
using CombinedCodingStats.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinedCodingStats.Service
{
    public class SVGService : ISVGService
    {
        public const string Canva = "<svg width=\"{0}\" height=\"{1}\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" style=\"overflow: hidden; position: relative;\">";

        public const string AreaClose = "</svg>";

        public const string Background = "<rect x=\"0\" y=\"0\" width=\"{0}\" height=\"{1}\" " +
                        "rx=\"4\" ry=\"4\" fill=\"{2}\" stroke=\"none\"></rect>";

        public const string ActivitySquareOpen = "<rect x=\"{0}\" y=\"{1}\" width=\"{2}\" height=\"{2}\" " +
                "r=\"{3}\" rx=\"{3}\" ry=\"{4}\" fill=\"{4}\" stroke=\"none\" style=\"-webkit-tap-highlight-color: rgba(0, 0, 0, 0);\">";

        public const string ActivitySquareClose = "</rect>";

        public const string Animation = "<animate attributeName=\"fill\" values=\"{0}\" dur=\"{1}s\"/>";

        public const string Month = "<text style=\"font-size:{0}px;fill:{1};font-family:{2}\"" +
            " y=\"{3}\" x=\"{4}\">{5}</text>";

        public const string WeekDays = 
            "<text style=\"font-size:{0}px;fill:{1};font-family:{2}\" x=\"{3}\" y=\"{4}\" >{5}</text>" +
            "<text style=\"font-size:{0}px;fill:{1};font-family:{2}\" x=\"{3}\" y=\"{6}\" >{7}</text>" +
            "<text style=\"font-size:{0}px;fill:{1};font-family:{2}\" x=\"{3}\" y=\"{8}\" >{9}</text>";

        public string BuildGraph(Dictionary<DateTime, int> activityPerDay, bool disableAnimation, Platform platform, Theme theme)
        {
            theme.LoadColors();
            string activitySquares = "";
            var today = DateTime.Now.Date;
            var date = today.AddDays(-365 - (int)today.DayOfWeek + 1); // A year - days the week (0 = sunday) + one to go to monday

            int width = 53 * platform.ActivitySquareDistance;
            int height = 7 * platform.ActivitySquareDistance;

            bool canStart = !platform.StartAtSameDay;
            int maxActivity = activityPerDay.Values.Max();

            string monday = DayOfWeek.Monday.ToString().Substring(0, platform.WeekdaySize),
                wednesday = DayOfWeek.Wednesday.ToString().Substring(0, platform.WeekdaySize),
                friday = DayOfWeek.Friday.ToString().Substring(0, platform.WeekdaySize);

            activitySquares += String.Format(WeekDays, platform.FontSize, theme.FontColor, platform.FontFamily, 2,
                platform.HeaderSpacing + (2 * platform.ActivitySquareDistance), monday,
                platform.HeaderSpacing + (4 * platform.ActivitySquareDistance), wednesday,
                platform.HeaderSpacing + (6 * platform.ActivitySquareDistance), friday);

            bool monthWritten = false;
            for (int horizontal = 0; horizontal < 53; horizontal++)
            {
                for (int vertical = 0; vertical < 7; vertical++)
                {
                    if(date.Day == 1)
                    {
                        monthWritten = false;
                    }

                    if(date.Day > 21) // Last week won't fit the month name
                    {
                        monthWritten = true;
                    }

                    if (date > today)
                        continue;

                    if (!canStart)
                    {
                        if (date.Day == today.Day)
                        {
                            canStart = true;
                        }
                    }

                    if (!monthWritten && date.DayOfWeek == 0 && canStart) // Start on first monday otm
                    {
                        monthWritten = true;
                        var monthShortName = date.ToString("MMMM").Substring(0, 3);
                        activitySquares += String.Format(Month, platform.FontSize, theme.FontColor, platform.FontFamily,
                            17, horizontal * platform.ActivitySquareDistance + platform.WeekDaySpacing, monthShortName);
                    }

                    if (canStart)
                    {
                        int activity = activityPerDay.GetValueOrDefault(date, 0);

                        string color = theme.NoActivityColor;

                        if (platform.ActivityLevel4 < activity)
                        {
                            color = theme.ActivityLevel4Color;
                        }
                        else if (platform.ActivityLevel3 < activity)
                        {
                            color = theme.ActivityLevel3Color;
                        }
                        else if (platform.ActivityLevel2 < activity)
                        {
                            color = theme.ActivityLevel2Color;
                        }
                        else if (platform.ActivityLevel1 < activity)
                        {
                            color = theme.ActivityLevel1Color;
                        }

                        var activityLevel = platform.GetActivityLevelIndex(activity);
                        activitySquares += String.Format(ActivitySquareOpen,
                           horizontal * platform.ActivitySquareDistance + platform.WeekDaySpacing,
                           vertical * platform.ActivitySquareDistance + platform.MonthSpacing,
                           platform.ActivitySquareSize,
                           platform.ActivitySquareRounding,
                           color,
                           theme.GetColorsBelow(activityLevel),
                           activity * platform.MaxAnimationDuration / maxActivity);

                        if(!disableAnimation)
                        {
                            activitySquares += String.Format(Animation,
                                theme.GetColorsBelow(activityLevel),
                                activity * platform.MaxAnimationDuration / maxActivity);
                        }

                        activitySquares += ActivitySquareClose;
                    }

                    date = date.AddDays(1);
                }
            }


            return String.Format(Canva,
                width + (platform.ActivitySquareDistance * 0.2) + platform.WeekDaySpacing,
                height + (platform.ActivitySquareDistance * 0.2) + platform.MonthSpacing)
                + String.Format(Background,
                width + (platform.ActivitySquareDistance * 0.2) + platform.WeekDaySpacing,
                height + (platform.ActivitySquareDistance * 0.2) + platform.MonthSpacing,
                theme.BackgroundColor)
                + activitySquares
                + AreaClose;
        }
    }
}
