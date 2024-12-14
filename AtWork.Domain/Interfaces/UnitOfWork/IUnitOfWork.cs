using System.Data;

namespace AtWork.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository Repository { get; }
        IDbTransaction BeginTransaction();
        Task<Exception?> SaveChangesAsync(CancellationToken ct);
        void Rollback();
    }
}
