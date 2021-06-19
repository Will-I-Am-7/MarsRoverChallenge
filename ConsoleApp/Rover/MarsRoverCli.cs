using MarsRoverChallenge.ConsoleApp.Enums;
using MarsRoverChallenge.ConsoleApp.Extensions;
using MarsRoverChallenge.ConsoleApp.Interfaces;
using System.Collections.Generic;
using System.Drawing;

namespace MarsRoverChallenge.ConsoleApp.Rover
{
    public class MarsRoverCli : IRoverCli
    {
        private readonly IConsoleOutput _output;
        private readonly IConsoleInput _input;
        private readonly ICommandProcessor _commandProcessor;
        private readonly IRoverHandler _roverHandler;

        public MarsRoverCli(IConsoleOutput output, IConsoleInput input, ICommandProcessor commandProcessor, IRoverHandler roverHandler)
        {
            _output = output;
            _input = input;
            _commandProcessor = commandProcessor;
            _roverHandler = roverHandler;
        }

        public void Start()
        {
            _output.Information("Welcome to the mars rover CLI :) Enter 'x' to stop...");

            var rovers = GetRoversWithNavigation();

            if (rovers.Count > 0)
            {
                _output.Information("\nRovers: \n");
            }

            int index = 0;

            foreach (var rover in rovers)
            {
                var navigationResult = _roverHandler.NavigateRover(rover.Key, rover.Value);

                if (navigationResult.DestinationReached)
                {
                    _output.Success($"Rover {++index}: {rover.Key}");
                }
                else
                {
                    _output.Warning($"Rover {++index}: {rover.Key} - {navigationResult.Message}");
                }
            }

            VisualizeRovers();
        }

        private void VisualizeRovers()
        {
            _output.Information("\nPlateau:\n");

            for (int col = _roverHandler.UpperRightPosition.Y; col >= 0; col--)
            {
                _output.Warning($"\n{col}", true);

                for (int row = 0; row <= _roverHandler.UpperRightPosition.X; row++)
                {
                    string text = " x ";

                    if (row == 0)
                    {
                        text = "\tx ";
                    }

                    if (_roverHandler.Grid[row, col] == null)
                    {
                        _output.Information(text, true);
                    }
                    else
                    {
                        _output.Error(text.Replace('x', 'R'), true);
                    }
                }
            }

            _output.Information("\n");

            if (_roverHandler.UpperRightPosition.X > 9)
            {
                return;
            }

            for (int row = 0; row <= _roverHandler.UpperRightPosition.X; row++)
            {
                string text = $" {row} ";

                if (row == 0)
                {
                    text = $"\t{row} ";
                }

                _output.Warning(text, true);
            }
        }

        private Dictionary<IRover, IEnumerable<NavigationCommand>> GetRoversWithNavigation()
        {
            Dictionary<IRover, IEnumerable<NavigationCommand>> roversWithNavigation = new();

            var plateauCoords = GetPlateauCoords();

            if (plateauCoords == null)
            {
                return roversWithNavigation;
            }

            _roverHandler.SetPlateau(plateauCoords.Value.X, plateauCoords.Value.Y);

            while (true)
            {
                var rover = GetRover();

                if (rover == null)
                {
                    break;
                }

                var roverNavigation = GetRoverNavigation();

                if (roverNavigation == null)
                {
                    break;
                }

                roversWithNavigation.Add(rover, roverNavigation);
            }

            return roversWithNavigation;
        }

        private IEnumerable<NavigationCommand> GetRoverNavigation()
        {
            _output.Information("\nAdd navigation commands for rover: ");

            IEnumerable<NavigationCommand> navigation;

            while (true)
            {
                string userInput = _input.GetText();

                if (userInput.RemoveWhitespace().ToLower() == "x")
                {
                    navigation = new List<NavigationCommand>();
                    break;
                }

                navigation = _commandProcessor.GetRoverNavigationFromText(userInput);

                if (navigation != null)
                {
                    break;
                }

                _output.Warning("\nInvalid rover navigation entered (Example = LMRM): ");
            }

            return navigation;
        }

        private IRover GetRover()
        {
            IRover rover = null;

            _output.Information("\nAdd rover starting position ('x' to stop): ");

            while (true)
            {
                string userInput = _input.GetText();

                if (userInput.RemoveWhitespace().ToLower() == "x")
                {
                    break;
                }

                rover = _commandProcessor.GetRoverFromText(userInput);

                if (rover != null)
                {
                    break;
                }

                _output.Warning("\nInvalid rover position entered (Example = 1 1 E): ");
            }

            return rover;
        }

        private Point? GetPlateauCoords()
        {
            Point? plateauCoords = null;

            _output.Information("\nPlease enter the upper-right coordinates of the plateau: ");

            while (true)
            {
                string userInput = _input.GetText();

                if (userInput.RemoveWhitespace().ToLower() == "x")
                {
                    break;
                }

                plateauCoords = _commandProcessor.GetPlateauCoordsFromText(userInput);

                if (plateauCoords != null)
                {
                    break;
                }

                _output.Warning("\nInvalid coordinates entered (Example = 1 1): ");
            }

            return plateauCoords;
        }
    }
}
