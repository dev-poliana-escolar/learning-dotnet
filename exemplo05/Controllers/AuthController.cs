using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using exemplo05.Data;
using exemplo05.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace exemplo05.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO usuarioDTO)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioDTO.Email && u.Senha == usuarioDTO.Senha);

            if (usuario == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim("email", usuario.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MinhaChaveSecretaMuitoLonga1234567890"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "MeuProjeto",
                audience: "MeuProjeto",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { Token = tokenString });
        }
    }
}
