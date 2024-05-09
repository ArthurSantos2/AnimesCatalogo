using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AnimesCatalogo.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAnimeService, AnimeService>();

            //services.AddScoped<ITokenJwtService, TokenJwtService>();

            //services.AddTransient<TokenJwtService>();
        }

    }
}
