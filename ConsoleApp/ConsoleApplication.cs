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
        private readonly IHostApplicationLifetime _lifeTime;

        public ConsoleApplication(IConsoleOutput output, IRoverCli roverCli, IHostApplicationLifetime lifeTime)
        {
            _output = output;
            _roverCli = roverCli;
            _lifeTime = lifeTime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _roverCli.Start();
                _lifeTime.StopApplication();
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
