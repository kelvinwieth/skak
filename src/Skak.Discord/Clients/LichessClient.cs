using Ndjson.AsyncStreams.Net.Http;
using Skak.Discord.Models.Dtos;
using Skak.Discord.Models.Enums;

namespace Skak.Discord.Clients
{
    public interface ILichessClient
    {
        Task<ILichessTournament> GetLastFinishedTournamentAsync();

        Task<List<ILichessTournament>> GetLastTournamentsAsync(int quantity);

        Task<LichessSwissResult> GetSwissResultAsync(string tournamentId, int maxPlayers);
    }

    public class LichessClient : ILichessClient
    {
        private readonly IHttpClientFactory _factory;

        public LichessClient(IHttpClientFactory factory) => _factory = factory;

        // TODO: Remove this ugly method and use GetLastTournamentsAsync instead
        public async Task<ILichessTournament> GetLastFinishedTournamentAsync()
        {
            var swissTask = GetLastFinishedTournamentAsync(TournamentType.Swiss);
            var arenaTask = GetLastFinishedTournamentAsync(TournamentType.Arena);

            await Task.WhenAll(swissTask, arenaTask);

            var swiss = swissTask.Result;
            var arena = arenaTask.Result;

            return swiss?.StartDate > arena?.StartDate ? swiss : arena;
        }

        // TODO: Remove this ugly method and use GetLastTournamentsAsync instead
        public async Task<ILichessTournament> GetLastFinishedTournamentAsync(TournamentType tournamentType)
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

        public async Task<List<ILichessTournament>> GetLastTournamentsAsync(int quantity)
        {
            var tournaments = new List<ILichessTournament>();

            var swissTask = GetLastTournamentsAsync(TournamentType.Swiss, 20);
            var arenaTask = GetLastTournamentsAsync(TournamentType.Arena, 20);

            await Task.WhenAll(swissTask, arenaTask);

            var swissTournaments = swissTask.Result;
            var arenaTournaments = arenaTask.Result;

            tournaments.AddRange(swissTournaments);
            tournaments.AddRange(arenaTournaments);

            return tournaments
                .OrderByDescending(t => t.StartDate)
                .Take(quantity)
                .ToList();
        }

        public async Task<List<ILichessTournament>> GetLastTournamentsAsync(
            TournamentType tournamentType,
            int quantity)
        {
            var client = _factory.CreateClient();

            var route = LichessRoutes.TeamTournaments(tournamentType, quantity);

            var response = await client.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var tournaments = new List<ILichessTournament>();

            var stream = 
                tournamentType == TournamentType.Swiss ?
                response.Content.ReadFromNdjsonAsync<LichessTeamSwiss>() :
                response.Content.ReadFromNdjsonAsync<LichessTeamArena>()
                as IAsyncEnumerable<ILichessTournament>;

            await foreach (var tournament in stream)
            {
                if (tournament == null)
                {
                    continue;
                }

                tournaments.Add(tournament);
            }

            return tournaments;
        }

        public async Task<LichessSwissResult> GetSwissResultAsync(string tournamentId, int maxPlayers)
        {
            using var client = _factory.CreateClient();

            var url = LichessRoutes.SwissResult(tournamentId, maxPlayers);
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var stream = response.Content.ReadFromNdjsonAsync<SwissPlayer>();
            var result = new LichessSwissResult();

            await foreach (var player in stream)
            {
                if (player == null)
                {
                    continue;
                }

                result.Players.Add(player);
            }

            return result;
        }
    }
}
