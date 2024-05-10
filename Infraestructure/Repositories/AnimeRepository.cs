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

        public async Task<IEnumerable<Anime>> GetAnime(string? name, string? director, string? keyWord, int pagNumber, int pagQuantity, CancellationToken cancellationToken)
        {
            var query = _dbContext.Animes
                .Where(s => s.IsActive == true && 
                (s.AnimeName == name 
                || s.DirectorName == director 
                || s.Description.Contains(keyWord)))
                .Skip((pagNumber - 1) * pagQuantity) 
                .Take(pagQuantity);                 

            return await query.ToListAsync(cancellationToken);
        }

        public Task UpdateAnime(Anime anime, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task CreateAnime(Anime anime, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _dbContext.Add(anime);

            return Task.CompletedTask;
        }

        public Task DeleteAnime(long idAnime, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        
    }
}
