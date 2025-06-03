using AtWork.Domain.Base;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AtWork.Domain.Application.Ponto.Requests
{
    public record GetPontosRequest(DateTime DT_Ponto) : IRequest<ObjectResponse<List<GetPontosResult>>>;

    public record GetPontosResult(Guid ID, DateTime DT_Ponto, string ST_Ponto, string TP_Ponto);

    public class GetPontosHandler(DatabaseContext db, UserInfo userInfo) : IRequestHandler<GetPontosRequest, ObjectResponse<List<GetPontosResult>>>
    {
        public async Task<ObjectResponse<List<GetPontosResult>>> Handle(GetPontosRequest request, CancellationToken cancellationToken)
        {
            ObjectResponse<List<GetPontosResult>> result = new([]);

            if (userInfo.ID_Funcionario == Guid.Empty)
            {
                return result;
            }

            DateTime dt_ini = request.DT_Ponto.GetFirstMomentOfDate();
            DateTime dt_final = request.DT_Ponto.GetLastMomentOfDate();


            var pontos = await (from a in db.TB_Ponto
                                where a.DT_Ponto >= dt_ini && a.DT_Ponto <= dt_final
                                && a.ID_Funcionario == userInfo.ID_Funcionario
                                && a.ST_Ponto != StatusPonto.Cancelado
                                orderby a.DT_Ponto
                                select new GetPontosResult(a.ID_Funcionario, a.DT_Ponto, a.ST_Ponto, a.TP_Ponto))
                                .ToListAsync(cancellationToken);

            result.Value = pontos;

            return result;
        }
    }
}
