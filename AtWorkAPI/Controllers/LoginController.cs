using AtWork.Domain.Application.Login.Request;
using AtWork.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AtWorkAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class LoginController(IMediator mediator, IConfiguration configuration) : ControllerBase
    {
        [HttpPost("auth")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            ObjectResponse<AuthResult?> login = await mediator.Send(request);

            if (login.Value is null)
            {
                return BadRequest(new
                {
                    token = "",
                    nome = "",
                    email = "",
                    login = login.Value,
                    notifications = login.Notifications,
                    ok = login.Ok,
                });
            }

            Claim[] claims =
            [
                new Claim(JwtRegisteredClaimNames.Sub, login.Value.Login), // Sub = Subject
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique ID para o token
                new Claim("login", login.Value.Login), // Adiciona o login como claim personalizada
                new Claim("nome", login.Value.Nome),
                new Claim("tp_login", login.Value.TP_Login)
            ];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                login = login.Value,
                email = login.Value.Email,
                notifications = login.Notifications,
                ok = login.Ok,
                nome = login.Value.Nome
            });
        }
    }
}
