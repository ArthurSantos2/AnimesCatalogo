using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.SeedWork;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AnimeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public async Task<List<Anime>> GetAnime(string? name, string? director, string? keyWord, int pagNumber, int pagQuantity, CancellationToken cancellationToken)
        {
            var query = _dbContext.Animes
                .Where(s => s.IsActive == true &&
                (s.Name == name
                || s.Director == director
                || s.Description.Contains(keyWord)))
                .Skip((pagNumber - 1) * pagQuantity)
                .Take(pagQuantity);

            return await query.ToListAsync(cancellationToken);
        }

        public Task UpdateAnime(long id, Anime anime, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _dbContext.Animes
                .Where(b => b.Id == id)
                .ExecuteUpdate(s => s.SetProperty(b => b.Name, anime.Name)
                                        .SetProperty(b => b.Director, anime.Director)
                                            .SetProperty(b => b.Description, anime.Description));

            return Task.CompletedTask;
        }
        public Task CreateAnime(Anime anime, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _dbContext.Add(anime);

            return Task.CompletedTask;
        }

        public async Task<Anime> GetAnimeById(long id, CancellationToken cancellationToken)
        {
            var anime = await _dbContext.Animes
                .Where(s => s.IsActive == true && s.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            return anime;
        }

        public Task DeleteAnime(long idAnime, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _dbContext.Animes
                .Where(b => b.Id == idAnime)
                .ExecuteUpdate(s => s.SetProperty(b => b.IsActive, false));

            return Task.CompletedTask;
        }


    }
}