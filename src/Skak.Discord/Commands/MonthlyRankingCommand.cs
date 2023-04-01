using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using Ndjson.AsyncStreams.Net.Http;
using Skak.Discord.Builders;
using Skak.Discord.Models;
using Skak.Discord.Models.Dtos;
using Skak.Discord.Models.Enums;
using Skak.Discord.Utils;

namespace Skak.Discord.Commands
{
    [SlashRequirePermissions(Permissions.ManageRoles)]
    public class MonthlyRankingCommand : ApplicationCommandModule
    {
        private readonly IHttpClientFactory _factory;

        public MonthlyRankingCommand(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        [SlashCommand("mensal", "Retorna o ranking mensal dos torneios no Lichess")]
        public async Task SendMonthlyRankingAsync
            (InteractionContext context,
            [Option("canal", "Onde a embed com o ranking será enviada")] DiscordChannel channel)
        {
            await context.DeferAsync();

            using var client = _factory.CreateClient();
            const string url = "https://lichess.org/api/team/chessclub-brpt/swiss?max=31";

            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var tournaments = new List<LichessTeamSwiss>();
                var stream = response.Content.ReadFromNdjsonAsync<LichessTeamSwiss>();
                await foreach (var tournament in stream)
                {
                    if (tournament == null) return;
                    tournaments.Add(tournament);
                }

                var now = DateTime.UtcNow.ToBrasilia();
                var daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                var startDate = new DateTime(now.Year, now.Month, 1);
                var finalDate = new DateTime(now.Year, now.Month, daysInMonth, 23, 59, 59);

                var monthTournaments = tournaments
                    .Where(
                    t => t.StartDate >= startDate &&
                    t.StartDate <= finalDate && !t.Name.Contains("CC Tour - Final") &&
                    t.IsFinished);

                if (!monthTournaments.Any())
                {
                    throw new Exception("Nenhum torneio finalizado nesse mês.");
                }

                var tournamentResults = new List<LichessSwissResult>();

                foreach (var tournament in monthTournaments)
                {
                    var result = await GetSwissResult(tournament.Id);
                    tournamentResults.Add(result);
                }

                var ranking = new MonthlyRanking(tournamentResults);
                var embed = MonthlyRankingEmbedBuilder.Build(ranking, now.ToMonth());

                var feedback = new DiscordWebhookBuilder
                {
                    Content = $"Ranking enviado com sucesso em {channel.Mention}!",
                };

                await context.EditResponseAsync(feedback);
                await channel.SendMessageAsync(embed);
            }
            catch (Exception ex)
            {
                var webhook = new DiscordWebhookBuilder();
                var embed = EmbedBuilder.Exception(ex);
                webhook.AddEmbed(embed);
                await context.EditResponseAsync(webhook);
            }
        }

        private async Task<LichessSwissResult> GetSwissResult(string id)
        {
            using var client = _factory.CreateClient();

            var url = $"https://lichess.org/api/swiss/{id}/results?nb=5";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var stream = response.Content.ReadFromNdjsonAsync<SwissPlayer>();
            var result = new LichessSwissResult();

            await foreach (var player in stream)
            {
                if (player == null)
                {
                    continue;
                }

                result.Players.Add(player);
            }

            return result;
        }
    }
}
