using Application.Dtos;

namespace Application.Services
{
    public interface IAnimeService
    {
        Task<List<AnimeDto>> GetAnimes(ParametersDto parameters, CancellationToken cancellationToken);
        Task<AnimeDto> InsertAnime(AnimeDto anime, CancellationToken cancellationToken);
    }
}
