using Skak.Discord.Models.Enums;

namespace Skak.Discord.Models.Dtos
{
    public class LichessTeamSwiss : ILichessTournament
    {
        // Lichess fields
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public DateTime StartsAt { get; set; }

        public string Status { get; set; } = string.Empty;

        // Computed fields
        // Dont ask me, Lichess API is a mess
        public DateTime StartDate => StartsAt;

        public bool IsFinished => Status == "finished";

		public TournamentType Type => TournamentType.Swiss;
    }
}

//{
//    "id": "ZD1ad7WW",
//    "createdBy": "intermezo",
//    "startsAt": "2023-01-08T21:30:00.000Z",
//    "name": "Sunday Chess Club",
//    "clock": {
//        "limit": 600,
//        "increment": 5
//    },
//    "variant": "standard",
//    "round": 3,
//    "nbRounds": 3,
//    "nbPlayers": 6,
//    "nbOngoing": 0,
//    "status": "finished",
//    "stats": {
//        "absences": 4,
//        "averageRating": 1784,
//        "byes": 0,
//        "blackWins": 6,
//        "games": 6,
//        "draws": 2,
//        "whiteWins": 4
//    },
//    "rated": true
//}
