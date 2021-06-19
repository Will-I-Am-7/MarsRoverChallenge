using MarsRoverChallenge.ConsoleApp.Enums;
using MarsRoverChallenge.ConsoleApp.Models;
using System.Collections.Generic;
using System.Drawing;

namespace MarsRoverChallenge.ConsoleApp.Interfaces
{
    public interface IRoverHandler
    {
        public IRover[,] Grid { get; }
        public Point UpperRightPosition { get; }

        public void SetPlateau(int upperRightX, int upperRightY);
        public NavigationResult NavigateRover(IRover rover, IEnumerable<NavigationCommand> commands);
    }
}
