using Application.Dtos;
using Domain.Interfaces.Repositories;

namespace Application.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeService(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }
        public async Task<List<AnimeDto>> GetAnimes(string? seachParameter, int pagNumber, int pagQuantity)
        {
            seachParameter = seachParameter ?? string.Empty;
            seachParameter = seachParameter.Trim();

            if (string.IsNullOrEmpty(seachParameter))
                throw new ArgumentNullException("É necessário preencher ao menos um dos critérios de busca");

            var data = await _animeRepository.GetAnime(seachParameter, pagNumber, pagQuantity);

            var result = data.Select(anime => new AnimeDto()
            {
                nome = anime.AnimeName,
                descricao = anime.Description,
                diretor = anime.DirectorName
            }).ToList();

            return result;
        }
    }
}
