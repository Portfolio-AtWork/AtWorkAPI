using AtWork.Domain.Application.Grupo.Commands;
using AtWork.Domain.Application.Grupo.Requests;
using AtWork.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtWorkAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class GrupoController(IMediator mediator) : ControllerBase
    {
        [HttpGet("getGruposByLogin")]
        public async Task<ObjectResponse<List<GetGruposByLoginResult>>> GetGruposByLogin([FromQuery] GetGruposByLoginRequest request) => await mediator.Send(request);

        [HttpPost("createGrupo")]
        public async Task<ObjectResponse<bool>> CreateGrupo([FromBody] CreateGrupoCommand command) => await mediator.Send(command);
    }
}
