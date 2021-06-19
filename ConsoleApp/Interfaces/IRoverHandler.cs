using MarsRoverChallenge.ConsoleApp.Enums;
using MarsRoverChallenge.ConsoleApp.Models;
using System.Collections.Generic;
using System.Drawing;

namespace MarsRoverChallenge.ConsoleApp.Interfaces
{
    /// <summary>
    /// Handles the rovers on the grid and keeps track of their positions on the grid
    /// </summary>
    public interface IRoverHandler
    {
        public IRover[,] Grid { get; }
        public Point UpperRightPosition { get; }

        public void SetPlateau(int upperRightX, int upperRightY);
        public NavigationResult NavigateRover(IRover rover, IEnumerable<NavigationCommand> commands);
    }
}
