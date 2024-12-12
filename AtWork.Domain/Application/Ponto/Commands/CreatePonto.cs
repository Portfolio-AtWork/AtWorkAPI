using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using MediatR;

namespace AtWork.Domain.Application.Ponto.Commands
{
    public record CreatePontoCommand(Guid ID_Funcionario) : IRequest<ObjectResponse<bool>>;

    public class CreatePontoHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePontoCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CreatePontoCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            unitOfWork.BeginTransaction();

            TB_Ponto ponto = new()
            {
                DT_Ponto = DateTime.Now,
                ST_Ponto = StatusPonto.Aprovado,
                ID_Funcionario = command.ID_Funcionario
            };

            await unitOfWork.Repository.AddAsync(ponto, cancellationToken);

            result.Value = unitOfWork.SaveChangesAsync().Ok();
            return result;
        }
    }
}
