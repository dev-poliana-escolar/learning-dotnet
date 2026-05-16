using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace gerencia_alunos.Auth;

public class TokenService
{
    private readonly string secretKey;

    public TokenService(string secretKey)
    {
        this.secretKey = secretKey;
    }

    public string GenerateToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username) //coloca informações dentro do token.
            }),

            Expires = DateTime.UtcNow.AddHours(3), //tempo de expiração do token

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature // assinatura criptográfica.
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}