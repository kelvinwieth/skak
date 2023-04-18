using System.Text;
using DSharpPlus.Entities;
using Skak.Discord.Models;
using Skak.Discord.Models.Dtos;
using Skak.Discord.Utils;
using static DSharpPlus.Entities.DiscordEmbedBuilder;

namespace Skak.Discord.Builders
{
    public class EmbedBuilder
    {
        private static string GetPodiumLine(int position, DiscordUser user, LichessSwissResult result)
        {
            var builder = new StringBuilder();
            builder.Append(GetPlaceEmoji(position));
            
            if (user == null)
            {
                var player = result.Players.ElementAtOrDefault(position - 1);

                if (player == null)
                {
                    return string.Empty;
                }

                builder.Append($" {player.Username}");
            }
            else
            {
                builder.Append($" <@{user.Id}>");
            }

            builder.Append("\n");
            return builder.ToString();
        }

        public static DiscordEmbed TournamentPodium(
            TournamentInfo tournament,
            string imageUrl,
            DiscordUser firstPlace,
            DiscordUser secondPlace,
            DiscordUser thirdPlace,
            LichessSwissResult result)
        {
            var firstLine = GetPodiumLine(1, firstPlace, result);
            var secondLine = GetPodiumLine(2, secondPlace, result);
            var thirdLine = GetPodiumLine(3, thirdPlace, result);

            return new DiscordEmbedBuilder
            {
                Title = $":trophy: **Pódio: {tournament.Name}** :trophy:",
                Description =
                    $":calendar: **Data**: {tournament.StartsAt:dd/MM/yyyy}\n" +
                    $":computer: **Link**: {tournament.Url}\n" +
                    $"\n" +
                    firstLine +
                    secondLine +
                    thirdLine,
                Color = new DiscordColor("#FFC300"),
                ImageUrl = imageUrl,
            };
        }

        public static DiscordEmbed Exception(Exception exception)
        {
            return new DiscordEmbedBuilder
            {
                Title = "Oops...",
                Description = $"Um erro aconteceu: {exception.Message}",
            };
        }

        public static (DiscordEmbed firstPart, DiscordEmbed secondPart) Ranking(List<TeamMember> members)
        {
            var ranking = members
                .OrderByDescending(m => m.HighestRating)
                .Take(100)
                .Select((m, i) => RankingLine(m, i + 1));

            var first50 = ranking.Take(50);
            var second50 = ranking.Skip(50).Take(50);

            var date = DateTime.UtcNow;

            var firstPart =  new DiscordEmbedBuilder
            {
                Title = "Top 100 Jogadores",
                Description = string.Join("\n", first50),
            };

            var secondPart = new DiscordEmbedBuilder
            {
                Description = string.Join("\n", second50),
                Footer = new EmbedFooter
                {
                    Text = $"Atualizado em {date:dd/MM/yyyy}",
                },
            };

            return (firstPart, secondPart);
        }

        private static string RankingLine(TeamMember member, int position)
        {
            var name = $"[{member.Username}](https://lichess.org/@/{member.Username})"; 
            return $"** {position}º ** - {name} ({member.HighestRating})";
        }

        public static DiscordEmbed Invite(ILichessTournament tournament)
        {
            // If tournament is null, return tournament not found
            if (tournament == null)
            {
                return new DiscordEmbedBuilder().WithDescription("Torneio não encontrado.");
            }

            var brasiliaDate = tournament.StartDate.ToBrasilia();
            var lisboaDate = tournament.StartDate.ToLisboa();

            return new DiscordEmbedBuilder()
            {
                Title = $":trophy: {tournament.Name} :trophy:",
                Description =
                    $"**Link:** {tournament.Url}\n" +
                    $"**Instruções para entrar:** <#825136910807728138>\n\n" +
                    $"📆 **Data:** {brasiliaDate:dd/MM/yyyy}\n" +
                    $"⌚ **Horário:** {brasiliaDate:HH:mm}h (BRA) - {lisboaDate:HH:mm}h (POR)\n" +
                    $"📋 **Formato:** {tournament.Format.ToUft8String()}\n" +
                    $"⌛ **Ritmo de Jogo:** {tournament.TimeControl}",
                ImageUrl = "https://i.imgur.com/sLxTHD3.png",
            };
        }
        
        private static string GetPlaceEmoji(int position)
        {
            switch (position)
            {
                case 1:
                    return ":first_place:";
                case 2:
                    return ":second_place:";
                case 3:
                    return ":third_place:";
                default:
                    return string.Empty;
            }
        }
    }
}
