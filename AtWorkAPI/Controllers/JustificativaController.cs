using AtWork.Domain.Application.Justificativa.Commands;
using AtWork.Domain.Application.Justificativa.Requests;
using AtWork.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtWorkAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class JustificativaController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ObjectResponse<List<GetJustificativasResult>>> Index([FromQuery] GetJustificativasRequest request) => await mediator.Send(request);

        [HttpPut("approveJustificativa")]
        public async Task<ObjectResponse<bool>> ApproveJustificativa([FromBody] ApproveJustificativaCommand command) => await mediator.Send(command);

        [HttpPut("cancelJustificativa")]
        public async Task<ObjectResponse<bool>> CancelJustificativa([FromBody] CancelJustificativaCommand command) => await mediator.Send(command);
    }
}
