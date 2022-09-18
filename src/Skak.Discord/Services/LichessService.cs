using Skak.Discord.Clients;
using Skak.Discord.Models;
using Skak.Discord.Models.Dtos;
using Skak.Discord.Models.Enums;

namespace Skak.Discord.Services
{
    public class LichessService : IChessWebsiteService
    {
        private readonly ILichessClient _lichessClient;

        public LichessService(ILichessClient lichessClient)
        {
            _lichessClient = lichessClient;
        }

        public async Task<TournamentInfo> GetTournamentAsync(string url, TournamentType type)
        {
            var tournamentId = url.Split('/').Last();

            ILichessTournament lichessTournament = type switch
            {
                TournamentType.Arena => await _lichessClient.GetArenaAsync(tournamentId),
                TournamentType.Swiss => await _lichessClient.GetSwissAsync(tournamentId),
                _ => throw new ArgumentException($"Tipo de torneio {type} não suportado."),
            };

            var info = TournamentInfo.FromLichess(lichessTournament);
            return info;
        }
    }
}
