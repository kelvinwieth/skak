using Newtonsoft.Json;
using Skak.Discord.Models.Enums;

namespace Skak.Discord.Models.Dtos
{
    public class LichessLastTeamArena : ILichessTournament
    {
        [JsonProperty("fullName")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("startsAt")]
        public long RawStartsAt { get; set; }

        [JsonProperty("isFinished")]
        public bool IsFinished { get; set; }

		[JsonIgnore]
		public TournamentType Type => TournamentType.Arena;

		[JsonIgnore]
		public DateTime StartsAt => new DateTime(RawStartsAt);
    }
}
