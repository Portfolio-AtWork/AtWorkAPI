using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Enums.Models;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using MediatR;

namespace AtWork.Domain.Application.Ponto.Commands
{
    public record CreatePontoManualCommand(Guid ID_Funcionario, DateTime DT_Ponto) : IRequest<ObjectResponse<bool>>;

    public class CreatePontoManualHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePontoManualCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CreatePontoManualCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            bool valida = ValidaDataPonto(command, result);

            if (!valida)
            {
                return result;
            }

            unitOfWork.BeginTransaction();

            TB_Ponto ponto = new()
            {
                DT_Ponto = command.DT_Ponto,
                ST_Ponto = StatusPonto.PendenteAprovacao,
                ID_Funcionario = command.ID_Funcionario,
            };

            await unitOfWork.Repository.AddAsync(ponto, cancellationToken);

            result.Value = unitOfWork.SaveChangesAsync().Ok();
            return result;
        }

        private static bool ValidaDataPonto(CreatePontoManualCommand command, ObjectResponse<bool> result)
        {
            if (command.DT_Ponto.Date > DateTime.Today)
            {
                result.Notifications.Add(new Notification("O dia da data não pode passar do dia de hoje", NotificationKind.Warning));
                return false;
            }

            return true;
        }
    }
}
