using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzariaAPI.DTOs;
using PizzariaAPI.Services;
using PizzariaAPI.Users;
using Swashbuckle.AspNetCore.Annotations;

namespace PizzariaAPI.Controller
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [SwaggerOperation(Summary = "Realiza o login de um usuário", Description = "Autentica um usuário e gera um token de acesso.")]
        [SwaggerResponse(200, "Login realizado com sucesso. Retorna o usuário e o token de autenticação.", typeof(object))]
        [SwaggerResponse(404, "Usuário ou senha inválidos.", typeof(object))]
        [SwaggerResponse(400, "Requisição inválida.", typeof(string))]

        public IActionResult Login([FromBody] UsuarioRequest usuarioRequest)
        {
            var usuario = UsuarioRepository.Get(usuarioRequest.nome, usuarioRequest.senha);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos." });
            }

            var token = AuthService.GenerateToken(usuario);

            usuario.Senha = "";

            return Ok(new
            {
                usuario = usuario,
                token = token
            });
        }
    }
}
