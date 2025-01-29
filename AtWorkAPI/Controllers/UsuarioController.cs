using AtWork.Domain.Application.Usuario.Commands;
using AtWork.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtWorkAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UsuarioController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ObjectResponse<bool>> Create([FromBody] CreateUsuarioCommand command) => await mediator.Send(command);
    }
}
