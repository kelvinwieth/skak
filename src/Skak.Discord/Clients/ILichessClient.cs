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

		[Get("/team/{teamId}/swiss?max=1")]
		public Task<LichessSwissTournament> GetTeamLastSwissAsync(string teamId);

		[Get("/team/{teamId}/arena?max=1")]
		public Task<LichessLastTeamArena> GetTeamLastArenaAsync(string teamId);
    }
}
