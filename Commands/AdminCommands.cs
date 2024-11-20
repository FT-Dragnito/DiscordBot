using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
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
        public async Task KickAsync(InteractionContext ctx, [Option("user", "L'utilisateur à expulser")] DiscordUser user)
        {
            var isAdmin = ctx.Member.Permissions.HasPermission(Permissions.Administrator);

            if (!isAdmin)
            {
                _logger.LogWarning("L'utilisateur {User} a tenté d'utiliser la commande 'kick' sans avoir les permissions nécessaires.", ctx.User.Username);
                await ctx.CreateResponseAsync("❌ Vous n'avez pas les permissions nécessaires pour utiliser cette commande.", true);
                return;
            }

            if (ctx.User.Id == user.Id)
            {
                _logger.LogWarning("L'utilisateur {User} a tenté de s'auto-expulser.", ctx.User.Username);
                await ctx.CreateResponseAsync("❌ Vous ne pouvez pas vous expulser vous-même.", true);
                return;
            }

            _logger.LogInformation("La commande 'kick' a été exécutée par {User} pour expulser {TargetUser} à {Time}.", ctx.User.Username, user.Username, DateTime.UtcNow);

            var member = await ctx.Guild.GetMemberAsync(user.Id);
            if (member == null)
            {
                _logger.LogWarning("L'utilisateur spécifié ({TargetUser}) n'est pas trouvé dans le serveur.", user.Username);
                await ctx.CreateResponseAsync("❌ L'utilisateur spécifié n'est pas trouvé dans ce serveur.", true);
                return;
            }

            await member.RemoveAsync("Expulsé par commande admin.");
            await ctx.CreateResponseAsync($"✅ L'utilisateur {user.Username} a été expulsé du serveur.");
        }
    }
}
