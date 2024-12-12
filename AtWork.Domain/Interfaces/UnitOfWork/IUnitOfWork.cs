using System.Data;

namespace AtWork.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository Repository { get; }
        IDbTransaction BeginTransaction();
        Exception? SaveChangesAsync();
        void Rollback();
    }
}
