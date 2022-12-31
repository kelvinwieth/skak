using System.Threading.Channels;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Skak.Discord.Commands
{
    [SlashRequirePermissions(Permissions.ManageRoles)]
	public class InvitationCommands : ApplicationCommandModule
	{
		public static async Task InviteAsync(InteractionContext context)
		{
			await context.DeferAsync();
            var webhook = new DiscordWebhookBuilder();

            webhook.WithContent($"Command not implemented yet.");

            await context.EditResponseAsync(webhook);
        }
    }
}
