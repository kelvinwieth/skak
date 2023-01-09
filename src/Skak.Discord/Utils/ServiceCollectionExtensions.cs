using Microsoft.Extensions.DependencyInjection;
using Skak.Discord.Clients;

namespace Skak.Discord.Utils
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Add new services here
            services.AddTransient<ILichessClient, LichessClient>();
            services.AddHttpClient();

            return services;
        }
    }
}
