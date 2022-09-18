using Microsoft.Extensions.DependencyInjection;
using Skak.Discord.Clients;
using Skak.Discord.Configuration;
using Skak.Discord.Services;
using Refit;

namespace Skak.Discord.Utils
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Add new services here
            services.AddRefitClients();
            services.AddTransient<IChessWebsiteService, LichessService>();

            return services;
        }

        private static IHttpClientBuilder AddRefitClients(this IServiceCollection services)
        {
            return services
                .AddRefitClient<ILichessClient>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(ConfigurationManager.Lichess.BaseUrl);
                });
        }
    }
}
