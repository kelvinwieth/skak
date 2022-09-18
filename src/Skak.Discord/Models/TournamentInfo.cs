using Skak.Discord.Models.Dtos;

namespace Skak.Discord.Models
{
    public class TournamentInfo
    {
        public static TournamentInfo FromLichess(ILichessTournament tournament)
        {
            return new TournamentInfo
            {
                Name = tournament.Name,
                StartsAt = tournament.StartsAt,
                Url = $"https://lichess.org/tournament/{tournament.Id}",
            };
        }

        public string Name { get; set; } = string.Empty;
        public DateTime StartsAt { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}
