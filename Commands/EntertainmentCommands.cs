using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Commands
{
    public class EntertainmentCommands : ApplicationCommandModule
    {
        private readonly ILogger<EntertainmentCommands> _logger;
        private readonly HttpClient _httpClient;

        public EntertainmentCommands(ILogger<EntertainmentCommands> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        [SlashCommand("meme", "Affiche un mème aléatoire.")]
        public async Task MemeAsync(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            try
            {
                _logger.LogInformation("La commande 'meme' a été exécutée par {User} à {Time}.", ctx.User.Username, DateTime.UtcNow);

                var response = await _httpClient.GetStringAsync("https://meme-api.com/gimme");
                var json = JsonDocument.Parse(response).RootElement;
                var memeUrl = json.GetProperty("url").GetString();
                var title = json.GetProperty("title").GetString();

                var embed = new DiscordEmbedBuilder
                {
                    Title = title,
                    ImageUrl = memeUrl,
                    Color = DiscordColor.Azure
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'exécution de la commande 'meme'.");
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Impossible de récupérer un mème pour l'instant. Réessayez plus tard."));
            }
        }

        [SlashCommand("quote", "Affiche une citation inspirante ou amusante.")]
        public async Task QuoteAsync(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            try
            {
                _logger.LogInformation("La commande 'quote' a été exécutée par {User} à {Time}.", ctx.User.Username, DateTime.UtcNow);

                var response = await _httpClient.GetStringAsync("https://favqs.com/api/qotd");
                var json = JsonDocument.Parse(response).RootElement;
                var content = json.GetProperty("quote").GetProperty("body").GetString();
                var author = json.GetProperty("quote").GetProperty("author").GetString();

                var embed = new DiscordEmbedBuilder
                {
                    Title = "Citation du jour",
                    Description = $"\"{content}\"\n\n- **{author}**",
                    Color = DiscordColor.Purple
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'exécution de la commande 'quote'.");
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Impossible de récupérer une citation pour l'instant. Réessayez plus tard."));
            }
        }

        [SlashCommand("joke", "Fournit une blague aléatoire pour détendre l'atmosphère.")]
        public async Task JokeAsync(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            try
            {
                _logger.LogInformation("La commande 'joke' a été exécutée par {User} à {Time}.", ctx.User.Username, DateTime.UtcNow);

                var response = await _httpClient.GetStringAsync("https://official-joke-api.appspot.com/random_joke");
                var json = JsonDocument.Parse(response).RootElement;
                var setup = json.GetProperty("setup").GetString();
                var punchline = json.GetProperty("punchline").GetString();

                var embed = new DiscordEmbedBuilder
                {
                    Title = "Blague du jour",
                    Description = $"{setup}\n\n**{punchline}**",
                    Color = DiscordColor.Orange
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'exécution de la commande 'joke'.");
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Impossible de récupérer une blague pour l'instant. Réessayez plus tard."));
            }
        }

        [SlashCommand("fact", "Donne un fait amusant ou intéressant.")]
        public async Task FactAsync(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            try
            {
                _logger.LogInformation("La commande 'fact' a été exécutée par {User} à {Time}.", ctx.User.Username, DateTime.UtcNow);

                var response = await _httpClient.GetStringAsync("https://uselessfacts.jsph.pl/random.json?language=en");
                var json = JsonDocument.Parse(response).RootElement;
                var fact = json.GetProperty("text").GetString();

                var embed = new DiscordEmbedBuilder
                {
                    Title = "Fait amusant",
                    Description = fact,
                    Color = DiscordColor.Green
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'exécution de la commande 'fact'.");
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Impossible de récupérer un fait amusant pour l'instant. Réessayez plus tard."));
            }
        }
    }
}
