using AtWork.Domain.Base;
using AtWork.Domain.Database;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AtWork.Domain.Application.Ponto.Commands
{
    public class CreatePontoCommand() : IRequest<ObjectResponse<bool>>;

    public class CreatePontoHandler(IUnitOfWork unitOfWork, UserInfo userInfo, DatabaseContext db) : IRequestHandler<CreatePontoCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CreatePontoCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            unitOfWork.BeginTransaction();

            List<TB_Ponto> pontosJaCadastrados = await (from a in db.TB_Ponto
                                                        where a.DT_Ponto.Date == DateTime.UtcNow.Date && a.ID_Funcionario == userInfo.ID_Funcionario
                                                        select a).ToListAsync(cancellationToken);

            string tp_ponto = EntradaOuSaida(pontosJaCadastrados);

            TB_Ponto ponto = new()
            {
                DT_Ponto = DateTime.UtcNow.ToBrazilianTime(),
                ST_Ponto = StatusPonto.Aprovado,
                ID_Funcionario = userInfo.ID_Funcionario,
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
