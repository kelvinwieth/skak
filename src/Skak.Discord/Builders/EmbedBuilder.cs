using System;
using DSharpPlus.Entities;
using Skak.Discord.Models;
using Skak.Discord.Models.Dtos;
using static DSharpPlus.Entities.DiscordEmbedBuilder;

namespace Skak.Discord.Builders
{
    public class EmbedBuilder
    {
        public static DiscordEmbed TournamentPodium(
            TournamentInfo tournament,
            string imageUrl,
            DiscordUser? firstPlace,
            DiscordUser? secondPlace,
            DiscordUser? thirdPlace)
        {
            var firstLine = firstPlace != null ?
                $":first_place: <@{firstPlace.Id}>\n" :
                string.Empty;

            var secondLine = secondPlace != null ?
                $":second_place: <@{secondPlace.Id}>\n" :
                string.Empty;

            var thirdLine = thirdPlace != null ?
                $":third_place: <@{thirdPlace.Id}>" :
                string.Empty;

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

        public static DiscordEmbed Ranking(List<TeamMember> members)
        {
            var ranking = members
                .OrderByDescending(m => m.HighestRating)
                .Take(100)
                .Select((m, i) => RankingLine(m, i + 1));

            var date = DateTime.UtcNow;

            return new DiscordEmbedBuilder
            {
                Title = "Top 100 Jogadores",
                Description = string.Join("\n", ranking),
                Footer = new EmbedFooter
                {
                    Text = $"Atualizado em {date:dd/MM/yyyy}",
                },
            };
        }

        private static string RankingLine(TeamMember member, int position)
        {
            var name = $"[{member.Username}](https://lichess.org/@/{member.Username})"; 
            return $"** {position}º ** - {name} ({member.HighestRating})";
        }
    }
}
