using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DiscordBot.Services;

namespace DiscordBot.Commands
{
    public class GeneralCommands : ApplicationCommandModule
    {
        private readonly ILogger<GeneralCommands> _logger;
        private readonly UptimeService _uptimeService;

        public GeneralCommands(ILogger<GeneralCommands> logger, UptimeService uptimeService)
        {
            _logger = logger;
            _uptimeService = uptimeService;

        }

        [SlashCommand("serverinfo", "Affiche des informations sur le serveur.")]
        public async Task ServerInfoAsync(InteractionContext ctx)
        {
            var server = ctx.Guild;
            var owner = server.Owner;

            _logger.LogInformation("La commande 'serverinfo' a été exécutée par {User} à {Time}.", ctx.User.Username, DateTime.UtcNow);

            var embed = new DiscordEmbedBuilder
            {
                Title = $"Informations sur le serveur : {server.Name}",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = server.IconUrl },
                Color = DiscordColor.Blurple
            }
            .AddField("Propriétaire", owner.Mention, true)
            .AddField("Membres", server.MemberCount.ToString(), true)
            .AddField("Créé le", server.CreationTimestamp.UtcDateTime.ToString("f"), true)
            .WithTimestamp(DateTime.UtcNow);

            await ctx.CreateResponseAsync(embed);
        }

        [SlashCommand("userinfo", "Affiche des informations sur un utilisateur.")]
        public async Task UserInfoAsync(InteractionContext ctx, [Option("user", "L'utilisateur à examiner.")] DiscordUser? user = null)
        {
            user ??= ctx.User;

            _logger.LogInformation("La commande 'userinfo' a été exécutée par {User} à {Time} pour l'utilisateur {TargetUser}.", 
                ctx.User.Username, 
                DateTime.UtcNow, 
                user.Username ?? "Inconnu");

            var embed = new DiscordEmbedBuilder
            {
                Title = $"Informations sur l'utilisateur : {user.Username}",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = user.AvatarUrl },
                Color = DiscordColor.Gold
            }
            .AddField("Nom complet", $"{user.Username}#{user.Discriminator}", true)
            .AddField("ID", user.Id.ToString(), true)
            .AddField("Créé le", user.CreationTimestamp.UtcDateTime.ToString("f"), true)
            .WithTimestamp(DateTime.UtcNow);

            await ctx.CreateResponseAsync(embed);
        }

        [SlashCommand("uptime", "Affiche depuis combien de temps le bot est en ligne.")]
        public async Task UptimeAsync(InteractionContext ctx)
        {
            var uptimeSpan = _uptimeService.GetUptime();
            var uptime = $"{uptimeSpan.Days}j {uptimeSpan.Hours}h {uptimeSpan.Minutes}m {uptimeSpan.Seconds}s";

            _logger.LogInformation("La commande 'uptime' a été exécutée par {User} à {Time}.", ctx.User.Username, DateTime.UtcNow);

            await ctx.CreateResponseAsync($"Le bot est en ligne depuis **{uptime}**.");
        }
    }
}
