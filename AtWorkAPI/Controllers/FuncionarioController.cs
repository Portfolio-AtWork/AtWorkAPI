using AtWork.Domain.Application.Funcionario.Commands;
using AtWork.Domain.Application.Funcionario.Requests;
using AtWork.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtWorkAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class FuncionarioController(IMediator mediator) : ControllerBase
    {
        [HttpGet("getFuncionariosByGrupo")]
        public async Task<ObjectResponse<List<GetFuncionariosByGrupoResult>>> GetFuncionariosByGrupo([FromQuery] GetFuncionariosByGrupoRequest request) => await mediator.Send(request);

        [HttpPost("createFuncionario")]
        public async Task<ObjectResponse<bool>> CreateFuncionario([FromBody] CreateFuncionarioCommand command) => await mediator.Send(command);
    }
}
