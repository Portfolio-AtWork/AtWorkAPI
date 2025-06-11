using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Enums.Models;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using AtWork.Shared.Structs.Messages;
using MediatR;
using System.Data;

namespace AtWork.Domain.Application.Justificativa.Commands
{
    public record ApproveJustificativaCommand(Guid ID_Funcionario, Guid ID_Justificativa) : IRequest<ObjectResponse<bool>>;

    public class ApproveJustificativaHandler(IUnitOfWork unitOfWork) : IRequestHandler<ApproveJustificativaCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(ApproveJustificativaCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            TB_Justificativa? ponto = await unitOfWork.Repository.GetAsync<TB_Justificativa>(item => item.ID == command.ID_Justificativa && item.ID_Funcionario == command.ID_Funcionario, cancellationToken);

            if (ponto is null)
            {
                result.Value = false;
                return result;
            }

            using IDbTransaction t = unitOfWork.BeginTransaction();

            ponto.ST_Justificativa = StatusJustificativa.Aprovado;
            await unitOfWork.Repository.UpdateAsync(ponto, cancellationToken);

            bool saved = (await unitOfWork.SaveChangesAsync(cancellationToken)).Ok();

            if (saved)
            {
                result.AddNotification(MessagesStruct.SUCESSO_AO_APROVAR_REGISTRO, NotificationKind.Success);
                result.Value = true;
            }
            else
            {
                result.AddNotification(MessagesStruct.FALHA_AO_APROVAR_REGISTRO, NotificationKind.Warning);
                result.Value = false;
            }

            return result;
        }
    }
}
