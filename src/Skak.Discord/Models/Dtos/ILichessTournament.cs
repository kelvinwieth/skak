using Skak.Discord.Models.Enums;

namespace Skak.Discord.Models.Dtos
{
    public interface ILichessTournament
    {
        string Id { get; }

        string Name { get; }

        DateTime StartDate { get; }

        bool IsFinished { get; }

		TournamentType Type { get; }
    }
}
