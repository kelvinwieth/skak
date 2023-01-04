using DSharpPlus;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Skak.Discord.Commands;

namespace Skak.Discord.Utils
{
    public static class DiscordClientExtensions
    {
        public static DiscordClient AddSlashCommands(this DiscordClient client, IServiceProvider services)
        {
            var slashConfiguration = new SlashCommandsConfiguration { Services = services };

            var slash = client.UseSlashCommands(slashConfiguration);
            slash.RegisterCommands();

            client.UseInteractivity();
            return client;
        }

        private static SlashCommandsExtension RegisterCommands(this SlashCommandsExtension slash)
        {
            // Add new command modules here
            slash.RegisterCommands<HealthCommands>();
            slash.RegisterCommands<PodiumCommands>();
            slash.RegisterCommands<InvitationCommands>();
            slash.RegisterCommands<RankingCommand>();

            return slash;
        }
    }
}
