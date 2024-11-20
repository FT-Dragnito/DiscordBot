using DSharpPlus.SlashCommands;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Commands
{
    public class GeneralCommands : ApplicationCommandModule
    {
        private readonly ILogger<GeneralCommands> _logger;

        public GeneralCommands(ILogger<GeneralCommands> logger)
        {
            _logger = logger;
        }

        [SlashCommand("ping", "Tester la connexion du bot.")]
        public async Task PingAsync(InteractionContext ctx)
        {
            _logger.LogInformation("La commande 'ping' a été exécutée par {User} à {Time}", ctx.User.Username, DateTime.UtcNow);

            await ctx.CreateResponseAsync("Pong!");
        }
    }
}
