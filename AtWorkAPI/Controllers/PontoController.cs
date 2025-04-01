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
        public async Task<ObjectResponse<List<GetPontosResult>>> Index([FromQuery] GetPontosRequest request) => await mediator.Send(request);

        [HttpGet("byFuncionario")]
        public async Task<ObjectResponse<List<GetPontoByFuncionarioResult>>> GetPontosByFuncionario([FromQuery] GetPontoByFuncionarioRequest request) => await mediator.Send(request);

        [HttpPost]
        public async Task<ObjectResponse<bool>> Create([FromBody] CreatePontoCommand command) => await mediator.Send(command);

        [HttpPost("createPontoManual")]
        public async Task<ObjectResponse<bool>> CreatePontoManual([FromBody] CreatePontoManualCommand command) => await mediator.Send(command);

        [HttpDelete]
        public async Task<ObjectResponse<bool>> Delete([FromQuery] DeletePontoCommand command) => await mediator.Send(command);

        [HttpPut("approvePonto")]
        public async Task<ObjectResponse<bool>> ApprovePonto([FromBody] ApprovePontoCommand command) => await mediator.Send(command);

        [HttpPut("cancelPonto")]
        public async Task<ObjectResponse<bool>> CancelPonto([FromBody] CancelPontoCommand command) => await mediator.Send(command);

    }
}
