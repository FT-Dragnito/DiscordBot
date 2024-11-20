using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using DotNetEnv;

namespace DiscordBot
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            DotNetEnv.Env.Load();

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .UseSerilog((context, services, configuration) =>
                {
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.FromLogContext();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<Services.DiscordService>();
                    services.AddSingleton<Services.UptimeService>();
                    services.AddHostedService<Startup>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}
