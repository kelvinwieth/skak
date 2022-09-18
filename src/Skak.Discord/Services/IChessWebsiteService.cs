using Skak.Discord.Models;
using Skak.Discord.Models.Enums;

namespace Skak.Discord.Services
{
    public interface IChessWebsiteService
    {
        Task<TournamentInfo> GetTournamentAsync(string url, TournamentType type);
    }
}
