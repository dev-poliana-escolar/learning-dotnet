using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gerencia_alunos.Auth;

namespace gerencia_alunos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel login)
        {
            if (login.Username == "admin" &&
                login.Password == "123456")
            {
                var token = _tokenService.GenerateToken(login.Username);

                return Ok(new
                {
                    token = token
                });
            }

            return Unauthorized("Usuário ou senha inválidos");
        }
    }
}
