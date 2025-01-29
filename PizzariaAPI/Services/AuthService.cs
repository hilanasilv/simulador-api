using Microsoft.IdentityModel.Tokens;
using PizzariaAPI.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PizzariaAPI.Services
{
    // Classe estática de serviços relacionados à autenticação
    public static class AuthService
    {
        // Método responsável por gerar um token JWT com base nos dados do usuário
        public static string GenerateToken(Usuario usuario)
        {
            // Manipulador para criar e validar tokens JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtConfig.ChaveSecreta);// Converte a chave secreta para bytes
            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Role, usuario.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
