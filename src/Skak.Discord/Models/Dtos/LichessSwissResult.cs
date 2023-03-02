namespace Skak.Discord.Models.Dtos
{
    public class LichessSwissResult
    {
        public List<SwissPlayer> Players { get; set; } = new();
    }

    public class SwissPlayer
    {
        public int Rank { get; set; }
        public double Points { get; set; }
        public double Tiebreak { get; set; }
        public int Rating { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Performance { get; set; }
    }
}
