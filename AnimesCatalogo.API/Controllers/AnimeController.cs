using Application.Dtos;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace AnimesCatalogo.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AnimeController : ControllerBase
    {
        private readonly ILogger<AnimeController> _logger;
        private readonly IAnimeService _animeService;

        public AnimeController(ILogger<AnimeController> logger, IAnimeService animeService)
        {
            _logger = logger;
            _animeService = animeService;
        }

        /// <summary>
        /// Obtenção de animes
        /// </summary>
        /// <param name="filtroNome"></param>
        /// <param name="filtroDiretor"></param>
        /// <param name="filtroPalavraChave"></param>
        /// <param name="itensPorPagina"></param>
        /// <param name="paginaAtual"></param>
        /// <returns>
        /// <response code="200">Requisição bem sucedida</response>
        /// <response code="400">A solicitação foi enviada com erro, revisar</response>
        /// <response code="401">Ausência de autorização</response>
        /// <response code="500">Erro interno</response>
        /// </returns>
        [HttpGet]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAnimes(string? filtroNome = null, string? filtroDiretor = null, string? filtroPalavraChave = null,
            int itensPorPagina = 10, int paginaAtual = 1, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new ParametersDto()
                {
                    Nome = filtroNome,
                    Diretor = filtroDiretor,
                    PalavraChave = filtroPalavraChave,
                    QuantidadePaginas = itensPorPagina,
                    NumeroPagina = paginaAtual
                };

                var response = await _animeService.GetAnimes(request, cancellationToken);

                _logger.LogInformation($"Retorno dos animes: {response}");

                return Ok(response);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(@$"Ocorreu um erro durante a busca dos animes | {ex.Message}");
                return Problem(detail: ex.Message, statusCode: 400);
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message}");
                return Problem(detail: "Houve uma falha, contate o suporte", statusCode: 500);
            }
        }

        /// <summary>
        /// Cadastro de Anime
        /// </summary>
        /// <param name="cadastro"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <response code="201">Foi cadastrado com sucesso</response>
        /// <response code="400">A solicitação foi enviada com erro, revisar</response>
        /// <response code="401">Ausência de autorização</response>
        /// <response code="500">Erro interno</response>
        /// </returns>
        [HttpPost]
        [ProducesResponseType<AnimeDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAnime([FromBody] RequestDto cadastro, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _animeService.InsertAnime(cadastro, cancellationToken);

                _logger.LogInformation($"Cadastro do anime: {response}");

                return Ok(response);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(@$"Ocorreu um erro durante o cadastro de animes | {ex.Message}");
                return Problem(detail: ex.Message, statusCode: 400);
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Ocorreu umaa falha grave durante a criação do anime | {ex.Message}");
                return Problem(detail: "Houve uma falha, contate o suporte", statusCode: 500);
            }
        }

        /// <summary>
        /// Modificar um Anime existente
        /// </summary>
        /// <param name="modificar"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <response code="202">Foi modificado com sucesso</response>
        /// <response code="400">A solicitação foi enviada com erro, revisar</response>
        /// <response code="401">Ausência de autorização</response>
        /// <response code="500">Erro interno</response>
        /// </returns>
        [HttpPut]
        [ProducesResponseType<string>(StatusCodes.Status202Accepted)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModifyAnime(int id, [FromBody] RequestDto modificar, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _animeService.UpdateAnime(id,modificar, cancellationToken);

                _logger.LogInformation($"Cadastro do anime: {response}");

                return Accepted();
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(@$"Ocorreu um erro durante a modifação do anime | {ex.Message}");
                return Problem(detail: ex.Message, statusCode: 400);
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Ocorreu umaa falha grave durante a modifação do anime | {ex.Message}");
                return Problem(detail: "Houve uma falha, contate o suporte", statusCode: 500);
            }
        }

        /// <summary>
        /// Deletar um Anime existente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <response code="202">Foi deletado com sucesso</response>
        /// <response code="400">A solicitação foi enviada com erro, revisar</response>
        /// <response code="401">Ausência de autorização</response>
        /// <response code="500">Erro interno</response>
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType<string>(StatusCodes.Status202Accepted)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAnime(int id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _animeService.DeleteAnime(id, cancellationToken);

                _logger.LogInformation($"Deletado o anime: {response}");

                return Accepted();
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(@$"Ocorreu um erro durante a exclusão de animes | {ex.Message}");
                return Problem(detail: ex.Message, statusCode: 400);
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Ocorreu umaa falha grave durante a exclusão do anime | {ex.Message}");
                return Problem(detail: "Houve uma falha, contate o suporte", statusCode: 500);
            }
        }
    }
}
