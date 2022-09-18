using DSharpPlus;

namespace Skak.Discord.Configuration
{
    public static class ConfigurationManager
    {
        public static class Discord
        {
            private static string BotToken =>
            	Environment.GetEnvironmentVariable("SKAK_TOKEN") ??
            	throw new ArgumentException("Missing SKAK_TOKEN variable");

            public static DiscordConfiguration BotConfiguration => new()
            {
                Token = BotToken,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All,
            };
        }

        public struct Lichess
        {
            public const string BaseUrl = "https://lichess.org/api";
        }
    }
}
