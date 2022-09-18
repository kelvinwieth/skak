using DSharpPlus.SlashCommands;

namespace Skak.Discord.Models.Enums
{
    public enum TournamentType
    {
        [ChoiceName("Arena")]
        Arena,

        [ChoiceName("Suíço")]
        Swiss,
    }
}
