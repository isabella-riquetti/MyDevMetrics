using Aspose.Svg;
using CombinedCodingStats.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinedCodingStats.Service
{
    public class SVGService : ISVGService
    {
        public const string AreaOpen = "<svg width=\"{0}\" height=\"{1}\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" style=\"overflow: hidden; position: relative;\">";

        public const string AreaClose = "</svg>";

        public const string Background = "<rect x=\"0\" y=\"0\" width=\"{0}\" height=\"{1}\" " +
                        "rx=\"4\" ry=\"4\" fill=\"{2}\" stroke=\"none\"></rect>";

        public const string ActivitySquareOpen = "<rect x=\"{0}\" y=\"{1}\" width=\"{2}\" height=\"{2}\" " +
                "r=\"{3}\" rx=\"{3}\" ry=\"{4}\" fill=\"{4}\" stroke=\"none\" style=\"-webkit-tap-highlight-color: rgba(0, 0, 0, 0);\">";

        public const string ActivitySquareClose = "</rect>";

        public string Animation = "<animate attributeName=\"fill\" values=\"{0}\" dur=\"{1}s\"/>";

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
            for (int horizontal = 0; horizontal < 53; horizontal++)
            {
                for (int vertical = 0; vertical < 7; vertical++)
                {
                    if (date > today)
                        continue;

                    if (!canStart)
                    {
                        if (date.Day == today.Day)
                        {
                            canStart = true;
                        }
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
                           horizontal * platform.ActivitySquareDistance + 7.5,
                           vertical * platform.ActivitySquareDistance + 7.5,
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


            return String.Format(AreaOpen, width + (platform.ActivitySquareDistance * 0.8), height + (platform.ActivitySquareDistance * 0.8))
                + String.Format(Background, width + (platform.ActivitySquareDistance * 0.8), height + (platform.ActivitySquareDistance * 0.8), theme.BackgroundColor)
                + activitySquares
                + AreaClose;
        }
    }
}
