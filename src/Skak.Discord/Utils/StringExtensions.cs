using System.Text;

namespace Skak.Discord.Utils
{
    public static class StringExtensions
    {
        public static string ToUft8String(this string value)
        {
            var bytes = Encoding.Default.GetBytes(value);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
