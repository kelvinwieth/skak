using Newtonsoft.Json;

namespace Skak.Discord.Models.Dtos
{
    public class LichessArenaTournament : ILichessTournament
    {
        [JsonProperty("fullName")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("startsAt")]
        public DateTime StartsAt { get; set; }

        [JsonProperty("isFinished")]
        public bool IsFinished { get; set; }
    }
}
