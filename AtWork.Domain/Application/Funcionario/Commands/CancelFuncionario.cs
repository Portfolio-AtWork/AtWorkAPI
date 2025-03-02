using AtWork.Domain.Base;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Enums.Models;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using AtWork.Shared.Structs.Messages;
using MediatR;
using System.Data;

namespace AtWork.Domain.Application.Funcionario.Commands
{
    public record CancelFuncionarioCommand(Guid ID_Funcionario) : IRequest<ObjectResponse<bool>>;

    public class CancelFuncionarioHandler(IUnitOfWork unitOfWork, UserInfo userInfo) : IRequestHandler<CancelFuncionarioCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CancelFuncionarioCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            TB_Funcionario? funcionario = await unitOfWork.Repository.GetAsync<TB_Funcionario>(item => item.ID == command.ID_Funcionario && item.ID_Usuario == userInfo.ID_Usuario, cancellationToken);

            if (funcionario is null)
            {
                result.Value = false;
                return result;
            }

            using IDbTransaction t = unitOfWork.BeginTransaction();

            List<TB_Ponto> pontos = await unitOfWork.Repository.GetListAsync<TB_Ponto>(item => item.ID_Funcionario == command.ID_Funcionario, cancellationToken);
            foreach (var ponto in pontos)
            {
                ponto.ST_Ponto = StatusPonto.Cancelado;
                await unitOfWork.Repository.UpdateAsync(ponto, cancellationToken);
            }

            funcionario.ST_Status = StatusRegistro.Cancelado;
            await unitOfWork.Repository.UpdateAsync(funcionario, cancellationToken);

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
