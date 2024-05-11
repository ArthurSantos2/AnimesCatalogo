using Application.Dtos;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.SeedWork;
using System.Linq.Expressions;

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
                    Nome = anime.Name,
                    Descricao = anime.Description,
                    Diretor = anime.Director,
                    IdReference = anime.Id
                }).ToList();

                return result;
            }
            catch (Exception)
            {
                throw new ApplicationException(ErrorCode.InternalServerError, "Houve uma falha na busca dos animes, verifique os critérios de busca");
            }

        }

        public async Task<AnimeDto> InsertAnime(RequestDto anime, CancellationToken cancellationToken)
        {
            var entity = new Anime(anime.nome, anime.descricao, anime.diretor);
            try
            {
                await _animeRepository.CreateAnime(entity, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var result = new AnimeDto()
                {
                    Descricao = entity.Description,
                    Diretor = entity.Director,
                    Nome = entity.Name,
                    IdReference = entity.Id
                };

                return result;
            }
            catch (Exception)
            {
                throw new ApplicationException(ErrorCode.InternalServerError, "Não foi possível realizar a inserção");
            }
        }
        public async Task<bool> UpdateAnime(long id, RequestDto data, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _animeRepository.GetAnimeById(id, cancellationToken);

                var dto = new RequestDto()
                {
                    nome = data.nome != null ? data.nome : entity.Name,
                    diretor = data.diretor != null ? data.diretor : entity.Director,
                    descricao = data.descricao != null ? data.descricao : entity.Description
                };

                Anime anime = new Anime(dto.nome, dto.descricao, dto.diretor);

                await _animeRepository.UpdateAnime(id, anime, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception)
            {
                throw new ApplicationException(ErrorCode.InternalServerError, "Não foi possível realizar a inserção");
            }
            
        }

        public async Task<bool> DeleteAnime(long id, CancellationToken cancellationToken)
        {
            try
            {
                await _animeRepository.DeleteAnime(id, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception)
            {
                throw new ApplicationException(ErrorCode.InternalServerError, "Não foi possível realizar a inserção");
            }

        }

    }
}