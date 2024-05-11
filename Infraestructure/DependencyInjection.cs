
using Domain.Interfaces.Repositories;
using Domain.SeedWork;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class DependencyInjection
    {
        public static void AddInfraestructure(this IServiceCollection services)
        {
            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();

            services.AddDbContext<ApplicationDbContext>(ConfigureDbContext);

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IAnimeRepository, AnimeRepository>();

        }

        private static void ConfigureDbContext(IServiceProvider serviceProvider, DbContextOptionsBuilder builder)
        {
            var dbConnectionFactory = serviceProvider.GetRequiredService<IDbConnectionFactory>();

            builder.UseSqlServer(dbConnectionFactory.ConnectionString);
        }
    }

}
