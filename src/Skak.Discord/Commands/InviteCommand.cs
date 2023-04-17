using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using Skak.Discord.Builders;
using Skak.Discord.Clients;

namespace Skak.Discord.Commands
{
    [SlashRequirePermissions(Permissions.ManageRoles)]
	public class InviteCommand : ApplicationCommandModule
	{
        private readonly ILichessClient _client;

        public InviteCommand(ILichessClient client) => _client = client;

        [SlashCommand("invite", "Envia um convite para o primeiro torneio não finalizado")]
        public async Task InviteAsync(
            InteractionContext context,
            [Option("mention", "Menção a ser usada no convite")] DiscordRole role)
		{
			await context.DeferAsync();
            var webhook = new DiscordWebhookBuilder();

            try
            {
                // Get last 20 tournaments
                var tournaments = await _client.GetLastTournamentsAsync(quantity: 20);

                // Filter by not finished, and take by the min date
                var firstNotFinished = tournaments
                    .Where(t => !t.IsFinished)
                    .MinBy(t => t.StartDate);

                // Build invite embed
                var embed = EmbedBuilder.Invite(firstNotFinished);

                // Return embed
                webhook.AddEmbed(embed);
            }
            catch (Exception ex)
            {
                var embed = EmbedBuilder.Exception(ex);
                webhook.ClearComponents();
                webhook.AddEmbed(embed);
            }

            // IDK why, but @everyone mention is returning @@everyone
            // So we need this workaround
            var mention = role.Id == context.Guild.EveryoneRole.Id ?
                "@everyone" : role.Mention;

            await context.Channel.SendMessageAsync($"{mention}");
            await context.EditResponseAsync(webhook);
        }
    }
}
