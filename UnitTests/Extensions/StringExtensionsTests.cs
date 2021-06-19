using FluentAssertions;
using MarsRoverChallenge.ConsoleApp.Extensions;
using Xunit;

namespace MarsRoverChallenge.UnitTests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(" ", "")]
        [InlineData(null, null)]
        [InlineData(" test   ", "test")]
        [InlineData(" t e st  ", "test")]
        public void Should_remove_white_space(string text, string expected) 
        {
            text.RemoveWhitespace().Should().Be(expected);
        }
    }
}
