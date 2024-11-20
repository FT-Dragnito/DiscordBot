using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Services
{
    public class DiscordService
    {
        private readonly ILogger<DiscordService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _services;
        private DiscordClient? _discordClient;

        public DiscordService(ILogger<DiscordService> logger, IConfiguration configuration, IServiceProvider services)
        {
            _logger = logger;
            _configuration = configuration;
            _services = services;
        }

        public async Task InitializeAsync()
        {
            var token = Environment.GetEnvironmentVariable("TOKEN");
            if (string.IsNullOrEmpty(token))
                throw new Exception("Le token du bot est absent des variables d'environnement.");

            _discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged,
                MinimumLogLevel = LogLevel.None
            });

            var slashCommands = _discordClient.UseSlashCommands(new SlashCommandsConfiguration
            {
                Services = _services
            });

            slashCommands.RegisterCommands<Commands.GeneralCommands>();
            slashCommands.RegisterCommands<Commands.AdminCommands>();
            slashCommands.RegisterCommands<Commands.EntertainmentCommands>();

            _discordClient.Ready += OnClientReady;

            await _discordClient.ConnectAsync();

            _logger.LogInformation("Bot connecté à Discord.");
        }

        private Task OnClientReady(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            _logger.LogInformation("Le bot est prêt et connecté !");
            return Task.CompletedTask;
        }

        public async Task StopAsync()
        {
            if (_discordClient != null)
            {
                await _discordClient.DisconnectAsync();
                _discordClient.Dispose();
            }
        }
    }
}
