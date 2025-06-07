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
    public record CancelJustificativaCommand(Guid ID_Funcionario, Guid ID_Justificativa) : IRequest<ObjectResponse<bool>>;

    public class CancelJustificativaHandler(IUnitOfWork unitOfWork) : IRequestHandler<CancelJustificativaCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CancelJustificativaCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            TB_Justificativa? Justificativa = await unitOfWork.Repository.GetAsync<TB_Justificativa>(item => item.ID == command.ID_Justificativa && item.ID_Funcionario == command.ID_Funcionario, cancellationToken);

            if (Justificativa is null)
            {
                result.Value = false;
                return result;
            }

            using IDbTransaction t = unitOfWork.BeginTransaction();

            Justificativa.ST_Justificativa = StatusJustificativa.Cancelado;
            await unitOfWork.Repository.UpdateAsync(Justificativa, cancellationToken);

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
