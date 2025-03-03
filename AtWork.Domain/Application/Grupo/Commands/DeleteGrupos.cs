using AtWork.Domain.Base;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Enums.Models;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs.Messages;
using MediatR;
using System.Data;

namespace AtWork.Domain.Application.Grupo.Commands
{
    public record DeleteGruposCommand(List<Guid> ListaGrupos) : IRequest<ObjectResponse<bool>>;

    public class DeleteGruposHandler(IUnitOfWork unitOfWork, UserInfo userInfo) : IRequestHandler<DeleteGruposCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(DeleteGruposCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            using IDbTransaction t = unitOfWork.BeginTransaction();

            await ProcessaDelete(command.ListaGrupos, cancellationToken);

            bool saved = (await unitOfWork.SaveChangesAsync(cancellationToken)).Ok();

            if (saved)
            {
                result.AddNotification(MessagesStruct.SUCESSO_AO_DELETAR_REGISTRO, NotificationKind.Success);
                result.Value = true;
            }
            else
            {
                result.AddNotification(MessagesStruct.FALHA_AO_DELETAR_REGISTRO, NotificationKind.Warning);
                result.Value = false;
            }
            return result;
        }

        private async Task ProcessaDelete(List<Guid> ListaGrupos, CancellationToken ct)
        {
            if (ct.IsCancellationRequested) return;

            foreach (Guid id_grupo in ListaGrupos)
            {
                List<TB_Funcionario> funcionarios = await unitOfWork.Repository.GetListAsync<TB_Funcionario>(item => item.ID_Grupo == id_grupo, ct);

                foreach (TB_Funcionario funcionario in funcionarios)
                {
                    List<TB_Ponto> pontos = await unitOfWork.Repository.GetListAsync<TB_Ponto>(item => item.ID_Funcionario == funcionario.ID, ct);

                    foreach (TB_Ponto ponto in pontos)
                    {
                        await unitOfWork.Repository.DeleteAsync(ponto, ct);
                    }

                    await unitOfWork.Repository.DeleteAsync(funcionario, ct);
                }

                TB_Grupo_X_Admin? vinculo = await unitOfWork.Repository.GetAsync<TB_Grupo_X_Admin>(item => item.ID_Grupo == id_grupo && item.ID_Usuario == userInfo.ID_Usuario, ct);
                TB_Grupo? grupo = await unitOfWork.Repository.GetAsync<TB_Grupo>(item => item.ID == id_grupo, ct);

                if (vinculo is not null)
                    await unitOfWork.Repository.DeleteAsync(vinculo, ct);

                if (grupo is not null)
                    await unitOfWork.Repository.DeleteAsync(grupo, ct);
            }
        }
    }
}
