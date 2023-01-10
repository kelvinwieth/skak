using Ndjson.AsyncStreams.Net.Http;
using Skak.Discord.Models.Dtos;
using Skak.Discord.Models.Enums;

namespace Skak.Discord.Clients
{
    public interface ILichessClient
    {
        Task<ILichessTournament?> GetLastFinishedTournamentAsync();
    }

    public class LichessClient : ILichessClient
    {
        private readonly IHttpClientFactory _factory;

        public LichessClient(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<ILichessTournament?> GetLastFinishedTournamentAsync()
        {
            var swissTask = GetLastFinishedTournamentAsync(TournamentType.Swiss);
            var arenaTask = GetLastFinishedTournamentAsync(TournamentType.Arena);

            await Task.WhenAll(swissTask, arenaTask);

            var swiss = swissTask.Result;
            var arena = arenaTask.Result;

            return swiss?.StartDate > arena?.StartDate ? swiss : arena;
        }

        public async Task<ILichessTournament?> GetLastFinishedTournamentAsync(TournamentType tournamentType)
        {
            var client = _factory.CreateClient();
            var route = LichessRoutes.TeamTournaments(tournamentType, 20);

            var response = await client.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var tournaments = new List<ILichessTournament>();

            IAsyncEnumerable<ILichessTournament> stream =
                tournamentType == TournamentType.Swiss ?
                response.Content.ReadFromNdjsonAsync<LichessTeamSwiss>() :
                response.Content.ReadFromNdjsonAsync<LichessTeamArena>();

            await foreach (var tournament in stream)
            {
                if (tournament == null)
                {
                    continue;
                }

                tournaments.Add(tournament);
            }

            return tournaments
                .Where(t => t.IsFinished)
                .MaxBy(t => t.StartDate);
        }
    }
}
