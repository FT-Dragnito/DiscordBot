using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using DiscordBot.Services;

namespace DiscordBot
{
    public class Startup : IHostedService
    {
        private readonly ILogger<Startup> _logger;
        private readonly DiscordService _discordService;

        public Startup(ILogger<Startup> logger, DiscordService discordService)
        {
            _logger = logger;
            _discordService = discordService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Démarrage du bot...");

            try
            {
                await _discordService.InitializeAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors du démarrage du bot.");
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _discordService.StopAsync();
            _logger.LogInformation("Bot arrêté.");
        }
    }
}
