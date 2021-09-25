using CombinedCodingStats.Helper;
using CombinedCodingStats.Model.GitLab;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace CombinedCodingStatsTest.HelpersTests
{
    public class ObjectExtensionsTests
    {
        [Theory, MemberData(nameof(ObjectExtensionConvertToObjectTests))]
        public void ToObject(ObjectExtensionConvertToObjectTestInput test)
        {
            var result = test.Parameters.ToObject<GitLabMetricsQueryParameters>();

            result.Should().BeEquivalentTo(test.ExpectedResult);
        }

        public static TheoryData<ObjectExtensionConvertToObjectTestInput> ObjectExtensionConvertToObjectTests = new TheoryData<ObjectExtensionConvertToObjectTestInput>()
        {
            new ObjectExtensionConvertToObjectTestInput()
            {
                Name = "No parameters",
                Parameters = new Dictionary<string, object>(),
                ExpectedResult = new GitLabMetricsQueryParameters()
            },
            new ObjectExtensionConvertToObjectTestInput()
            {
                Name = "Unrecognized parameter",
                Parameters = new Dictionary<string, object>()
                {
                    { "test", "hello" }
                },
                ExpectedResult = new GitLabMetricsQueryParameters()
            },
            new ObjectExtensionConvertToObjectTestInput()
            {
                Name = "Null parameter, no default ",
                Parameters = new Dictionary<string, object>()
                {
                    { "Platform", null }
                },
                ExpectedResult = new GitLabMetricsQueryParameters()
            },
            new ObjectExtensionConvertToObjectTestInput()
            {
                Name = "Empty parameter, no default ",
                Parameters = new Dictionary<string, object>()
                {
                    { "Theme", "" }
                },
                ExpectedResult = new GitLabMetricsQueryParameters()
                {
                    Theme = ""
                }
            },
            new ObjectExtensionConvertToObjectTestInput()
            {
                Name = "Parameter captalization",
                Parameters = new Dictionary<string, object>()
                {
                    { "pLATFORM", "github" },
                    { "THEME", "dark" }
                },
                ExpectedResult = new GitLabMetricsQueryParameters()
                {
                    Platform = "github",
                    Theme = "dark"
                }
            },
            new ObjectExtensionConvertToObjectTestInput()
            {
                Name = "Only Basic Parameters",
                Parameters = new Dictionary<string, object>()
                {
                    { "Platform", "github" },
                    { "tHEME", "dark" }
                },
                ExpectedResult = new GitLabMetricsQueryParameters()
                {
                    Platform = "github",
                    Theme = "dark"
                }
            }
        };

        public class ObjectExtensionConvertToObjectTestInput : TestInput
        {
            public IDictionary<string, object> Parameters { get; set; }
            public GitLabMetricsQueryParameters ExpectedResult { get; set; }
        }
    }
}
