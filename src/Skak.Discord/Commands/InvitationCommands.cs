using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Skak.Discord.Commands
{
    [SlashRequirePermissions(Permissions.ManageRoles)]
	public class InvitationCommands : ApplicationCommandModule
	{
        [SlashCommand("invite", "Envia um convite para um torneio.")]
        public static async Task InviteAsync(InteractionContext context)
		{
			await context.DeferAsync();
            var webhook = new DiscordWebhookBuilder();

            webhook.WithContent($"Command not implemented yet.");

            await context.EditResponseAsync(webhook);
        }
    }
}
