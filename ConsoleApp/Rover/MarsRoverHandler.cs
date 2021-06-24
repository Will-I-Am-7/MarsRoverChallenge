using MarsRoverChallenge.ConsoleApp.Enums;
using MarsRoverChallenge.ConsoleApp.Interfaces;
using MarsRoverChallenge.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MarsRoverChallenge.ConsoleApp.Rover
{
    public class MarsRoverHandler : IRoverHandler
    {
        // Lower left coordinates are assumed to be at 0,0
        private IRover[,] _grid;
        private Point _upperRightPosition;

        public MarsRoverHandler()
        {
            _upperRightPosition = new Point(1, 1);

            _grid = new MarsRover[_upperRightPosition.X + 1, _upperRightPosition.Y + 1];
        }

        public void SetPlateau(int upperRightX, int upperRightY)
        {
            if (upperRightX < 0 || upperRightY < 0)
            {
                throw new ArgumentException("Grid upper bounds (X and Y) must be greater or equal to 0");
            }

            _upperRightPosition = new Point(upperRightX, upperRightY);

            _grid = new MarsRover[_upperRightPosition.X + 1, _upperRightPosition.Y + 1];
        }

        public NavigationResult NavigateRover(IRover rover, IEnumerable<NavigationCommand> commands)
        {
            if (rover == null)
            {
                throw new ArgumentNullException(nameof(rover));
            }

            if (commands == null)
            {
                throw new ArgumentNullException(nameof(commands));
            }

            // Check bounds
            if (OutOfBounds(rover.Position.X, rover.Position.Y))
            {
                return new NavigationResult
                {
                    Message = $"Out of bounds. Rover could not be added to grid",
                    DestinationReached = false
                };
            }

            // Check collision
            if (Collides(rover.Position.X, rover.Position.Y))
            {
                return new NavigationResult
                {
                    Message = $"Collision with another rover. Rover could not be added to grid",
                    DestinationReached = false
                };
            }

            var result = new NavigationResult
            {
                Message = "Destination reached",
                DestinationReached = true
            };

            foreach (var command in commands)
            {
                // Movement command
                if (command == NavigationCommand.M)
                {
                    Point newRoverPosition = rover.ProjectMovement();

                    // Check bounds
                    if (OutOfBounds(newRoverPosition.X, newRoverPosition.Y))
                    {
                        result = new NavigationResult
                        {
                            Message = "Out of bounds",
                            DestinationReached = false
                        };

                        break;
                    }

                    // Check collision
                    if (Collides(newRoverPosition.X, newRoverPosition.Y))
                    {
                        result = new NavigationResult
                        {
                            Message = "Collision with another rover",
                            DestinationReached = false
                        };

                        break;
                    }

                    rover.MoveTo(newRoverPosition);

                    continue;
                }

                // Navigation command
                rover.ChangeDirection(command);
            }

            // Finally, add the rover to the grid
            _grid[rover.Position.X, rover.Position.Y] = rover;

            return result;
        }

        private bool OutOfBounds(int x, int y) 
        {
            return x < 0 || y < 0 || x > _upperRightPosition.X || y > _upperRightPosition.Y;
        }

        private bool Collides(int x, int y) 
        {
            return _grid[x, y] != null;
        }

        public IRover[,] Grid => _grid;
        public Point UpperRightPosition => _upperRightPosition;
    }
}
