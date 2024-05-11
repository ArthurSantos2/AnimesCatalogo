using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IAnimeRepository
    {
        Task<List<Anime>> GetAnime(string? name, string? director, string? keyWord, int pagNumber, int pagQuantity, CancellationToken cancellationToken);
        Task CreateAnime(Anime anime, CancellationToken cancellationToken);
        Task UpdateAnime(long id, Anime anime, CancellationToken cancellationToken);
        Task<Anime> GetAnimeById(long id, CancellationToken cancellationToken);
        Task DeleteAnime(long idAnime, CancellationToken cancellationToken);
    }
}