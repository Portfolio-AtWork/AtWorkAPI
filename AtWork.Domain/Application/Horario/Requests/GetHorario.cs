using AtWork.Domain.Base;
using AtWork.Domain.Database;
using AtWork.Shared.DTO.Horario;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AtWork.Domain.Application.Horario.Requests
{
    public class GetHorarioResult
    {
        public Guid ID_Horario { get; set; }
        public List<DiaDTO> Dias { get; set; } = [];
    };

    public record GetHorarioRequest : IRequest<ObjectResponse<List<GetHorarioResult>>>;

    public class GetHorarioHandler(DatabaseContext db, UserInfo userInfo) : IRequestHandler<GetHorarioRequest, ObjectResponse<List<GetHorarioResult>>>
    {
        public async Task<ObjectResponse<List<GetHorarioResult>>> Handle(GetHorarioRequest request, CancellationToken cancellationToken)
        {
            ObjectResponse<List<GetHorarioResult>> result = new();

            List<GetHorarioResult> horarios = await (from a in db.TB_Horario
                                                     where a.ID_Usuario == userInfo.ID_Usuario && a.ST_Status != StatusRegistro.Cancelado
                                                     select new GetHorarioResult
                                                     {
                                                         ID_Horario = a.ID
                                                     }).ToListAsync(cancellationToken);

            foreach (var horario in horarios)
            {
                horario.Dias = await (from b in db.TB_Horario_X_Dia
                                      where b.ID_Horario == horario.ID_Horario && b.ST_Status != StatusRegistro.Cancelado
                                      select new DiaDTO
                                      {
                                          Dia_Da_Semana = b.Dia_Da_Semana,
                                          Hora_Final = b.Hora_Final,
                                          Hora_Inicio = b.Hora_Inicio,
                                          ST_Status = b.ST_Status,
                                      }).ToListAsync(cancellationToken);
            }

            result.Value = horarios;

            return result;
        }
    }
}
