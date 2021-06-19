using FluentAssertions;
using MarsRoverChallenge.ConsoleApp.Console;
using MarsRoverChallenge.ConsoleApp.Enums;
using MarsRoverChallenge.ConsoleApp.Rover;
using System.Collections.Generic;
using System.Drawing;
using Xunit;

namespace MarsRoverChallenge.UnitTests.Console
{
    public class ConsoleCommandProcessorTests
    {
        [Fact]
        public void Navigation_should_be_null()
        {
            var processor = new ConsoleCommandProcessor();

            var result = processor.GetRoverNavigationFromText("LMT");

            result.Should().BeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Navigation_should_be_empty(string navigation)
        {
            var processor = new ConsoleCommandProcessor();

            var result = processor.GetRoverNavigationFromText(navigation);

            result.Should().BeEmpty();
        }

        [Fact]
        public void Navigation_should_be_correct()
        {
            var processor = new ConsoleCommandProcessor();

            var result = processor.GetRoverNavigationFromText("LRM");

            var expectedCommands = new List<NavigationCommand>
            {
                NavigationCommand.L,
                NavigationCommand.R,
                NavigationCommand.M
            };

            result.Should().BeEquivalentTo(expectedCommands);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("3 4 F")]
        [InlineData("ddf")]
        [InlineData("10 F N")]
        public void Rover_should_be_null(string text)
        {
            var processor = new ConsoleCommandProcessor();

            var result = processor.GetRoverFromText(text);

            result.Should().BeNull();
        }

        [Theory]
        [InlineData("0 0 N", 0, 0, CardinalDirection.N)]
        [InlineData("3 4 E", 3, 4, CardinalDirection.E)]
        [InlineData("12 4 W", 12, 4, CardinalDirection.W)]
        [InlineData("10 45 W", 10, 45, CardinalDirection.W)]
        [InlineData(" 1 4 S ", 1, 4, CardinalDirection.S)]
        public void Rover_should_be_returned(string text, int expectedX, int expectedY, CardinalDirection expectedDirection)
        {
            var processor = new ConsoleCommandProcessor();

            var rover = processor.GetRoverFromText(text);

            var expectedRover = new MarsRover(expectedX, expectedY, expectedDirection);

            rover.Should().BeEquivalentTo(expectedRover);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("3 D")]
        [InlineData("ddf")]
        [InlineData("3 1 1")]
        public void Plateau_Coords_Should_Be_Null(string text)
        {
            var processor = new ConsoleCommandProcessor();

            var result = processor.GetPlateauCoordsFromText(text);

            result.Should().BeNull();
        }

        [Theory]
        [InlineData("1 1", 1, 1)]
        [InlineData("3 60", 3, 60)]
        [InlineData("20 20", 20, 20)]
        [InlineData(" 5 5 ", 5, 5)]
        public void Plateau_Coords_Should_Be_Returned(string text, int expectedX, int expectedY)
        {
            var processor = new ConsoleCommandProcessor();

            var result = processor.GetPlateauCoordsFromText(text);

            var expectedCoords = new Point(expectedX, expectedY);

            result.Should().Be(expectedCoords);
        }
    }
}
