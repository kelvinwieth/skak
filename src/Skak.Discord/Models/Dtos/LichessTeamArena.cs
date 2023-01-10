using Skak.Discord.Models.Enums;

namespace Skak.Discord.Models.Dtos
{
    public class LichessTeamArena : ILichessTournament
    {
        // Lichess fields
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public long StartsAt { get; set; }

        public int Status { get; set; }

        // Computed fields
        // Dont ask me, Lichess API is a mess
		public DateTime StartDate => new(StartsAt);

        public bool IsFinished => Status == 30;

		public TournamentType Type => TournamentType.Arena;
    }
}

//{
//    "id": "40THCFeT",
//    "createdBy": "dandao-7822",
//    "system": "arena",
//    "minutes": 45,
//    "clock": {
//        "limit": 300,
//        "increment": 0
//    },
//    "rated": false,
//    "fullName": "Terça dos Ataques Arena",
//    "nbPlayers": 4,
//    "variant": {
//        "key": "standard",
//        "short": "Std",
//        "name": "Standard"
//    },
//    "startsAt": 1669152600000,
//    "finishesAt": 1669155300000,
//    "status": 30,
//    "perf": {
//        "key": "blitz",
//        "name": "Blitz",
//        "position": 1,
//        "icon": ")"
//    },
//    "minRatedGames": {
//        "nb": 10
//    },
//    "teamMember": "chessclub-brpt",
//    "position": {
//        "name": "Custom position",
//        "fen": "rnbqkb1r/pp3ppp/3ppn2/8/3NP1P1/2N5/PPP2P1P/R1BQKB1R b KQkq - 0 1"
//    },
//    "winner": {
//        "id": "dandao-7822",
//        "name": "Dandao-7822",
//        "title": null
//    }
//}
