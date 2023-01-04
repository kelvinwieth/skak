using System;
using Newtonsoft.Json;

namespace Skak.Discord.Models.Dtos
{
    public class TeamMember
    {
        public string Username { get; set; } = string.Empty;

        public Performances Perfs { get; set; } = new();

        private Performance Blitz => Perfs.Blitz;

        private Performance Rapid => Perfs.Rapid;

        public int BlitzRating => Blitz.Prov ? 0 : Blitz.Rating;

        public int RapidRating => Rapid.Prov ? 0 : Rapid.Rating;

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
