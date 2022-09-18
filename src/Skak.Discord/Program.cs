using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using Skak.Discord.Configuration;
using Skak.Discord.Utils;

var services = new ServiceCollection()
    .AddServices()
    .BuildServiceProvider();

var client = new DiscordClient(ConfigurationManager.Discord.BotConfiguration)
    .AddSlashCommands(services);

await client.ConnectAsync();
await Task.Delay(-1);
