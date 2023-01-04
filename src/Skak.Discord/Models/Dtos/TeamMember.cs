using System;
using Newtonsoft.Json;

namespace Skak.Discord.Models.Dtos
{
    public class TeamMember
    {
        public string Username { get; set; } = string.Empty;

        public Performances Perfs { get; set; } = new();

        public int BlitzRating => Perfs.Blitz.Rating;

        public int RapidRating => Perfs.Rapid.Rating;

        public int HighestRating => BlitzRating > RapidRating ? BlitzRating : RapidRating;
    }

    public class Performances
    {
        public Performance Blitz { get; set; } = new();

        public Performance Rapid { get; set; } = new();
    }

    public class Performance
    {
        public int Rating { get; set; } = 0;

        public bool Prov { get; set; } = false;
    }
}
