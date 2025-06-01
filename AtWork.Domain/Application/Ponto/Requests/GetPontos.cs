using AtWork.Domain.Base;
using AtWork.Domain.Database;
using AtWork.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AtWork.Domain.Application.Ponto.Requests
{
    public class GetPontosRequest : IRequest<ObjectResponse<List<GetPontosResult>>>;

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

            var pontos = await (from a in db.TB_Ponto
                                where a.DT_Ponto >= DateTime.UtcNow.Date && a.ID_Funcionario == userInfo.ID_Funcionario
                                select new GetPontosResult(a.ID_Funcionario, a.DT_Ponto, a.ST_Ponto, a.TP_Ponto))
                                .ToListAsync(cancellationToken);

            result.Value = pontos;

            return result;
        }
    }
}
