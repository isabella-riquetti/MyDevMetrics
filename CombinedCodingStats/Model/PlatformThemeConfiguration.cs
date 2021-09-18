using Newtonsoft.Json;
using System.Collections.Generic;

namespace CombinedCodingStats
{
    public class Theme
    {
        [JsonProperty("background_color")]
        public string BackgroundColor { get; set; }

        [JsonProperty("no_activity_color")]
        public string NoActivityColor { get; set; }

        [JsonProperty("activity_level_1_color")]
        public string ActivityLevel1Color { get; set; }

        [JsonProperty("activity_level_2_color")]
        public string ActivityLevel2Color { get; set; }

        [JsonProperty("activity_level_3_color")]
        public string ActivityLevel3Color { get; set; }

        [JsonProperty("activity_level_4_color")]
        public string ActivityLevel4Color { get; set; }

        public List<string> Colors;

        public void LoadColors()
        {
            Colors = new List<string>()
            {
                NoActivityColor, BackgroundColor, ActivityLevel1Color, ActivityLevel2Color, ActivityLevel3Color, ActivityLevel4Color
            };
        }

        public string GetColorsBelow(int activityLevel)
        {
            var colorsBelow = Colors.GetRange(0, activityLevel + 2);
            if(activityLevel > 1)
            {
                colorsBelow.RemoveAt(0);
            }
            return string.Join(';', colorsBelow);
        }
    }

    public class Platform
    {
        [JsonProperty("start_at_same_day")]
        public bool StartAtSameDay { get; set; }

        [JsonProperty("activity_level_1")]
        public int ActivityLevel1 { get; set; }

        [JsonProperty("activity_level_2")]
        public int ActivityLevel2 { get; set; }

        [JsonProperty("activity_level_3")]
        public int ActivityLevel3 { get; set; }

        [JsonProperty("activity_level_4")]
        public int ActivityLevel4 { get; set; }

        [JsonProperty("activity_square_size")]
        public int ActivitySquareSize { get; set; }

        [JsonProperty("activity_square_spacing")]
        public int ActivitySquareSpacing { get; set; }

        [JsonProperty("activity_square_rounding")]
        public int ActivitySquareRounding { get; set; }

        [JsonProperty("max_animation_duration")]
        public double MaxAnimationDuration { get; set; }

        [JsonProperty("themes")]
        public Dictionary<string, Theme> Themes { get; set; }


        public int ActivitySquareDistance => ActivitySquareSize + ActivitySquareSpacing;

        public int GetActivityLevelIndex(int activity)
        {
            if (activity >= ActivityLevel4)
            {
                return 4;
            }
            if (activity >= ActivityLevel3)
            {
                return 3;
            }
            if (activity >= ActivityLevel2)
            {
                return 2;
            }
            if (activity >= ActivityLevel1)
            {
                return 1;
            }

            return 0;
        }
    }
}
