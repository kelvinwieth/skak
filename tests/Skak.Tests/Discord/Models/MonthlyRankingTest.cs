using Skak.Discord.Models;
using Skak.Discord.Models.Dtos;
using Xunit;

namespace Skak.Tests.Discord.Models
{
    public class MonthlyRankingTest
    {
        [Fact]
        public void Constructor_OrderRankingCorrectly()
        {
            // Arrange
            var firstTournament = new LichessSwissResult()
            {
                Players = new()
                {
                    new SwissPlayer
                    {
                        Username = "inter",
                        Rank = 1,
                        Points = 7,
                    },
                    new SwissPlayer
                    {
                        Username = "mezo",
                        Rank = 2,
                        Points = 6,
                    },
                },
            };

            var secondTournament = new LichessSwissResult()
            {
                Players = new()
                {
                    new SwissPlayer
                    {
                        Username = "inter",
                        Rank = 1,
                        Points = 6,
                    },
                    new SwissPlayer
                    {
                        Username = "mezo",
                        Rank = 2,
                        Points = 5,
                    },
                    new SwissPlayer
                    {
                        Username = "pudim",
                        Rank = 3,
                        Points = 4.5,
                    },
                },
            };

            var tournaments = new List<LichessSwissResult>
            {
                firstTournament,
                secondTournament,
            };

            // Act
            var monthlyRanking = new MonthlyRanking(tournaments);

            // Assert
            Assert.Equal(3, monthlyRanking.Ranking.Count);

            Assert.Equal("inter", monthlyRanking.Ranking[0].Username);
            Assert.Equal("mezo", monthlyRanking.Ranking[1].Username);
            Assert.Equal("pudim", monthlyRanking.Ranking[2].Username);

            Assert.Equal(13, monthlyRanking.Ranking[0].TournamentPoints);
            Assert.Equal(11, monthlyRanking.Ranking[1].TournamentPoints);
            Assert.Equal(4.5, monthlyRanking.Ranking[2].TournamentPoints);

            Assert.Equal(10, monthlyRanking.Ranking[0].Points);
            Assert.Equal(8, monthlyRanking.Ranking[1].Points);
            Assert.Equal(3, monthlyRanking.Ranking[2].Points);
        }

        [Fact]
        public void Constructor_OrderWithCorrectTiebreaks()
        {
            // Arrange
            var firstTournament = new LichessSwissResult()
            {
                Players = new()
                {
                    new SwissPlayer
                    {
                        Username = "inter",
                        Rank = 1,
                        Points = 10,
                    },
                },
            };

            var secondTournament = new LichessSwissResult()
            {
                Players = new()
                {
                    new SwissPlayer
                    {
                        Username = "pudim",
                        Rank = 2,
                        Points = 1.5,
                    },
                    new SwissPlayer
                    {
                        Username = "mezo",
                        Rank = 3,
                        Points = 1,
                    },
                },
            };

            var thirdTournament = new LichessSwissResult()
            {
                Players = new()
                {
                    new SwissPlayer
                    {
                        Username = "mezo",
                        Rank = 4,
                        Points = 1,
                    },
                    new SwissPlayer
                    {
                        Username = "pudim",
                        Rank = 5,
                        Points = 1,
                    },
                },
            };

            var tournaments = new List<LichessSwissResult>
            {
                firstTournament,
                secondTournament,
                thirdTournament,
            };

            // Act
            var monthlyRanking = new MonthlyRanking(tournaments);

            // Assert
            Assert.Equal("pudim", monthlyRanking.Ranking[0].Username);
            Assert.Equal("mezo", monthlyRanking.Ranking[1].Username);
            Assert.Equal("inter", monthlyRanking.Ranking[2].Username);
        }
    }
}
