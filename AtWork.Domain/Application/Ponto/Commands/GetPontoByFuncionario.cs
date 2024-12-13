using AtWork.Domain.Database;
using AtWork.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AtWork.Domain.Application.Ponto.Commands
{
    public record GetPontoByFuncionarioRequest(Guid ID_Funcionario) : IRequest<ObjectResponse<List<GetPontoByFuncionarioResult>>>;
    public record GetPontoByFuncionarioResult(Guid ID, Guid ID_Funcionario, DateTime DT_Ponto, string ST_Ponto);

    public class GetPontoByFuncionarioHandler(DatabaseContext db) : IRequestHandler<GetPontoByFuncionarioRequest, ObjectResponse<List<GetPontoByFuncionarioResult>>>
    {
        public async Task<ObjectResponse<List<GetPontoByFuncionarioResult>>> Handle(GetPontoByFuncionarioRequest request, CancellationToken cancellationToken)
        {
            ObjectResponse<List<GetPontoByFuncionarioResult>> result = new();

            List<GetPontoByFuncionarioResult> query = await (from a in db.TB_Ponto
                                                             where a.ID_Funcionario == request.ID_Funcionario
                                                             select new GetPontoByFuncionarioResult(a.ID, a.ID_Funcionario, a.DT_Ponto, a.ST_Ponto))
                                                             .ToListAsync(cancellationToken);

            result.Value = query;

            return result;
        }
    }
}
