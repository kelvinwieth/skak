using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skak.Discord.Models
{
    public class RankedPlayer
    {
        public string Username { get; set; } = string.Empty;
        public int Points { get; set; }
        public double TournamentPoints { get; set; }
        public int TournamentsPlayed { get; set; }

        public string LichessUrl => $"https://lichess.org/@/{Username}";
    }
}
