using Skak.Discord.Models.Enums;

namespace Skak.Discord.Utils
{
	public static class EnumExtensions
	{
		public static string ToLichessResource(this TournamentType type)
		{
			return type switch
			{
				TournamentType.Arena => "tournament",
				TournamentType.Swiss => "swiss",
				_ => throw new ArgumentException("Tournament type not supported."),
			};
		}
	}
}
