using System;

namespace CombinedCodingStats.Service
{
    // TODO: Move to a service
    internal class SVGBuilder
    {
        // TODO: Think in a best approach that does not involve a variable in the SVGService nor a mutable string here
        internal string SVG { get; private set; }

        private readonly Platform _platform;
        private readonly Theme _theme;

        internal SVGBuilder(Platform platform, Theme theme)
        {
            this._platform = platform;
            this._theme = theme;

            StartSVG();
        }

        private void StartSVG()
        {
            int weeks = 52 + 1; // A year and possibly fraction of a week
            int daysOfTheWeek = 7;
            int requiredWidth = weeks * _platform.ActivitySquareDistance;
            int requiredHeigth = daysOfTheWeek * _platform.ActivitySquareDistance;

            this.BuildCanva(requiredWidth, requiredHeigth);
            this.BuildBackground(requiredWidth, requiredHeigth);
            this.BuildWeekDayHeader();
        }

        internal void BuildCanva(int requiredWidth, int requiredHeigth)
        {
            IncrementSVG(String.Format(SVGTags.CanvaOpen,
                requiredWidth + (_platform.ActivitySquareDistance * 0.2) + _platform.WeekHeaderSpacing,
                requiredHeigth + (_platform.ActivitySquareDistance * 0.2) + _platform.MonthHeaderSpacing + _platform.ExtraMonthHeaderSpacing));
        }

        internal void BuildBackground(int requiredWidth, int requiredHeigth)
        {
            IncrementSVG(String.Format(SVGTags.Background,
                requiredWidth + (_platform.ActivitySquareDistance * 0.2) + _platform.WeekHeaderSpacing,
                requiredHeigth + (_platform.ActivitySquareDistance * 0.2) + _platform.MonthHeaderSpacing + _platform.ExtraMonthHeaderSpacing,
                _theme.BackgroundColor));
        }

        internal void BuildWeekDayHeader()
        {
            string monday = DayOfWeek.Monday.ToString().Substring(0, _platform.WeekdaySize);
            string wednesday = DayOfWeek.Wednesday.ToString().Substring(0, _platform.WeekdaySize);
            string friday = DayOfWeek.Friday.ToString().Substring(0, _platform.WeekdaySize);

            IncrementSVG(String.Format(SVGTags.WeekDays,
                _platform.BaseSpacing,
                _platform.MonthHeaderSpacing + (((int)DayOfWeek.Monday+1) * _platform.ActivitySquareDistance),
                _platform.MonthHeaderSpacing + (((int)DayOfWeek.Wednesday+1) * _platform.ActivitySquareDistance),
                _platform.MonthHeaderSpacing + (((int)DayOfWeek.Friday+1) * _platform.ActivitySquareDistance),
                _platform.FontSize,
                _theme.FontColor,
                _platform.FontFamily, 
                monday,
                wednesday,
                friday));
        }

        internal void BuildMonthHeader(DateTime date, int column)
        {
            var monthShortName = date.ToString("MMMM").Substring(0, 3);

            IncrementSVG(String.Format(SVGTags.Month,
                column * _platform.ActivitySquareDistance + _platform.WeekHeaderSpacing,
                _platform.MonthHeaderSpacing,
                _platform.FontSize,
                _theme.FontColor,
                _platform.FontFamily,
                monthShortName));
        }

        internal void BuildActivitySquare(int activity, int activityLevel, int maxActivity, int column, int line)
        {
            var color = _theme.GetColor(_platform, activity);

            IncrementSVG(String.Format(SVGTags.ActivitySquareOpen,
                           column * _platform.ActivitySquareDistance + _platform.WeekHeaderSpacing,
                           line * _platform.ActivitySquareDistance + _platform.MonthHeaderSpacing + _platform.ExtraMonthHeaderSpacing, // arbritary space
                           _platform.ActivitySquareSize,
                           _platform.ActivitySquareRounding,
                           color,
                           _theme.GetColorsBelow(activityLevel),
                           activity * _platform.MaxAnimationDuration / maxActivity));
        }

        internal void BuildActivitySquareClosing()
        {
            IncrementSVG(SVGTags.ActivitySquareClose);
        }

        internal void BuildAnimation(int activity, int activityLevel, int maxActivity)
        {
            IncrementSVG(String.Format(SVGTags.Animation,
                                _theme.GetColorsBelow(activityLevel),
                                activity * _platform.MaxAnimationDuration / maxActivity));
        }

        internal void BuildCanvaClosing()
        {
            IncrementSVG(SVGTags.CanvaClose);
        }

        internal void IncrementSVG(string increment)
        {
            SVG += increment;
        }
    }
}
