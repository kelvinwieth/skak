using Skak.Discord.Models.Enums;

namespace Skak.Discord.Clients
{
    public static class LichessRoutes
    {
        public const string BaseUrl = "https://lichess.org/api";

        public const string TeamName = "chessclub-brpt";

        public static string SwissTournaments(int maxTournaments)
        {
            return $"{BaseUrl}/team/{TeamName}/swiss?max={maxTournaments}";
        }

        public static string ArenaTournaments(int maxTournaments)
        {
            return $"{BaseUrl}/team/{TeamName}/arena?max={maxTournaments}";
        }

        public static string TeamTournaments(TournamentType type, int maxTournaments)
        {
            return type switch
            {
                TournamentType.Swiss => SwissTournaments(maxTournaments),
                TournamentType.Arena => ArenaTournaments(maxTournaments),
                _ => throw new ArgumentException("Unsupported tournament type"),
            };
        }
    }
}
