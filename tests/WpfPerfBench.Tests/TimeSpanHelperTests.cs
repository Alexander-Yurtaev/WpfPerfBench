using FluentAssertions;
using WpfPerfBench.Core.Helpers;

namespace WpfPerfBench.Tests;

public class TimeSpanHelperTests
{
    [Theory]
    [InlineData("12:34:56.78", "12:34:56")]
    [InlineData("00:00:00.78", "00:00:00")]
    [InlineData("23:59:59.99", "23:59:59")]
    public void Should_Return_Value_In_Correct_HMS_Format(string timeStr, string expected)
    {
        // Arrange
        var time = TimeSpan.Parse(timeStr);

        // Act
        var actual = TimeSpanHelper.ToHmsFormatString(time);

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("12:34:56.78989", "12:34:56.78")]
    [InlineData("00:00:00.78989", "00:00:00.78")]
    [InlineData("23:59:59.99989", "23:59:59.99")]
    public void Should_Return_Value_In_Correct_HMSF_Format(string timeStr, string expected)
    {
        // Arrange
        var time = TimeSpan.Parse(timeStr);

        // Act
        var actual = TimeSpanHelper.ToHmsfFormatString(time);

        // Assert
        actual.Should().Be(expected);
    }
}