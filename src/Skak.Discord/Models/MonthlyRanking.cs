using Skak.Discord.Models.Dtos;

namespace Skak.Discord.Models
{
    public class MonthlyRanking
    {
        public MonthlyRanking(List<LichessSwissResult> tournaments)
        {
            Tournaments = tournaments;
            BuildRanking();
            OrderRanking();
        }

        public List<LichessSwissResult> Tournaments { get; set; } = new();

        public List<RankedPlayer> Ranking { get; set; } = new();

        private void BuildRanking()
        {
            Ranking.Clear();

            var playersWithResults = Tournaments
                .SelectMany(t => t.Players)
                .GroupBy(p => p.Username);

            Parallel.ForEach(playersWithResults, group =>
            {
                var username = group.Key;
                var results = group.ToList();

                var rankedPlayer = new RankedPlayer
                {
                    Username = username,
                };

                foreach (var result in results)
                {
                    rankedPlayer.TournamentsPlayed++;
                    rankedPlayer.TournamentPoints += result.Points;
                    rankedPlayer.Points += Math.Abs(result.Rank - 6);
                }

                lock (Ranking)
                {
                    Ranking.Add(rankedPlayer);
                }
            });
        }

        private void OrderRanking()
        {
            Ranking = Ranking
                .OrderByDescending(r => r.Points)
                .ThenByDescending(r => r.TournamentsPlayed)
                .ThenByDescending(r => r.TournamentPoints)
                .ToList();
        }
    }
}
