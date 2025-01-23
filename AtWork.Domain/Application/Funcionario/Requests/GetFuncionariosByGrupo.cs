using AtWork.Domain.Base;
using AtWork.Domain.Database;
using AtWork.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AtWork.Domain.Application.Funcionario.Requests
{
    public record GetFuncionariosByGrupoRequest(Guid ID_Grupo) : IRequest<ObjectResponse<List<GetFuncionariosByGrupoResult>>>;
    public record GetFuncionariosByGrupoResult(Guid ID, string Nome, string Email, string ST_Status);

    public class GetFuncionariosByGrupoHandler(DatabaseContext db, UserInfo userInfo) : IRequestHandler<GetFuncionariosByGrupoRequest, ObjectResponse<List<GetFuncionariosByGrupoResult>>>
    {
        public async Task<ObjectResponse<List<GetFuncionariosByGrupoResult>>> Handle(GetFuncionariosByGrupoRequest request, CancellationToken cancellationToken)
        {
            ObjectResponse<List<GetFuncionariosByGrupoResult>> result = new();

            List<GetFuncionariosByGrupoResult> funcionarios = await (from a in db.TB_Funcionario
                                                                     where a.ID_Grupo == request.ID_Grupo && a.ID_Usuario == userInfo.ID_Usuario
                                                                     select new GetFuncionariosByGrupoResult(a.ID, a.Nome, a.Email, a.ST_Status)).ToListAsync(cancellationToken);

            result.Value = funcionarios;
            return result;
        }
    }
}
