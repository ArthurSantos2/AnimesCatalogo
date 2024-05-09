using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimesCatalogo.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly ILogger<AnimeController> _logger;
        private readonly IAnimeService _animeService;

        public AnimeController(ILogger<AnimeController> logger, IAnimeService animeService)
        {
            _logger = logger;
            _animeService = animeService;
        }

        // GET: api/<AnimeController>
        [HttpGet]
        public async Task<IActionResult> GetAnimes()
        {
            try
            {
                var response = _animeService.GetAnimes("Picachu", 1,1);

                if (!response.IsCompletedSuccessfully)
                {
                    _logger.LogInformation(@$"Ocorreu um erro durante a busca dos animes |
                        {response.Exception?.InnerException?.InnerException}");
                    return Problem(detail: response.Exception?.InnerException?.InnerException?.ToString(), statusCode: 400);
                }

                _logger.LogInformation($"Retorno dos animes: {response}");

                return Ok(response);
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message}");
                return Problem(detail: exception.Message, statusCode: 500);
            }
        }

        // POST api/<AnimeController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return Ok();
        }

        // PUT api/<AnimeController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        //// DELETE api/<AnimeController>/5
        //[HttpPut("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    return Ok();
        //}
    }
}
