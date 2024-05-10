using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<Anime>> GetAnime(string? name, string? director, string? keyWord, int pagNumber, int pagQuantity, CancellationToken cancellationToken);
        Task CreateAnime(Anime anime, CancellationToken cancellationToken);
        Task UpdateAnime(Anime anime, CancellationToken cancellationToken);
        Task DeleteAnime(long idAnime, CancellationToken cancellationToken);
    }
}
