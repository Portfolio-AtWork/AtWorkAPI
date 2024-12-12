using AtWork.Domain.Database;
using AtWork.Domain.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace AtWork.Infra.UnitOfWork
{
    public class UnitOfWork(
        DatabaseContext context,
        IBaseRepository baseRepository
    ) : IUnitOfWork
    {
        public IBaseRepository Repository { get; } = baseRepository;
        private IDbTransaction? transaction;

        public IDbTransaction BeginTransaction()
        {
            if (context.Database.GetDbConnection().State != ConnectionState.Open)
            {
                context.Database.OpenConnection();
            }

            transaction = context.Database.BeginTransaction().GetDbTransaction();
            return transaction;
        }

        public Exception? SaveChangesAsync()
        {
            try
            {
                context.SaveChanges();
                transaction?.Commit();
                return null;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                return ex;
            }
        }

        public void Rollback()
        {
            transaction?.Rollback();
        }

        public void Dispose()
        {
            transaction?.Dispose();
            context?.Dispose();
        }
    }
}
