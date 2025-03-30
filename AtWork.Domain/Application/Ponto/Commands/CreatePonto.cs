using AtWork.Domain.Base;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using MediatR;

namespace AtWork.Domain.Application.Ponto.Commands
{
    public class CreatePontoCommand() : IRequest<ObjectResponse<bool>>;

    public class CreatePontoHandler(IUnitOfWork unitOfWork, UserInfo userInfo) : IRequestHandler<CreatePontoCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CreatePontoCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            unitOfWork.BeginTransaction();

            TB_Ponto ponto = new()
            {
                DT_Ponto = DateTime.UtcNow,
                ST_Ponto = StatusPonto.Aprovado,
                ID_Funcionario = userInfo.ID_Funcionario
            };

            await unitOfWork.Repository.AddAsync(ponto, cancellationToken);

            Exception? error = await unitOfWork.SaveChangesAsync(cancellationToken);

            result.Value = error.Ok();
            return result;
        }
    }
}
