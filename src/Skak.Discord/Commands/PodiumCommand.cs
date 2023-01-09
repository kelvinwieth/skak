using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using Skak.Discord.Builders;
using Skak.Discord.Clients;
using Skak.Discord.Models;

namespace Skak.Discord.Commands
{
    [SlashRequirePermissions(Permissions.ManageRoles)]
    public class PodiumCommands : ApplicationCommandModule
    {
        private readonly ILichessClient _lichessClient;

        public PodiumCommands(ILichessClient lichessClient)
        {
            _lichessClient = lichessClient;
        }

        [SlashCommand("podio", "Envia o pódio de um torneio finalizado.")]
        public async Task PostPodiumAsync(
            InteractionContext context,
            [Option("imagem", "Link da imagem a ser enviada")] string imageUrl,
            [Option("campeao", "Membro campeão do torneio")] DiscordUser firstPlace,
            [Option("vice", "Membro vice-campeão do torneio")] DiscordUser secondPlace,
            [Option("terceiro", "Membro terceiro colocado do torneio")] DiscordUser thirdPlace)
        {
            await context.DeferAsync();

            try
            {
                var tournament = await _lichessClient.GetLastFinishedTournamentAsync();

                if (tournament == null)
                {
                    throw new ArgumentException("No tournament found");
                }

                var tournamentInfo = TournamentInfo.FromLichess(tournament);
                var embed = EmbedBuilder.TournamentPodium(
                    tournamentInfo,
                    imageUrl,
                    firstPlace,
                    secondPlace,
                    thirdPlace);

                await context.Channel.SendMessageAsync(embed);
                await context.DeleteResponseAsync();
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
