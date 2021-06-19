using MarsRoverChallenge.ConsoleApp.Enums;
using MarsRoverChallenge.ConsoleApp.Extensions;
using MarsRoverChallenge.ConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MarsRoverChallenge.ConsoleApp.Console
{
    public class ConsoleCommandProcessor : ICommandProcessor
    {
        public IEnumerable<NavigationCommand> GetRoverNavigationFromText(string text)
        {
            List<NavigationCommand> navigation = new();

            if (string.IsNullOrWhiteSpace(text))
            {
                return navigation;
            }

            text = text.RemoveWhitespace();

            foreach (char character in text)
            {
                if (!Enum.TryParse(character.ToString().ToUpper(), out NavigationCommand command))
                {
                    return null;
                }

                navigation.Add(command);
            }

            return navigation;
        }

        public IRover GetRoverFromText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            text = text.Trim();

            string[] textArr = text.Split(' ');

            if (textArr.Length != 3)
            {
                return null;
            }

            if (!int.TryParse(textArr[0], out int x))
            {
                return null;
            }

            if (!int.TryParse(textArr[1], out int y))
            {
                return null;
            }

            if (!Enum.TryParse(textArr[2].ToUpper(), out CardinalDirection direction))
            {
                return null;
            }

            if (x < 0 || y < 0)
            {
                return null;
            }

            return new Rover.MarsRover(x, y, direction);
        }

        public Point? GetPlateauCoordsFromText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            text = text.Trim();

            string[] textArr = text.Split(' ');

            if (textArr.Length != 2)
            {
                return null;
            }

            if (!int.TryParse(textArr[0], out int x))
            {
                return null;
            }

            if (!int.TryParse(textArr[1], out int y))
            {
                return null;
            }

            if (x < 0 || y < 0)
            {
                return null;
            }

            return new Point(x, y);
        }
    }
}
