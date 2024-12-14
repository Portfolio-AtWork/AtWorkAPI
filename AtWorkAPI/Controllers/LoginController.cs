using AtWork.Domain.Application.Usuario.Commands;
using AtWork.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtWorkAPI.Controllers
{
    [ApiController]
    [Route("api/login")]
    [AllowAnonymous]
    public class LoginController(IMediator mediator) : ControllerBase
    {
        [HttpPost("createUsuario")]
        public async Task<ObjectResponse<bool>> Create([FromBody] CreateUsuarioCommand command) => await mediator.Send(command);
    }
}
