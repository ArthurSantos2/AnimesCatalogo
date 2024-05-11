using Application.Dtos;

namespace Application.Services
{
    public interface IAnimeService
    {
        Task<List<AnimeDto>> GetAnimes(ParametersDto parameters, CancellationToken cancellationToken);
        Task<AnimeDto> InsertAnime(RequestDto anime, CancellationToken cancellationToken);
        Task<bool> UpdateAnime(long id, RequestDto data, CancellationToken cancellationToken);
        Task<bool> DeleteAnime(long id, CancellationToken cancellationToken);
    }
}