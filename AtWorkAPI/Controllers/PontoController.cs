using AtWork.Domain.Application.Ponto.Commands;
using AtWork.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AtWorkAPI.Controllers
{
    [ApiController]
    [Route("api/ponto")]
    public class PontoController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<ObjectResponse<bool>> Create(CreatePontoCommand command) => await mediator.Send(command);

        [HttpPost("createPontoManual")]
        public async Task<ObjectResponse<bool>> CreatePontoManual(CreatePontoManualCommand command) => await mediator.Send(command);

        [HttpDelete]
        public async Task<ObjectResponse<bool>> Delete(DeletePontoCommand command) => await mediator.Send(command);
    }
}
