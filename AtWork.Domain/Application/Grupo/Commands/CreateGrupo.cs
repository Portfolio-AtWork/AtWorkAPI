using AtWork.Domain.Base;
using AtWork.Domain.Database;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Enums.Models;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using AtWork.Shared.Structs.Messages;
using MediatR;
using System.Data;

namespace AtWork.Domain.Application.Grupo.Commands
{
    public record CreateGrupoCommand(string Nome) : IRequest<ObjectResponse<bool>>;

    public class CreateGrupoHandler(DatabaseContext db, IUnitOfWork unitOfWork, UserInfo userInfo) : IRequestHandler<CreateGrupoCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CreateGrupoCommand request, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            using IDbTransaction transaction = unitOfWork.BeginTransaction();

            TB_Grupo tb_grupo = new() { Nome = request.Nome, ST_Status = StatusRegistro.Ativo };
            TB_Grupo? grupo = await unitOfWork.Repository.AddAsync(tb_grupo, cancellationToken);

            if (grupo is null)
            {
                transaction.Rollback();

                result.AddNotification(MessagesStruct.ERRO_AO_CADASTRAR_GRUPO, NotificationKind.Error);
                result.Value = false;
                return result;
            }

            TB_Grupo_X_Admin tb_grupo_x_admin = new() { ID_Grupo = grupo.ID, ID_Usuario = userInfo.ID_Usuario };
            TB_Grupo_X_Admin? grupo_x_adm = await unitOfWork.Repository.AddAsync(tb_grupo_x_admin, cancellationToken);

            if (grupo_x_adm is null)
            {
                transaction.Rollback();

                result.AddNotification(MessagesStruct.ERRO_AO_CADASTRAR_GRUPO_X_ADM, NotificationKind.Error);
                result.Value = false;
                return result;
            }

            Exception? error = await unitOfWork.SaveChangesAsync(cancellationToken);

            result.Value = error.Ok();
            return result;
        }
    }
}
