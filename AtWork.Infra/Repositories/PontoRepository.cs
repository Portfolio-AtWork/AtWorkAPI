using AtWork.Domain.Database;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.Application;
using Microsoft.EntityFrameworkCore;

namespace AtWork.Infra.Repositories
{
    public class PontoRepository(DatabaseContext db) : IPontoRepository
    {
        public async Task<TB_Ponto?> Add(TB_Ponto entity, CancellationToken ct)
        {
            entity.DT_Cad = DateTime.Now;
            entity.DT_Alt = DateTime.Now;

            var saved = await db.TB_Ponto.AddAsync(entity, ct);

            if (saved is null)
            {
                return null;
            }
            else
            {
                await db.SaveChangesAsync(ct);
            }

            return entity;
        }

        public async Task<List<TB_Ponto>> GetByFuncionario(Guid ID_Funcionario, CancellationToken ct)
        {
            var query = await db.TB_Ponto.Where(item => item.ID_Funcionario == ID_Funcionario)
                                         .ToListAsync(ct);

            return query;
        }

        public async Task<bool> Update(TB_Ponto entity, CancellationToken ct)
        {
            var saved = db.TB_Ponto.Update(entity);

            if (saved is null)
            {
                return false;
            }
            else
            {
                await db.SaveChangesAsync(ct);
            }

            return true;
        }
    }
}
