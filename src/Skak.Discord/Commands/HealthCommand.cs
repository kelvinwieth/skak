using DSharpPlus.SlashCommands;

namespace Skak.Discord.Commands
{
    public class HealthCommands : ApplicationCommandModule
    {
        [SlashCommand("ping", "Responde com \"Pong!\"")]
        public async Task PingAsync(InteractionContext context)
        {
            await context.CreateResponseAsync(":ping_pong: Pong!");
        }
    }
}
