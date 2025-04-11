using AtWork.Domain.Base;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.DTO.Horario;
using AtWork.Shared.Enums.Models;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using AtWork.Shared.Structs.Messages;
using MediatR;

namespace AtWork.Domain.Application.Horario.Commands
{
    public record CreateHorarioCommand(List<DiaDTO> Dias) : IRequest<ObjectResponse<bool>>;

    public class CreateHorarioHandler(IUnitOfWork unitOfWork, UserInfo userInfo) : IRequestHandler<CreateHorarioCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CreateHorarioCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new(false);

            unitOfWork.BeginTransaction();

            TB_Horario? horario = await unitOfWork.Repository.AddAsync(new TB_Horario()
            {
                ST_Status = StatusRegistro.Ativo,
                ID_Usuario = userInfo.ID_Usuario
            }, cancellationToken);

            if (horario == null)
            {
                unitOfWork.Rollback();
                result.Value = false;
                return result;
            }

            foreach (DiaDTO dia in command.Dias)
            {
                TB_Horario_X_Dia horario_x_dia = new()
                {
                    ID_Horario = horario.ID,
                    Dia_Da_Semana = dia.Dia_Da_Semana,
                    Hora_Final = dia.Hora_Final,
                    Hora_Inicio = dia.Hora_Inicio,
                    ST_Status = StatusRegistro.Ativo
                };

                await unitOfWork.Repository.AddAsync(horario_x_dia, cancellationToken);
            }

            bool saved = (await unitOfWork.SaveChangesAsync(cancellationToken)).Ok();

            if (saved)
            {
                result.AddNotification(MessagesStruct.SUCESSO_AO_SALVAR_REGISTRO, NotificationKind.Success);
                result.Value = true;
            }
            else
            {
                result.AddNotification(MessagesStruct.FALHA_AO_SALVAR_REGISTRO, NotificationKind.Warning);
                result.Value = false;
            }

            return result;
        }
    }
}
