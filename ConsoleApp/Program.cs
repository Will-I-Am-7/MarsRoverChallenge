using MarsRoverChallenge.ConsoleApp.Console;
using MarsRoverChallenge.ConsoleApp.Interfaces;
using MarsRoverChallenge.ConsoleApp.Rover;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace MarsRoverChallenge.ConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder().ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddEnvironmentVariables();
                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            })
            .ConfigureServices((hostingContext, services) =>
            {
                services.AddOptions();
                services.AddSingleton<IConsoleOutput, ConsoleOutput>();
                services.AddSingleton<IConsoleInput, ConsoleInput>();
                services.AddSingleton<ICommandProcessor, ConsoleCommandProcessor>();
                services.AddSingleton<IRoverHandler, MarsRoverHandler>();
                services.AddSingleton<IRoverCli, MarsRoverCli>();
                services.AddSingleton<IHostedService, ConsoleApplication>();
            });

            await builder.RunConsoleAsync();
        }
    }
}
