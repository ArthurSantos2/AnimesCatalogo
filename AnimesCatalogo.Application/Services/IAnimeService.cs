using Application.Dtos;

namespace Application.Services
{
    public interface IAnimeService
    {
        Task<List<AnimeDto>> GetAnimes(string seachParameter, int pagNumber, int pagQuantity);
    }
}
