using CombinedCodingStats.Model.GitLab;
using Xunit;

namespace CombinedCodingStatsTest.ServicesTests
{
    public class GitLabMetricsQueryParametersTests
    {
        [Theory, MemberData(nameof(GetOptionsTests))]
        public void GetOptionsTest(GetOptionsTestInput test)
        {
            var result = test.QueryParameters.GetOptions();

            Assert.Equal(test.ExpectedPlatformActivitySquareSize, result.Platform?.ActivitySquareSize);
            Assert.Equal(test.ExpectedThemeActivityLevel1Color, result.Theme?.ActivityLevel1Color);
            Assert.Equal(test.ExpectedBackgroundStatus, result.BackgroundEnabled);
            Assert.Equal(test.ExpectedAnimationStatus, result.AnimationEnabled);
        }

        public static TheoryData<GetOptionsTestInput> GetOptionsTests = new TheoryData<GetOptionsTestInput>()
        {
            new GetOptionsTestInput()
            {
                Name = "No parameters, using defaults",
                QueryParameters = new GitLabMetricsQueryParameters(),
                ExpectedPlatformActivitySquareSize = 15,
                ExpectedThemeActivityLevel1Color = "#acd5f2",
                ExpectedBackgroundStatus = true,
                ExpectedAnimationStatus = true
            },
            new GetOptionsTestInput()
            {
                Name = "Invalid parameters, using defaults",
                QueryParameters = new GitLabMetricsQueryParameters()
                {
                    Platform = "Git",
                    Theme = "Rainbow",
                },
                ExpectedPlatformActivitySquareSize = 15,
                ExpectedThemeActivityLevel1Color = "#acd5f2",
                ExpectedBackgroundStatus = true,
                ExpectedAnimationStatus = true
            },
            new GetOptionsTestInput()
            {
                Name = "Valid platform, empty theme",
                QueryParameters = new GitLabMetricsQueryParameters()
                {
                    Platform = "GitHub"
                },
                ExpectedPlatformActivitySquareSize = 11,
                ExpectedThemeActivityLevel1Color = "#39d353",
                ExpectedBackgroundStatus = true,
                ExpectedAnimationStatus = true
            },
            new GetOptionsTestInput()
            {
                Name = "Valid theme, empty platform",
                QueryParameters = new GitLabMetricsQueryParameters()
                {
                    Background = "Nain"
                },
                ExpectedPlatformActivitySquareSize = 15,
                ExpectedThemeActivityLevel1Color = "#acd5f2",
                ExpectedBackgroundStatus = true,
                ExpectedAnimationStatus = true
            },
            new GetOptionsTestInput()
            {
                Name = "Valid",
                QueryParameters = new GitLabMetricsQueryParameters()
                {
                    Platform = "GitHub",
                    Theme = "Dark",
                    Background = "False",
                    Animation = "Disabled"
                },
                ExpectedPlatformActivitySquareSize = 11,
                ExpectedThemeActivityLevel1Color = "#0e4429",
                ExpectedBackgroundStatus = false,
                ExpectedAnimationStatus = false
            }
        };

        public class GetOptionsTestInput : TestInput
        {
            public GitLabMetricsQueryParameters QueryParameters { get; set; }

            public int ExpectedPlatformActivitySquareSize { get; set; }
            public string ExpectedThemeActivityLevel1Color { get; set; }
            public bool ExpectedBackgroundStatus { get; set; }
            public bool ExpectedAnimationStatus { get; set; }
        }
    }
}
