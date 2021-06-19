namespace MarsRoverChallenge.ConsoleApp.Interfaces
{
    public interface IConsoleOutput
    {
        public void Information(string message, bool sameLine = false);
        public void Error(string message, bool sameLine = false);
        public void Warning(string message, bool sameLine = false);
        public void Success(string message, bool sameLine = false);
    }
}
