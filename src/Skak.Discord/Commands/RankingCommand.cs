using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using Ndjson.AsyncStreams.Net.Http;
using Skak.Discord.Builders;
using Skak.Discord.Clients;
using Skak.Discord.Models.Dtos;

namespace Skak.Discord.Commands
{
    [SlashRequirePermissions(Permissions.ManageRoles)]
	class RankingCommand : ApplicationCommandModule
	{
		private readonly IHttpClientFactory _factory;

		public RankingCommand(IHttpClientFactory factory)
		{
            _factory = factory;
		}

        [SlashCommand("ranking", "Retorna o ranking do time no Lichess.")]
		public async Task SendRankingAsync(InteractionContext context)
		{
            await context.DeferAsync();

			using var client = _factory.CreateClient();
            const string url = "https://lichess.org/api/team/chessclub-brpt/users";

			try
			{
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var members = new List<TeamMember>();
                var membersStream = response.Content.ReadFromNdjsonAsync<TeamMember>();
                await foreach (var member in membersStream)
                {
                    if (member == null) return;
                    members.Add(member);
                }

                var embed = EmbedBuilder.Ranking(members);
                await context.Channel.SendMessageAsync(embed);
            }
            catch (Exception ex)
            {
                var webhook = new DiscordWebhookBuilder();
                var embed = EmbedBuilder.Exception(ex);
                webhook.AddEmbed(embed);
                await context.EditResponseAsync(webhook);
            }
        }
	}
}
