using MarsRoverChallenge.ConsoleApp.Enums;
using MarsRoverChallenge.ConsoleApp.Interfaces;
using System;
using System.Drawing;

namespace MarsRoverChallenge.ConsoleApp.Rover
{
    public class MarsRover : IRover
    {
        private Point _position;
        private CardinalDirection _direction;

        public MarsRover() { }

        public MarsRover(int xPos, int yPos, CardinalDirection direction)
        {
            SetLocation(xPos, yPos, direction);
        }

        public void SetLocation(int xPos, int yPos, CardinalDirection direction)
        {
            if (xPos < 0 || yPos < 0)
            {
                throw new ArgumentException("Rover position (X and Y) must be greater or equal to 0");
            }

            _position = new Point(xPos, yPos);
            _direction = direction;
        }

        public void ChangeDirection(NavigationCommand command)
        {
            if (command == NavigationCommand.L)
            {
                int newDirection = ((int)_direction) - 90;
                if (newDirection < 0)
                {
                    newDirection = 360 + newDirection;
                }

                _direction = (CardinalDirection)newDirection;
            }
            else if (command == NavigationCommand.R)
            {
                int newDirection = ((int)_direction) + 90;
                if (newDirection >= 360)
                {
                    newDirection = 360 - newDirection;
                }

                _direction = (CardinalDirection)newDirection;
            }
        }

        public void MoveTo(Point newPosition)
        {
            _position = newPosition;
        }

        public Point ProjectMovement(int speed = 1)
        {
            int xIncrement = 0;
            int yIncrement = 0;

            switch (_direction)
            {
                case CardinalDirection.N:
                    yIncrement += speed;
                    break;
                case CardinalDirection.S:
                    yIncrement -= speed;
                    break;
                case CardinalDirection.E:
                    xIncrement += speed;
                    break;
                case CardinalDirection.W:
                    xIncrement -= speed;
                    break;
                default:
                    break;
            }

            return new Point(_position.X + xIncrement, _position.Y + yIncrement);
        }

        public Point Position => _position;
        public CardinalDirection Direction => _direction;
        public override string ToString() => $"({_position.X} {_position.Y} {_direction})";
    }
}
