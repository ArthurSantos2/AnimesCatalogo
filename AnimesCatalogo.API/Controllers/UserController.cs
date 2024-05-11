using Applicaion.Models;
using Application.Dtos;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace AnimesCatalogo.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Cadastro de Login
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <response code="200">Foi cadastrado com sucesso</response>
        /// <response code="400">Ausência de autorização</response>
        /// </returns>
        [HttpPost]
        [ProducesResponseType<AnimeDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AdicionaUsuario([FromBody] AddUserRequest login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password)
                || string.IsNullOrWhiteSpace(login.Document))
                return BadRequest("Falta alguns dados");


            var user = new ApplicationUser
            {
                UserName = login.Email,
                Email = login.Email,
                Document = login.Document
            };

            var resultado = await _userManager.CreateAsync(user, login.Password);

            if (resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado2 = await _userManager.ConfirmEmailAsync(user, code);

            if (resultado2.Succeeded)
                return Ok("Usuário Adicionado com Sucesso");
            else
                return BadRequest("Erro ao confirmar usuário");

        }


    }
}

