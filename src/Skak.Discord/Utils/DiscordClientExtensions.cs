using DSharpPlus;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Skak.Discord.Commands;
using Skak.Discord.Handlers;

namespace Skak.Discord.Utils
{
    public static class DiscordClientExtensions
    {
        public static DiscordClient AddSlashCommands(this DiscordClient client, IServiceProvider services)
        {
            var slashConfiguration = new SlashCommandsConfiguration { Services = services };

            var slash = client.UseSlashCommands(slashConfiguration);
            slash.RegisterCommands();

            client
                .AddMessageHandlers()
                .UseInteractivity();

            return client;
        }

        private static SlashCommandsExtension RegisterCommands(this SlashCommandsExtension slash)
        {
            // Add new command modules here
            slash.RegisterCommands<HealthCommands>();
            slash.RegisterCommands<PodiumCommands>();
            slash.RegisterCommands<InviteCommand>();
            slash.RegisterCommands<RankingCommand>();
            slash.RegisterCommands<MonthlyRankingCommand>();

            return slash;
        }

        private static DiscordClient AddMessageHandlers(this DiscordClient client)
        {
            client.MessageCreated += LichessChallengeHandler.Handle;
            return client;
        }
    }
}
