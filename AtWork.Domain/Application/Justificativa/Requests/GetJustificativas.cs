using AtWork.Domain.Base;
using AtWork.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AtWork.Domain.Application.Justificativa.Requests
{
    public record GetJustificativasRequest(Guid? ID_Funcionario, int Mes, int Ano) : IRequest<ObjectResponse<List<GetJustificativasResult>>>;
    public record GetJustificativasResult(Guid ID_Justificativa, string Justificativa, byte[]? ImagemJustificativa, string ST_Justificativa, DateTime DT_Justificativa)
    {
        public bool TemImagemJustificativa => ImagemJustificativa is not null && ImagemJustificativa.Length > 0;
    };

    public class GetJustificativasHandler(DatabaseContext db, UserInfo userInfo) : IRequestHandler<GetJustificativasRequest, ObjectResponse<List<GetJustificativasResult>>>
    {
        public async Task<ObjectResponse<List<GetJustificativasResult>>> Handle(GetJustificativasRequest request, CancellationToken cancellationToken)
        {
            ObjectResponse<List<GetJustificativasResult>> result = new([]);

            Guid id_justificativa = request.ID_Funcionario ?? userInfo.ID_Funcionario;

            List<GetJustificativasResult> justificativas = await (from a in db.TB_Justificativa
                                                                  where a.DT_Justificativa.Year == request.Ano
                                                                        && a.DT_Justificativa.Month == request.Mes
                                                                        && a.ID_Funcionario == id_justificativa
                                                                  orderby a.DT_Justificativa
                                                                  select new GetJustificativasResult(a.ID, a.Justificativa, a.ImagemJustificativa, a.ST_Justificativa, a.DT_Justificativa))
                                                                  .ToListAsync(cancellationToken);

            result.Value = justificativas;

            return result;
        }
    }
}
