using Skak.Discord.Models.Dtos;
using Refit;

namespace Skak.Discord.Clients
{
    public interface ILichessClient
    {
        [Get("/tournament/{id}")]
        public Task<LichessArenaTournament> GetArenaAsync(string id);

        [Get("/swiss/{id}")]
        public Task<LichessSwissTournament> GetSwissAsync(string id);
    }
}
