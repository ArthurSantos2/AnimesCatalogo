using Application.Dtos;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        /// <param name="nome"></param>
        /// <param name="diretor"></param>
        /// <param name="palavra_chave"></param>
        /// <param name="itensPorPagina"></param>
        /// <param name="paginaAtual"></param>
        /// <returns></returns>
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
            catch(ApplicationException ex)
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
        /// <response code="200">Received</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] AnimeDto cadastro, CancellationToken cancellationToken)
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
            catch
            {
                return Problem(detail: "Houve uma falha, contate o suporte", statusCode: 500);
            }
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] RequestDto modificar)
        {
            return Ok();
        }

        // DELETE api/<AnimeController>/5
        [HttpPut("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
