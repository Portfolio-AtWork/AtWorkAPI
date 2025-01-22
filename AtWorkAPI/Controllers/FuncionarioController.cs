using AtWork.Domain.Application.Funcionario.Request;
using AtWork.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtWorkAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class FuncionarioController(IMediator mediator) : ControllerBase
    {
        [HttpGet("getFuncionariosByGrupo")]
        public async Task<ObjectResponse<List<GetFuncionariosByGrupoResult>>> GetFuncionariosByGrupo([FromQuery] GetFuncionariosByGrupoRequest request) => await mediator.Send(request);
    }
}
