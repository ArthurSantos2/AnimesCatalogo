using Application.Dtos;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.SeedWork;

namespace Application.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AnimeService(IAnimeRepository animeRepository, IUnitOfWork unitOfWork)
        {
            _animeRepository = animeRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<AnimeDto>> GetAnimes(ParametersDto request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _animeRepository.GetAnime(request.Nome, request.Diretor,
                request.PalavraChave, request.NumeroPagina, request.QuantidadePaginas, cancellationToken);

                var result = data.Select(anime => new AnimeDto()
                {
                    nome = anime.AnimeName,
                    descricao = anime.Description,
                    diretor = anime.DirectorName
                }).ToList();

                return result;
            }
            catch (Exception)
            {
                throw new ApplicationException(ErrorCode.InternalServerError, "Houve uma falha na busca dos animes, verifique os critérios de busca");
            }

        }

        public async Task<AnimeDto> InsertAnime(AnimeDto anime, CancellationToken cancellationToken)
        {
            var entity = new Anime(anime.nome, anime.descricao,anime.diretor);
            try
            {
                await _animeRepository.CreateAnime(entity, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch(Exception)
            {
                throw new ApplicationException(ErrorCode.InternalServerError, "Não foi possível realizar a inserção");
            }

            return anime;
        }
    }
}
