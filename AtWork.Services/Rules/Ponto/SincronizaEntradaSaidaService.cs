using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.Application.Ponto;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Extensions;
using AtWork.Shared.Structs;

namespace AtWork.Services.Rules.Ponto
{
    public class SincronizaEntradaSaidaService(IUnitOfWork unitOfWork) : ISincronizaEntradaSaidaService
    {
        public async Task HandleAsync(DateTime dt_base, Guid ID_Funcionario, CancellationToken ct)
        {
            DateTime datBase = dt_base.ToBrazilianTime().Date;

            DateTime dt_ini = datBase.GetFirstMomentOfDate();
            DateTime dt_final = datBase.GetLastMomentOfDate();

            List<TB_Ponto> pontos = await unitOfWork.Repository.GetListAsync<TB_Ponto>(
                item => item.DT_Ponto >= dt_ini && item.DT_Ponto <= dt_final && item.ST_Ponto != StatusPonto.Cancelado && item.ID_Funcionario == ID_Funcionario,
                ct
            );

            int pos = 0;
            List<TB_Ponto> ordered = [.. pontos.OrderBy(x => x.DT_Ponto)];

            foreach (TB_Ponto ponto in ordered)
            {
                if ((pos % 2) == 0)
                    ponto.TP_Ponto = TipoPonto.Entrada;
                else
                    ponto.TP_Ponto = TipoPonto.Saida;

                await unitOfWork.Repository.UpdateAsync(ponto, ct);

                pos++;
            }
        }
    }
}
