using MarsRoverChallenge.ConsoleApp.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRoverChallenge.ConsoleApp
{
    public class ConsoleApplication : IHostedService, IDisposable
    {
        private readonly IConsoleOutput _output;
        private readonly IRoverCli _roverCli;

        public ConsoleApplication(IConsoleOutput output, IRoverCli roverCli)
        {
            _output = output;
            _roverCli = roverCli;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _roverCli.Start();
            }
            catch (Exception ex)
            {
                _output.Error($"\nUnexpected error occurred, message: {ex.Message}");
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _output.Information("\n\n\nGoodbye :(");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // TODO: Cleanup?
        }
    }
}
