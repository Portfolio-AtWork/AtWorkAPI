using System.Data;

namespace AtWork.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository Repository<TRepository>() where TRepository : class;
        IDbTransaction BeginTransaction();
        Exception? SaveChangesAsync();
        void Rollback();
    }
}
