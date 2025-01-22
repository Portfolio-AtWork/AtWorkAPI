using AtWork.Domain.Base;
using AtWork.Domain.Database;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AtWork.Domain.Application.Grupo.Requests
{
    public record GetGruposByLoginRequest() : IRequest<ObjectResponse<List<GetGruposByLoginResult>>>;
    public record GetGruposByLoginResult(Guid ID, string Nome);

    public class GetGruposByLoginHandler(DatabaseContext db, UserInfo userInfo) : IRequestHandler<GetGruposByLoginRequest, ObjectResponse<List<GetGruposByLoginResult>>>
    {
        public async Task<ObjectResponse<List<GetGruposByLoginResult>>> Handle(GetGruposByLoginRequest request, CancellationToken cancellationToken)
        {
            ObjectResponse<List<GetGruposByLoginResult>> result = new()
            {
                Value = []
            };

            List<Guid> ids_grupos = await (from grp_x_adm in db.TB_Grupo_X_Admin
                                           where grp_x_adm.ID_Usuario == userInfo.ID_Usuario
                                           select grp_x_adm.ID_Grupo).ToListAsync(cancellationToken);

            List<GetGruposByLoginResult> grupos = await (from grp in db.TB_Grupo
                                                         where ids_grupos.Contains(grp.ID) && grp.ST_Status != StatusRegistro.Cancelado
                                                         select new GetGruposByLoginResult(grp.ID, grp.Nome)).ToListAsync(cancellationToken);

            result.Value.AddRange(grupos);

            return result;
        }
    }
}
