using FluentAssertions;
using MarsRoverChallenge.ConsoleApp.Enums;
using MarsRoverChallenge.ConsoleApp.Rover;
using System;
using Xunit;
using System.Drawing;

namespace MarsRoverChallenge.UnitTests.Rover
{
    public class MarsRoverTests
    {
        [Theory]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(-1, -1)]
        public void Should_throw_argument_exception(int x, int y)
        {
            var rover = new MarsRover();

            Action action = () => rover.SetLocation(x, y, CardinalDirection.E);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(CardinalDirection.N, NavigationCommand.R, CardinalDirection.E)]
        [InlineData(CardinalDirection.N, NavigationCommand.L, CardinalDirection.W)]
        [InlineData(CardinalDirection.E, NavigationCommand.R, CardinalDirection.S)]
        [InlineData(CardinalDirection.E, NavigationCommand.L, CardinalDirection.N)]
        [InlineData(CardinalDirection.S, NavigationCommand.R, CardinalDirection.W)]
        [InlineData(CardinalDirection.S, NavigationCommand.L, CardinalDirection.E)]
        [InlineData(CardinalDirection.W, NavigationCommand.R, CardinalDirection.N)]
        [InlineData(CardinalDirection.W, NavigationCommand.L, CardinalDirection.S)]
        public void Should_change_direction(CardinalDirection startingDirection, NavigationCommand navigation, CardinalDirection expectedDirection) 
        {
            var rover = new MarsRover(0, 0, startingDirection);

            rover.ChangeDirection(navigation);

            rover.Direction.Should().Be(expectedDirection);
        }

        [Fact]
        public void Should_change_position() 
        {
            var rover = new MarsRover(0, 0, CardinalDirection.N);

            var newPosition = new Point(1, 1);

            rover.MoveTo(newPosition);

            rover.Position.Should().Be(newPosition);
        }

        [Theory]
        [InlineData(0, 0, CardinalDirection.N, 0, 1)]
        [InlineData(0, 0, CardinalDirection.E, 1, 0)]
        [InlineData(1, 1, CardinalDirection.S, 1, 0)]
        [InlineData(1, 1, CardinalDirection.W, 0, 1)]
        public void Should_project_movement_correctly(int roverX, int roverY, CardinalDirection roverDirection, 
                                                      int expectedX, int expectedY) 
        {
            var rover = new MarsRover(roverX, roverY, roverDirection);

            var projectedPosition = rover.ProjectMovement();

            var expectedPosition = new Point(expectedX, expectedY);

            projectedPosition.Should().Be(expectedPosition);
        }
    }
}
