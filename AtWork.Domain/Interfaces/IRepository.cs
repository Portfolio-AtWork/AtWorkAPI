namespace AtWork.Domain.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<TEntity?> Add(TEntity entity, CancellationToken ct);
        Task<bool> Update(TEntity entity, CancellationToken ct);
    }
}
