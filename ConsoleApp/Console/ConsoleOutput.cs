using MarsRoverChallenge.ConsoleApp.Interfaces;

namespace MarsRoverChallenge.ConsoleApp.Console
{
    public class ConsoleOutput : IConsoleOutput
    {
        private void Message(string message, bool sameLine = false)
        {
            if (sameLine)
            {
                System.Console.Write(message);
                return;
            }

            System.Console.WriteLine(message);
        }

        public void Error(string message, bool sameLine = false)
        {
            System.Console.ForegroundColor = System.ConsoleColor.Red;
            Message(message, sameLine);
        }

        public void Information(string message, bool sameLine = false)
        {
            System.Console.ForegroundColor = System.ConsoleColor.White;
            Message(message, sameLine);
        }

        public void Success(string message, bool sameLine = false)
        {
            System.Console.ForegroundColor = System.ConsoleColor.Green;
            Message(message, sameLine);
        }

        public void Warning(string message, bool sameLine = false)
        {
            System.Console.ForegroundColor = System.ConsoleColor.DarkYellow;
            Message(message, sameLine);
        }
    }
}
