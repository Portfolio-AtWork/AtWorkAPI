using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Enums.Models;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using AtWork.Shared.Structs.Messages;
using MediatR;
using System.Data;

namespace AtWork.Domain.Application.Ponto.Commands
{
    public record CancelPontoCommand(Guid ID_Funcionario, Guid ID_Ponto) : IRequest<ObjectResponse<bool>>;

    public class CancelPontoHandler(IUnitOfWork unitOfWork) : IRequestHandler<CancelPontoCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CancelPontoCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            TB_Ponto? ponto = await unitOfWork.Repository.GetAsync<TB_Ponto>(item => item.ID == command.ID_Ponto && item.ID_Funcionario == command.ID_Funcionario, cancellationToken);

            if (ponto is null)
            {
                result.Value = false;
                return result;
            }

            using IDbTransaction t = unitOfWork.BeginTransaction();

            ponto.ST_Ponto = StatusPonto.Cancelado;
            await unitOfWork.Repository.UpdateAsync(ponto, cancellationToken);

            bool saved = (await unitOfWork.SaveChangesAsync(cancellationToken)).Ok();

            if (saved)
            {
                result.AddNotification(MessagesStruct.SUCESSO_AO_CANCELAR_REGISTRO, NotificationKind.Success);
                result.Value = true;
            }
            else
            {
                result.AddNotification(MessagesStruct.FALHA_AO_CANCELAR_REGISTRO, NotificationKind.Warning);
                result.Value = false;
            }

            return result;
        }
    }
}
