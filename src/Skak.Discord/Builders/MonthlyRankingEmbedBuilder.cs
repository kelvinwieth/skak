using DSharpPlus.Entities;
using Skak.Discord.Models;
using Skak.Discord.Models.Enums;
using Skak.Discord.Utils;
using System.Text;

namespace Skak.Discord.Builders
{
    public class MonthlyRankingEmbedBuilder
    {
        public static DiscordEmbed Build(MonthlyRanking ranking, Month month)
        {
            var stringBuilder = new StringBuilder();

            var position = 1;
            foreach (var player in ranking.Ranking)
            {
                var text = $"**{position}.** [{player.Username}]({player.LichessUrl}) - {player.Points} pontos";
                stringBuilder.AppendLine(text);

                if (position == 50)
                {
                    break;
                }

                position++;
            }

            return new DiscordEmbedBuilder
            {
                Title = $"Pontuação de {month.ToStringAsPortuguese()}",
                Description = stringBuilder.ToString(),
                Color = new DiscordColor("F181E3"),
                Timestamp = DateTime.Now.ToBrasilia(),
                ImageUrl = "https://i.imgur.com/UDpgrvv.png",
            };
        }
    }
}
