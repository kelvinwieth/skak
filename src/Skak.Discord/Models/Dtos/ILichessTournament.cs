namespace Skak.Discord.Models.Dtos
{
    public interface ILichessTournament
    {
        string Id { get; }
        string Name { get; }
        DateTime StartsAt { get; }
        bool IsFinished { get; }
    }
}
