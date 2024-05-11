
using Application.Dtos;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Auth;

namespace AnimesCatalogo.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public TokenController(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Criação de Token
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <response code="200">Foi cadastrado com sucesso</response>
        /// <response code="401">Ausência de autorização</response>
        /// </returns>
        [HttpPost]
        [ProducesResponseType<AnimeDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return Unauthorized();
            }

            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password
                , false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = new TokenJwtBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("43443FDFDF34DF34343fdf344SDFSDFSDFSDFSDF4545354345SDFGDFGDFGDFGdffgfdGDFGDGR%"))
                .AddSubject("Canal Dev Net Core")
                .AddIssuer("Teste.Security.Bearer")
                .AddAudience("Teste.Security.Bearer")
                .AddExpiryInMinutes(5)
                .Builder();

                return Ok(token.value);
            }

            return Unauthorized();
        }
    }
}
