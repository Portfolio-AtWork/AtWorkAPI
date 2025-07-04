﻿using AtWork.Domain.Database;
using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AtWork.Infra.UnitOfWork
{
    public class BaseRepository(DatabaseContext db) : IBaseRepository
    {
        public async Task<TEntity?> AddAsync<TEntity>(TEntity entity, CancellationToken ct) where TEntity : BaseEntity
        {
            entity.DT_Cad = DateTime.UtcNow.ToBrazilianTime();
            entity.DT_Alt = DateTime.UtcNow.ToBrazilianTime();

            await db.AddAsync(entity, ct);

            int changed = await db.SaveChangesAsync(ct);

            if (changed == 0)
            {
                return null;
            }

            return entity;
        }

        public async Task<TEntity?> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken ct) where TEntity : BaseEntity
        {
            TEntity? first = await db.Set<TEntity>().FirstOrDefaultAsync(predicate, ct);
            return first;
        }

        public async Task<bool> UpdateAsync<TEntity>(TEntity entity, CancellationToken ct) where TEntity : BaseEntity
        {
            entity.DT_Alt = DateTime.UtcNow.ToBrazilianTime();

            db.Update(entity);

            int changed = await db.SaveChangesAsync(ct);
            return changed > 0;
        }
        public async Task<bool> DeleteAsync<TEntity>(TEntity entity, CancellationToken ct) where TEntity : BaseEntity
        {
            db.Remove(entity);

            int changed = await db.SaveChangesAsync(ct);
            return changed > 0;
        }

        public async Task<List<TEntity>> GetListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken ct) where TEntity : BaseEntity
        {
            List<TEntity> first = await db.Set<TEntity>().Where(predicate).ToListAsync(ct);
            return first;
        }
    }
}
