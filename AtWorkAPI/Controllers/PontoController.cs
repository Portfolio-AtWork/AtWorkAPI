using AtWork.Domain.Application.Ponto.Commands;
using AtWork.Domain.Application.Ponto.Requests;
using AtWork.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtWorkAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PontoController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ObjectResponse<List<GetPontoByFuncionarioResult>>> Index([FromQuery] GetPontoByFuncionarioRequest request) => await mediator.Send(request);

        [HttpPost]
        public async Task<ObjectResponse<bool>> Create([FromBody] CreatePontoCommand command) => await mediator.Send(command);

        [HttpPost("createPontoManual")]
        public async Task<ObjectResponse<bool>> CreatePontoManual([FromBody] CreatePontoManualCommand command) => await mediator.Send(command);

        [HttpDelete]
        public async Task<ObjectResponse<bool>> Delete([FromQuery] DeletePontoCommand command) => await mediator.Send(command);
    }
}
