using Newtonsoft.Json;

namespace Skak.Discord.Models.Dtos
{
    public class LichessSwissTournament : ILichessTournament
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("startsAt")]
        public DateTime StartsAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;

        [JsonIgnore]
        public bool IsFinished => Status == "finished";
    }
}
