using AtWork.Domain.Database;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AtWork.Domain.Application.Ponto.Requests
{
    public record GetPontoByFuncionarioRequest(Guid ID_Funcionario, DateTime DT_Ponto) : IRequest<ObjectResponse<List<GetPontoByFuncionarioResult>>>;
    public record GetPontoByFuncionarioResult(Guid ID, Guid ID_Funcionario, DateTime DT_Ponto, string ST_Ponto);

    public class GetPontoByFuncionarioHandler(DatabaseContext db) : IRequestHandler<GetPontoByFuncionarioRequest, ObjectResponse<List<GetPontoByFuncionarioResult>>>
    {
        public async Task<ObjectResponse<List<GetPontoByFuncionarioResult>>> Handle(GetPontoByFuncionarioRequest request, CancellationToken cancellationToken)
        {
            ObjectResponse<List<GetPontoByFuncionarioResult>> result = new();

            if (!request.DT_Ponto.HasValue() || request.DT_Ponto.Date > DateTime.Now.Date)
            {
                result.AddNotification(MessagesStruct.DATA_INFORMADA_NAO_EH_VALIDA, Shared.Enums.Models.NotificationKind.Warning);
                result.Value = [];
                return result;
            }

            DateTime dtInit = request.DT_Ponto.GetFirstMomentOfDate();
            //FIX DEPOIS
            //DateTime dtEnd = request.DT_Ponto.GetLastMomentOfDate();

            var query = await db.TB_Ponto
                                .Where(a => a.ID_Funcionario == request.ID_Funcionario &&
                                            a.DT_Ponto >= dtInit) //&&
                                                                  //a.DT_Ponto <= dtEnd)
                                .Select(a => new GetPontoByFuncionarioResult(a.ID, a.ID_Funcionario, a.DT_Ponto, a.ST_Ponto))
                                .ToListAsync(cancellationToken);


            result.Value = query;

            return result;
        }
    }
}
