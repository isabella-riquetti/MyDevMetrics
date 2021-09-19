using CombinedCodingStats;
using Xunit;

namespace CombinedCodingStatsTest.HelpersTests
{
    public class StringHelperTests
    {
        public class IsPositiveOrNegativeTestInput : TestInput
        {
            public string Value { get; set; }
            public bool ExpectedResponse { get; set; }
        }

        [Theory, MemberData(nameof(IsPositiveTests))]
        public void IsPositiveTest(IsPositiveOrNegativeTestInput test)
        {
            Assert.Equal(test.Value.IsPositive(), test.ExpectedResponse);
        }

        public static TheoryData<IsPositiveOrNegativeTestInput> IsPositiveTests = new TheoryData<IsPositiveOrNegativeTestInput>()
        {
            new IsPositiveOrNegativeTestInput()
            {
                Name = "Should accept",
                Value = "true",
                ExpectedResponse = true
            },
            new IsPositiveOrNegativeTestInput()
            {
                Name = "Captalization",
                Value = "yEs",
                ExpectedResponse = true
            },
            new IsPositiveOrNegativeTestInput()
            {
                Name = "Null",
                Value = null,
                ExpectedResponse = false
            },
            new IsPositiveOrNegativeTestInput()
            {
                Name = "Empty",
                Value = "",
                ExpectedResponse = false
            },
        };


        [Theory, MemberData(nameof(IsNegativeTests))]
        public void IsNegativeTest(IsPositiveOrNegativeTestInput test)
        {
            Assert.Equal(test.Value.IsNegative(), test.ExpectedResponse);
        }

        public static TheoryData<IsPositiveOrNegativeTestInput> IsNegativeTests = new TheoryData<IsPositiveOrNegativeTestInput>()
        {
            new IsPositiveOrNegativeTestInput()
            {
                Name = "Basic",
                Value = "no",
                ExpectedResponse = true
            },
            new IsPositiveOrNegativeTestInput()
            {
                Name = "Captalization",
                Value = "nÃo",
                ExpectedResponse = true
            },
            new IsPositiveOrNegativeTestInput()
            {
                Name = "Null",
                Value = null,
                ExpectedResponse = false
            },
            new IsPositiveOrNegativeTestInput()
            {
                Name = "Empty",
                Value = "",
                ExpectedResponse = false
            },
        };
    }
}
