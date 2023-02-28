using DSharpPlus;
using DSharpPlus.EventArgs;
using System.Text.RegularExpressions;

namespace Skak.Discord.Handlers
{
    public class LichessChallengeHandler
    {
        public static async Task Handle(DiscordClient client, MessageCreateEventArgs args)
        {
            var content = args.Message.Content;
            var urls = content.Split(' ').Where(w => IsUrl(w));

            if (!urls.Any())
            {
                return;
            }

            foreach (var url in urls)
            {
                if (!IsLichessUrl(url))
                {
                    continue;
                }

                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);
                var html = await response.Content.ReadAsStringAsync();
                string title = Regex
                    .Match(html, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase)
                    .Groups["Title"]
                    .Value;

                if (title.Contains("Challenge from Anonymous"))
                {
                    await args.Message.DeleteAsync();
                    await args.Channel.SendMessageAsync(
                        $"Ei {args.Author.Mention}, não é permitido o envio de convites anônimos! " +
                        $"Se você quer desafiar outras pessoas, crie uma conta no Lichess. " +
                        $"Dessa forma, diminuímos a quantidade de cheaters em nosso servidor.");
                }
            }
        }

        public static bool IsLichessUrl(string url) => url.Contains("lichess.org");

        public static bool IsUrl(string text) => Uri.IsWellFormedUriString(text, UriKind.Absolute);
    }
}
