using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using Skak.Discord.Builders;
using Skak.Discord.Models.Enums;
using Skak.Discord.Services;

namespace Skak.Discord.Commands
{
    [SlashRequirePermissions(Permissions.ManageRoles)]
    public class PodiumCommands : ApplicationCommandModule
    {
        private readonly IChessWebsiteService _chessService;

        public PodiumCommands(IChessWebsiteService chessWebsiteService)
        {
            _chessService = chessWebsiteService;
        }

        [SlashCommand("podio", "Envia o pódio de um torneio finalizado.")]
        public async Task PostPodiumAsync(
            InteractionContext context,
            [Option("canal", "Canal onde a mensagem será enviada")] DiscordChannel channel,
            [Option("imagem", "Link da imagem a ser enviada")] string imageUrl,
            [Option("campeao", "Membro campeão do torneio")] DiscordUser firstPlace,
            [Option("vice", "Membro vice-campeão do torneio")] DiscordUser secondPlace,
            [Option("terceiro", "Membro terceiro colocado do torneio")] DiscordUser thirdPlace)
        {
            await context.DeferAsync();

            try
            {
                // Get tournament info on Lichess
                var tournamentInfo = await _chessService.GetLastTournamentAsync();

                // Build embed
                var embed = EmbedBuilder.TournamentPodium(
                    tournamentInfo,
                    imageUrl,
                    firstPlace,
                    secondPlace,
                    thirdPlace);

                // Send confirmation message
                var webhook = new DiscordWebhookBuilder();
                var buttons = new DiscordComponent[]
                {
                    new DiscordButtonComponent(ButtonStyle.Success, "send", "Enviar"),
                    new DiscordButtonComponent(ButtonStyle.Danger, "cancel", "Cancelar"),
                };

                webhook.WithContent($"Esta é uma prévia da mensagem a ser enviada no canal <#{channel.Id}>:\n");
                webhook.AddEmbed(embed);
                webhook.AddComponents(buttons);

                var message = await context.EditResponseAsync(webhook);
                var result = await message.WaitForButtonAsync();

                webhook.Clear();
                webhook.ClearComponents();

                if (result.TimedOut)
                {
                    throw new TimeoutException("Timeout excedido.");
                }

                var userAction = result.Result.Id;

                // If confirmed, send podium
                if (userAction == "send")
                {
                    await channel.SendMessageAsync(embed);
                    webhook.WithContent($"Pódio enviado com sucesso em <#{channel.Id}>!");
                    await context.EditResponseAsync(webhook);
                }
                else
                {
                    webhook.WithContent($"Ação cancelada.");
                    await context.EditResponseAsync(webhook);
                }
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
