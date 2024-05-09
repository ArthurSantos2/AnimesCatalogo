using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<Anime>> GetAnime(string seachParameter, int pagNumber, int pagQuantity);
        Task CreateAnime(Anime anime);
        Task UpdateAnime(Anime anime);
        Task DeleteAnime(long idAnime);
    }
}
