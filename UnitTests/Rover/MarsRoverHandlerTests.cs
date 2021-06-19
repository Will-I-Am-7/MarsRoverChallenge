using FluentAssertions;
using MarsRoverChallenge.ConsoleApp.Console;
using MarsRoverChallenge.ConsoleApp.Enums;
using MarsRoverChallenge.ConsoleApp.Rover;
using System;
using System.Collections.Generic;
using System.Drawing;
using Xunit;

namespace MarsRoverChallenge.UnitTests.Rover
{
    public class MarsRoverHandlerTests
    {
        [Theory]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(-1, -1)]
        public void Should_throw_argument_exception(int x, int y)
        {
            var roverHandler = new MarsRoverHandler();

            Action action = () => roverHandler.SetPlateau(x, y);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Should_throw_argument_null_exception()
        {
            var roverHandler = new MarsRoverHandler();

            Action action1 = () => roverHandler.NavigateRover(new MarsRover(), null);
            action1.Should().Throw<ArgumentNullException>();

            Action action2 = () => roverHandler.NavigateRover(null, new List<NavigationCommand>());
            action2.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(1, 2, CardinalDirection.N, "LMLMLMLMM", 1, 3, CardinalDirection.N)]
        [InlineData(3, 3, CardinalDirection.E, "MMRMMRMRRM", 5, 1, CardinalDirection.E)]
        public void Should_reach_destination(int roverX, int roverY,
                                             CardinalDirection roverDirection, string navigation,
                                             int destinationX, int destinationY, CardinalDirection destinationDirection)
        {
            var commandProcessor = new ConsoleCommandProcessor();
            var roverHandler = new MarsRoverHandler();
            roverHandler.SetPlateau(5, 5);

            var rover = new MarsRover(roverX, roverY, roverDirection);
            var commands = commandProcessor.GetRoverNavigationFromText(navigation);

            var result = roverHandler.NavigateRover(rover, commands);

            // Test navigation result
            result.DestinationReached.Should().BeTrue();

            // Test rover position
            rover.Position.Should().Be(new Point(destinationX, destinationY));
            rover.Direction.Should().Be(destinationDirection);

            // Test that the rover is actually on the grid
            roverHandler.Grid[destinationX, destinationY].Should().BeSameAs(rover);
        }

        [Fact]
        public void Navigation_output_without_commands()
        {
            var roverHandler = new MarsRoverHandler();
            roverHandler.SetPlateau(1, 1);

            var rover = new MarsRover(0, 0, CardinalDirection.N);
            var commands = new List<NavigationCommand> { };

            var result = roverHandler.NavigateRover(rover, commands);

            result.DestinationReached.Should().BeTrue();
            rover.Position.Should().Be(new Point(0, 0));
            rover.Direction.Should().Be(CardinalDirection.N);
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(5, 0)]
        [InlineData(4, 6)]
        public void Place_rover_outside_of_grid(int x, int y)
        {
            var roverHandler = new MarsRoverHandler();
            roverHandler.SetPlateau(3, 3);

            var rover = new MarsRover(x, y, CardinalDirection.N);
            var commands = new List<NavigationCommand> { };

            var result = roverHandler.NavigateRover(rover, commands);

            result.DestinationReached.Should().BeFalse();
            result.Message.Should().Be("Out of bounds. Rover could not be added to grid");

        }

        [Fact]
        public void Place_rover_at_existing_rover_position()
        {
            var roverHandler = new MarsRoverHandler();
            roverHandler.SetPlateau(3, 3);

            int x = 1;
            int y = 1;
            var direction = CardinalDirection.N;

            var rover = new MarsRover(x, y, direction);
            var rover2 = new MarsRover(x, y, direction);
            var commands = new List<NavigationCommand> { };

            roverHandler.NavigateRover(rover, commands);
            var result = roverHandler.NavigateRover(rover2, commands);

            result.DestinationReached.Should().BeFalse();
            result.Message.Should().Be("Collision with another rover. Rover could not be added to grid");
        }

        [Theory]
        [InlineData(CardinalDirection.N, "MMMM", 0, 3)]
        [InlineData(CardinalDirection.E, "MMMM", 3, 0)]
        [InlineData(CardinalDirection.S, "M", 0, 0)]
        [InlineData(CardinalDirection.W, "M", 0, 0)]
        public void Move_rover_out_of_bounds(CardinalDirection startingDirection, string navigation, int expectedX, int expectedY)
        {
            var commandProcessor = new ConsoleCommandProcessor();
            var roverHandler = new MarsRoverHandler();

            roverHandler.SetPlateau(3, 3);

            var rover = new MarsRover(0, 0, startingDirection);
            var commands = commandProcessor.GetRoverNavigationFromText(navigation);

            var result = roverHandler.NavigateRover(rover, commands);

            result.DestinationReached.Should().BeFalse();
            result.Message.Should().Be("Out of bounds");

            // Check the location of the rover on the grid
            roverHandler.Grid[expectedX, expectedY].Should().BeSameAs(rover);
        }

        [Fact]
        public void Collide_rover_with_rover()
        {
            var roverHandler = new MarsRoverHandler();

            roverHandler.SetPlateau(5, 5);

            // Rover1 ends at 2, 2 facing north
            var rover1 = new MarsRover(0, 0, CardinalDirection.E);
            var rover1Commands = new List<NavigationCommand>
            {
                NavigationCommand.M,
                NavigationCommand.M,
                NavigationCommand.L,
                NavigationCommand.M,
                NavigationCommand.M
            };

            var rover2 = new MarsRover(0, 0, CardinalDirection.N);
            var rover2Commands = new List<NavigationCommand>
            {
                NavigationCommand.M,
                NavigationCommand.M,
                NavigationCommand.R,
                NavigationCommand.M,
                NavigationCommand.M
            };

            roverHandler.NavigateRover(rover1, rover1Commands);
            var result = roverHandler.NavigateRover(rover2, rover2Commands);

            Point rover2ExpectedPosition = new(1, 2);
            CardinalDirection rover2ExpectedDirection = CardinalDirection.E;

            // Test the result
            result.DestinationReached.Should().BeFalse();
            result.Message.Should().Be("Collision with another rover");

            // Test rover position and direction
            rover2.Position.Should().Be(rover2ExpectedPosition);
            rover2.Direction.Should().Be(rover2ExpectedDirection);

            // See if the rover is on the grid
            roverHandler.Grid[rover2ExpectedPosition.X, rover2ExpectedPosition.Y].Should().BeSameAs(rover2);
        }
    }
}
