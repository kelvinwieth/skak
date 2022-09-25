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

		public async Task<TournamentInfo> GetLastTournamentAsync()
		{
			// TODO: Add database and save multiple teams
			var teamId = "chessclub-brpt";

			// Get last swiss and last arena
			var swissTask = _lichessClient.GetTeamLastSwissAsync(teamId);
			var arenaTask = _lichessClient.GetTeamLastArenaAsync(teamId);

			await Task.WhenAll(swissTask, arenaTask);

			var swiss = swissTask.Result;
			var arena = arenaTask.Result;

			// Proceed with most recent
			var mostRecent = swiss.StartsAt > arena.StartsAt ? swiss : arena as ILichessTournament;
			
			var info = TournamentInfo.FromLichess(mostRecent);
			return info;
		}
    }
}
