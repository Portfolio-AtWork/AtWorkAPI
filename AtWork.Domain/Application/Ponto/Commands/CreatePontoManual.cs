using AtWork.Domain.Base;
using AtWork.Domain.Database;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.Services.Validator;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AtWork.Domain.Application.Ponto.Commands
{
    public record CreatePontoManualCommand(Guid? ID_Funcionario, DateTime DT_Ponto) : IRequest<ObjectResponse<bool>>;

    public class CreatePontoManualHandler(IUnitOfWork unitOfWork, UserInfo userInfo, IBaseValidator<CreatePontoManualCommand, bool> validator, DatabaseContext db) : IRequestHandler<CreatePontoManualCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CreatePontoManualCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new(false);

            if (!await validator.IsValidAsync(command, result, cancellationToken))
            {
                return result;
            }

            unitOfWork.BeginTransaction();

            Guid id_funcionario = command.ID_Funcionario ?? userInfo.ID_Funcionario;

            List<TB_Ponto> pontosJaCadastrados = await (from a in db.TB_Ponto
                                                        where a.DT_Ponto.Date == DateTime.UtcNow.Date && a.ID_Funcionario == id_funcionario
                                                        select a).ToListAsync(cancellationToken);

            string tp_ponto = EntradaOuSaida(pontosJaCadastrados);

            TB_Ponto ponto = new()
            {
                DT_Ponto = command.DT_Ponto,
                ST_Ponto = StatusPonto.PendenteAprovacao,
                ID_Funcionario = id_funcionario,
                TP_Ponto = tp_ponto
            };

            await unitOfWork.Repository.AddAsync(ponto, cancellationToken);
            Exception? error = await unitOfWork.SaveChangesAsync(cancellationToken);

            result.Value = error.Ok();
            return result;
        }

        private static string EntradaOuSaida(List<TB_Ponto> pontosJaCadastrados)
        {
            int count = pontosJaCadastrados.Count;
            return ((count + 1) % 2) == 0 ? TipoPonto.Saida : TipoPonto.Entrada;
        }
    }
}
