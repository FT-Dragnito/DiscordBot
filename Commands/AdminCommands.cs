using DSharpPlus.SlashCommands;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Commands
{
    public class AdminCommands : ApplicationCommandModule
    {
        private readonly ILogger<AdminCommands> _logger;

        public AdminCommands(ILogger<AdminCommands> logger)
        {
            _logger = logger;
        }

        [SlashCommand("kick", "Expulser un utilisateur du serveur.")]
        public async Task KickAsync(InteractionContext ctx)
        {
            _logger.LogInformation("La commande 'kick' a été exécutée par {User} à {Time}", ctx.User.Username, DateTime.UtcNow);

            await ctx.CreateResponseAsync("L'utilisateur a été expulsé.");
        }
    }
}
