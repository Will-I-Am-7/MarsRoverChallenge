using MarsRoverChallenge.ConsoleApp.Interfaces;

namespace MarsRoverChallenge.ConsoleApp.Console
{
    public class ConsoleInput : IConsoleInput
    {
        public string GetText()
        {
            System.Console.ForegroundColor = System.ConsoleColor.DarkBlue;
            string text = System.Console.ReadLine();
            return text ?? "";
        }
    }
}
