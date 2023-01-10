using Skak.Discord.Models.Dtos;
using Skak.Discord.Utils;

namespace Skak.Discord.Models
{
    public class TournamentInfo
    {
        public static TournamentInfo FromLichess(ILichessTournament tournament)
        {
			var type = tournament.Type.ToLichessResource();
			
            return new TournamentInfo
            {
                Name = tournament.Name,
                StartsAt = tournament.StartDate,
                Url = $"https://lichess.org/{type}/{tournament.Id}",
            };
        }

        public string Name { get; set; } = string.Empty;
        public DateTime StartsAt { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}
