using MarsRoverChallenge.ConsoleApp.Enums;
using System.Drawing;

namespace MarsRoverChallenge.ConsoleApp.Interfaces
{
    /// <summary>
    /// Describes a basic rover
    /// </summary>
    public interface IRover
    {
        public Point Position { get; }
        public CardinalDirection Direction { get; }

        public void SetLocation(int xPos, int yPos, CardinalDirection direction);
        public void ChangeDirection(NavigationCommand command);
        public void MoveTo(Point newPosition);
        public Point ProjectMovement(int speed = 1);
    }
}
