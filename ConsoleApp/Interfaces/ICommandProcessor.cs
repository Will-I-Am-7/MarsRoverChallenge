using MarsRoverChallenge.ConsoleApp.Enums;
using System.Collections.Generic;
using System.Drawing;

namespace MarsRoverChallenge.ConsoleApp.Interfaces
{
    /// <summary>
    /// Processes string input commands into known types and classes
    /// </summary>
    public interface ICommandProcessor
    {
        public IEnumerable<NavigationCommand> GetRoverNavigationFromText(string text);
        public IRover GetRoverFromText(string text);
        public Point? GetPlateauCoordsFromText(string text);
    }
}
