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

        public async Task<IEnumerable<Anime>> GetAnime(string seachParameter, int pagNumber, int pagQuantity)
        {
            var query = _dbContext.Animes
                .Where(s => s.IsActive == true && 
                (s.AnimeName == seachParameter 
                || s.DirectorName == seachParameter 
                || s.Description.Contains(seachParameter)))
                .Skip((pagNumber - 1) * pagQuantity) 
                .Take(pagQuantity);                 

            return await query.ToListAsync();
        }

        public Task UpdateAnime(Anime anime)
        {
            throw new NotImplementedException();
        }
        public Task CreateAnime(Anime anime)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAnime(long idAnime)
        {
            throw new NotImplementedException();
        }

        
    }
}
