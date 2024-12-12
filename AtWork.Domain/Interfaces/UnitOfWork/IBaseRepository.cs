using AtWork.Domain.Database.Entities;
using System.Linq.Expressions;

namespace AtWork.Domain.Interfaces.UnitOfWork
{
    public interface IBaseRepository
    {
        Task<TEntity?> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken ct) where TEntity : BaseEntity;
        Task<TEntity?> AddAsync<TEntity>(TEntity entity, CancellationToken ct) where TEntity : BaseEntity;
        Task<bool> UpdateAsync<TEntity>(TEntity entity, CancellationToken ct) where TEntity : BaseEntity;
        Task<bool> DeleteAsync<TEntity>(TEntity entity, CancellationToken ct) where TEntity : BaseEntity;
    }
}
