using MarsRoverChallenge.ConsoleApp.Enums;
using System.Collections.Generic;
using System.Drawing;

namespace MarsRoverChallenge.ConsoleApp.Interfaces
{
    public interface ICommandProcessor
    {
        public IEnumerable<NavigationCommand> GetRoverNavigationFromText(string text);
        public IRover GetRoverFromText(string text);
        public Point? GetPlateauCoordsFromText(string text);
    }
}
